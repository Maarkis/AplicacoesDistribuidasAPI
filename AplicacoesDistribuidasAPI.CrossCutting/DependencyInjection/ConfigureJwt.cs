﻿using AplicacoesDistribuidasAPI.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AplicacoesDistribuidasAPI.CrossCutting.DependencyInjection
{
    public class ConfigureJwt
    {
        public static void ConfigureDependenciesJwt(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            SigningConfiguration signingConfiguration = new SigningConfiguration();
            serviceCollection.AddSingleton(signingConfiguration);

            TokenConfiguration tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                configuration.GetSection("TokenConfiguration")).Configure(tokenConfiguration);
            serviceCollection.AddSingleton(tokenConfiguration);


            serviceCollection.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;

                bearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingConfiguration.Key,
                    ValidateAudience = true,
                    ValidAudience = tokenConfiguration.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = tokenConfiguration.Issuer,

                };
            });



            serviceCollection.AddAuthorization(auth =>
            {

                //auth.AddPolicy("Admin", builder =>
                //{
                //    builder.RequireAuthenticatedUser();
                //    builder.RequireRole("Admin");
                //});

                //auth.AddPolicy("Editor", builder =>
                //{
                //    builder.RequireAuthenticatedUser();
                //    builder.RequireRole("Editor");
                //});

                auth.AddPolicy("Administrador", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireRole("Admin")                    
                    .Build());

                auth.AddPolicy("RequireEditorRole", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireRole("Editor")
                    .Build());
            });

        }
    }
}
