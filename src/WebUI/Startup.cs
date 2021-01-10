using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Woolworth.Application;
using Woolworth.Infrastructure;

namespace Woolworth.WebUI
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
            //Json options added to accept enum as string from API endpoints
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonOptions(options => 
                    options.JsonSerializerOptions
                        .Converters
                        .Add(new JsonStringEnumConverter()));
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddHttpContextAccessor();

            services.AddHealthChecks();

            // Customise default API behaviour
            // Allow to bypass the request when Model state for request is invalid
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "API to server woolworth Web UI",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ranjeet Singh",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/notranjeet"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Woolworth Test");
                c.RoutePrefix = string.Empty;
            });


            app.UseRouting();

            app.UseEndpoints(enpoints => {
                enpoints.MapControllers();
            });

        }
    }
}
