using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.API.Dto;
using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Mappers
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToDTOMappingProfile>();
                cfg.AddProfile<DTOToDomainMappingProfile>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
