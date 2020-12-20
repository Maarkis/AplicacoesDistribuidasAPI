using AplicacoesDistribuidasAPI.Domain.Dtos.User;
using AplicacoesDistribuidasAPI.Domain.Models.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacoesDistribuidasAPI.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDto>()
                .ReverseMap();

            CreateMap<UserModel, UserDtoCreate>()
                .ReverseMap();

            CreateMap<UserModel, UserDtoUpdate>()
                .ReverseMap();

        }
    }
}
