using AplicacoesDistribuidasAPI.Domain.Dtos.User;
using AplicacoesDistribuidasAPI.Domain.Entities.User;
using AplicacoesDistribuidasAPI.Domain.Interfaces;
using AplicacoesDistribuidasAPI.Domain.Interfaces.Services.User;
using AplicacoesDistribuidasAPI.Domain.Models.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicacoesDistribuidasAPI.Service.Services.User
{
    public class UserService : IUserService
    {

        private IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            UserEntity result = await _repository.SelectAsync(id);
            if (result == null)
                return null;
            return _mapper.Map<UserDto>(result);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            IEnumerable<UserEntity> result = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UserDto>>(result);
        }

        public async Task<UserDtoCreateResult> Post(UserDtoCreate user)
        {
            UserModel model = _mapper.Map<UserModel>(user);
            UserEntity entity = _mapper.Map<UserEntity>(model);
            UserEntity result = await _repository.InsertAsync(entity);

            return _mapper.Map<UserDtoCreateResult>(result);
        }

        public async Task<UserDtoUpdateResult> Put(UserDtoUpdate user)
        {
            UserModel model = _mapper.Map<UserModel>(user);
            UserEntity entity = _mapper.Map<UserEntity>(model);
            UserEntity result = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserDtoUpdateResult>(result);
        }
    }
}
