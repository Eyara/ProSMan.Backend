using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProSMan.Backend.API.Profiles;
using ProSMan.Backend.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

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
			});

			services.AddAutoMapper();

			services.AddMvc();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "ProSMan API", Version = "v1" });
			});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
