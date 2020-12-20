
using AplicacoesDistribuidasAPI.Domain.Dtos.Authentication;
using AplicacoesDistribuidasAPI.Domain.Entities.User;
using AplicacoesDistribuidasAPI.Domain.Interfaces.Services.User;
using AplicacoesDistribuidasAPI.Domain.Repository;
using AplicacoesDistribuidasAPI.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AplicacoesDistribuidasAPI.Service.Services.User
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly SigningConfiguration _signingConfiguration;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IConfiguration _configuaration;

        private readonly IUserRepository _repository;

        public AuthenticationService(IUserRepository repository,
                                    IConfiguration configuration,
                                    SigningConfiguration signingConfiguration,
                                    TokenConfiguration tokenConfiguration
                                   )
        {
            _repository = repository;
            _configuaration = configuration;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;

        }

        public async Task<object> FindByLogin(AuthenticationDto authentication)
        {
            UserEntity result = new UserEntity();
            if (authentication != null && !string.IsNullOrWhiteSpace(authentication.Email))
            {
                result = await _repository.FindByLogin(authentication.Email);
                if (result == null)
                {
                    return new
                    {
                        authentication = false,
                        message = "Falha na authenticação"
                    };
                }

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(result.Email),
                    new[]{
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, result.Email)
                    });

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToDouble(_tokenConfiguration.Seconds));

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token, result);
            }
            return null;
        }


        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            string token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity user)
        {
            return new
            {
                authenticated = true,
                create = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                token = token,
                userName = user.Name,
                userEmail = user.Email,
                message = "Usuário autenticado com sucesso"
            };

        }
    }
}
