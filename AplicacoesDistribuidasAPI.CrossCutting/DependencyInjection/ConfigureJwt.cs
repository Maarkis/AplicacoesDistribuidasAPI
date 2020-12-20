using AplicacoesDistribuidasAPI.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
            });



            serviceCollection.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });

        }
    }
}
