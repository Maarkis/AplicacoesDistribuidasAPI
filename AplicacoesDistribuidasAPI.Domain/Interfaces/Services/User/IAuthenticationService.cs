using AplicacoesDistribuidasAPI.Domain.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicacoesDistribuidasAPI.Domain.Interfaces.Services.User
{
    public interface IAuthenticationService
    {
        Task<object> FindByLogin(AuthenticationDto user);
    }
}
