using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Signawel.API.Extensions;
using Signawel.Business.MapperProfiles;
using Signawel.Data;
using Signawel.Data.Abstractions.Repositories;

namespace Signawel.API
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
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v0.1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "SignawelApi",
                    Version = "v0.1"
                });
            });

            // Add AutoMapper profile
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddDbContext<SignawelDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SignawelDb")))
                .AddSignawelAuthentication(Configuration)
                .AddSignawelDeterminationGraph();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionJsonResponse();
                app.UseSwagger();
                app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v0.1/swagger.json", "SignawelApi v0.1");
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
