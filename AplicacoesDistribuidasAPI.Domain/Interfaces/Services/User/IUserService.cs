using AplicacoesDistribuidasAPI.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicacoesDistribuidasAPI.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> Get(Guid id);
        Task<UserEntity> Post(UserEntity user);
        Task<UserEntity> Put(UserEntity user);
        Task<bool> Delete(Guid id);
    }
}
