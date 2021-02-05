using AplicacoesDistribuidasAPI.CrossCutting.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace AplicacoesDistribuidasAPI.Application
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

            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);
            ConfigureDataBase.ConfigureDependenciesDataBase(services, Configuration.GetConnectionString("aplicacoesDistribuidas"));

            // Configuration Mapper
            ConfigureMapper.ConfigureDependencieMapper(services);
            // End Configuration Mapper

            // Configuration JTW
            ConfigureJwt.ConfigureDependenciesJwt(services, Configuration);
            // End Configuration JTW



            services.AddControllers();

            // Configuration Swagger
            services.AddSwaggerGen(swaggerGen =>
            {
                swaggerGen.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API - Aplicação distribuídas",
                    Description = "API para gerenciamento de crud de produto",
                    TermsOfService = new Uri("https://github.com/Maarkis/AplicacoesDistribuidasAPI"),
                    Contact = new OpenApiContact
                    {
                        Name = "Jean Markis",
                        Email = "jeanmarkis85@gmail.com",
                        Url = new Uri("https://github.com/Maarkis/AplicacoesDistribuidasAPI"),
                    }
                });

                swaggerGen.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Token JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                swaggerGen.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });
            });

            // End Configuration Swagger

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUI =>
            {
                swaggerUI.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Estudo");
                swaggerUI.RoutePrefix = string.Empty;
            });          
                                  

            
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
