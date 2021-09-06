using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Profile.Api.Middlewares;
using ProfileApi.BusinessService;
using ProfileApi.Repository;

namespace Profile.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy("AngularJSPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
                options.SuppressModelStateInvalidFilter = true;
            }).AddNewtonsoftJson();

            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IProfileRepository, ProfileRepository>();

            services.AddSwaggerGen(setupOptions =>
            {
                setupOptions.SwaggerDoc("docs", new OpenApiInfo
                {
                    Title = "Profile Api",
                    Version = "1.0"
                });

                var dir = new DirectoryInfo(AppContext.BaseDirectory);
                foreach (var xmlFile in dir.EnumerateFiles("*.xml"))
                {
                    setupOptions.IncludeXmlComments(xmlFile.FullName);
                }
                setupOptions.CustomSchemaIds(SwaggerDefaultSchemaIdSelector);
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseExceptionMiddleware();

            app.UseSwagger(c =>
            {
                // Give the route to the generated json
                c.RouteTemplate = "/{documentname}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/docs/swagger.json", "Profile Api");
                c.RoutePrefix = "docs";
            });
        }

        private static string SwaggerDefaultSchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType) return modelType.Name;

            var prefix = modelType.GetGenericArguments()
                .Select(SwaggerDefaultSchemaIdSelector)
                .Aggregate((previous, current) => previous + current);

            if (prefix.Contains("[]"))
                prefix = prefix.Replace("[]", "Array");

            return prefix + modelType.Name.Split('`').First();

        }
    }
}
