using AplicacoesDistribuidasAPI.Data.Context;
using AplicacoesDistribuidasAPI.Data.Repository.User;
using AplicacoesDistribuidasAPI.Domain.Entities.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AplicacoesDistribuidasAPI.Data.Test.UserRepository
{
    public class UserCrud : BaseTest,  IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvide;

        public UserCrud(DbTest dbTest)
        {
            _serviceProvide = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "Insert User")]
        [Trait("Crud User", "User")]
        public async Task InsertUserTest()
        {
            using (var context = _serviceProvide.GetService<DataBaseContext>())
            {
                UserRepository userRepository = new UserRepository(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),                    
                    Name = Faker.Name.FullName()
                };

                UserEntity _result = await userRepository.InsertAsync(_entity);
                Assert.NotNull(_result);
                Assert.Equal(_entity.Email, _result.Email);
                Assert.Equal(_entity.Name, _result.Name);
                Assert.False(_result.Id == Guid.Empty);


            }

        }

        //[Fact(DisplayName = "Update User")]
        //[Trait("Crud User", "User")]
        //public async Task UpdateUserTest()
        //{
        //    using (var context = _serviceProvide.GetService<DataBaseContext>())
        //    {
        //        UserRepository userRepository = new UserRepository(context);
        //        UserEntity _entity = new UserEntity
        //        {
        //            Email = "teste2@email.com",
        //            Name = "teste2"
        //        };

        //        UserEntity _result = await userRepository.UpdateAsync(_entity);
        //        Assert.NotNull(_result);
        //        Assert.Equal("teste2@email.com", _result.Email);
        //        Assert.Equal("teste2", _result.Name);
        //        Assert.False(_result.Id == Guid.Empty);


        //    }

        //}
    }
}
