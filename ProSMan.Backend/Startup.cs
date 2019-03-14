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
using Microsoft.Extensions.Options;
using ProSMan.Backend.API.Profiles;
using ProSMan.Backend.Infrastructure;
using ProSMan.Backend.Model;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ProSMan.Backend
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ProSManContext>(options =>
			{
				options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"],
						b => b.MigrationsAssembly(typeof(ProSManContext).Assembly.GetName().Name));

				options.UseOpenIddict();
			});

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
					// Note: for SignalR connections, the default Authorization header does not work,
					// because the WebSockets JS API doesn't allow setting custom parameters.
					// To work around this limitation, the access token is retrieved from the query string.
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
					options.SetRefreshTokenLifetime(TimeSpan.FromDays(1));
					options.AcceptAnonymousClients();
					options.DisableScopeValidation();
				});

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

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseAuthentication();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

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
