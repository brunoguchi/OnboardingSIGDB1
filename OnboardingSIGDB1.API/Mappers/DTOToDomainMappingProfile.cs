using AutoMapper;
using Microsoft.Extensions.Logging.Internal;
using OnboardingSIGDB1.API.Dto;
using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Mappers
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<EmpresaFiltroDto, EmpresaFiltro>().ForMember(x => x.Cnpj, opt => opt.MapFrom(src => this.FormatarDocumento(src.Cnpj)));
            CreateMap<EmpresaDto, Empresa>().ForMember(x => x.Cnpj, opt => opt.MapFrom(src => this.FormatarDocumento(src.Cnpj)));
            CreateMap<CargoDto, Cargo>();
            CreateMap<FuncionarioDto, Funcionario>().ForMember(x => x.Cpf, opt => opt.MapFrom(src => this.FormatarDocumento(src.Cpf)));
            CreateMap<FuncionarioFiltroDto, FuncionarioFiltro>().ForMember(x => x.Cpf, opt => opt.MapFrom(src => this.FormatarDocumento(src.Cpf)));
            CreateMap<FuncionarioCargoDto, FuncionarioCargo>();
        }

        private string FormatarDocumento(string documento)
        {
            return !string.IsNullOrEmpty(documento) ? documento.Replace(".", "").Replace("-", "").Replace("/", "") : string.Empty;
        }
    }
}
