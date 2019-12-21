using System;
using System.Collections.Generic;
using System.Linq;
using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.API.Services;

namespace ProSMan.Backend
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ProSManContext>(options =>
			{
				options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"],
						b => b.MigrationsAssembly(typeof(ProSManContext).Assembly.GetName().Name));

				options.UseOpenIddict();
			}, ServiceLifetime.Transient);

			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<ProSManContext>()
				.AddDefaultTokenProviders();
			
			services.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddOAuthValidation(options =>
			{
				options.Events = new OAuthValidationEvents
				{
					OnRetrieveToken = context =>
					{
						context.Properties.AllowRefresh = true;
						context.Token = context.Request.Query["access_token"];
						return System.Threading.Tasks.Task.CompletedTask;
					}
				};
			});

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.SignIn.RequireConfirmedEmail = false;

				options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
				options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
			});

			services.AddOpenIddict()
				.AddCore(options =>
				{
					options.UseEntityFrameworkCore()
						.UseDbContext<ProSManContext>();
				})
				.AddServer(options =>
				{
					options.UseMvc();

					options.AllowPasswordFlow();
					options.AllowRefreshTokenFlow();
					options.DisableHttpsRequirement();
					options.UseRollingTokens();
					options.EnableTokenEndpoint("/api/authorization/token");
					options.AddSigningKey(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])));
					options.SetAccessTokenLifetime(TimeSpan.FromDays(1));
					options.SetRefreshTokenLifetime(TimeSpan.FromDays(1));
					options.AcceptAnonymousClients();
					options.DisableScopeValidation();
				});

			services.AddScoped<ITaskService, TaskService>();
			services.AddScoped<IProjectService, ProjectService>();
			services.AddScoped<ISprintService, SprintService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<INonSprintTaskService, NonSprintTaskService>();
			services.AddScoped<IBacklogTaskService, BacklogTaskService>();
			services.AddScoped<IDashboardService, DashboardService>();

			services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

			services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			}));

			services.AddAutoMapper();

			services.AddMvc();

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Info { Title = "ProSMan API", Version = "v1" });

				options.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
				options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
				{ "Bearer", Enumerable.Empty<string>() },
				});
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMapper mapper)
		{
			app.UseAuthentication();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			mapper.ConfigurationProvider.AssertConfigurationIsValid();

			app.UseCors("MyPolicy");
			app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				c.RoutePrefix = string.Empty;
			});
		}
	}
}
