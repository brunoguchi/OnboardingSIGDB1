using AutoMapper;
using OnboardingSIGDB1.API.Dto;
using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Mappers
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<EmpresaFiltro, EmpresaFiltroDto>();
            CreateMap<Empresa, EmpresaDto>().ForMember(x => x.Cnpj, opt => opt.MapFrom(src => this.FormatarDocumento(src.Cnpj))); ;
        }

        private string FormatarDocumento(string documento)
        {
            return !string.IsNullOrEmpty(documento) ? Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00") : string.Empty;
        }
    }
}
