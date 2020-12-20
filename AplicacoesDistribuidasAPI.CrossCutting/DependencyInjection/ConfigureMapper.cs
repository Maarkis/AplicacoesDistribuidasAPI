using AplicacoesDistribuidasAPI.CrossCutting.Mappings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacoesDistribuidasAPI.CrossCutting.DependencyInjection
{
    public class ConfigureMapper
    {
        public static void ConfigureDependencieMapper(IServiceCollection serviceCollection)
        {

            MapperConfiguration config = new AutoMapper.MapperConfiguration(config =>
            {
                config.AddProfile(new DtoToModelProfile());
                config.AddProfile(new EntityToDtoProfile());
                config.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }

    }
}
