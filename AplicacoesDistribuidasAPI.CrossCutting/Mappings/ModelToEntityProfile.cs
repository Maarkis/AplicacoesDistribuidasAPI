using AplicacoesDistribuidasAPI.Domain.Entities.User;
using AplicacoesDistribuidasAPI.Domain.Models.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacoesDistribuidasAPI.CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ReverseMap();
        }
    }
}
