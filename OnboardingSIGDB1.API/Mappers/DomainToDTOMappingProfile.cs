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
            CreateMap<Empresa, EmpresaDto>().ForMember(x => x.Cnpj, opt => opt.MapFrom(src => this.FormatarDocumentoCnpj(src.Cnpj)));
            CreateMap<Cargo, CargoDto>();
            CreateMap<Funcionario, FuncionarioDto>().ForMember(x => x.Cpf, opt => opt.MapFrom(src => this.FormatarDocumentoCpf(src.Cpf)));
            CreateMap<FuncionarioFiltro, FuncionarioFiltroDto>();
        }

        private string FormatarDocumentoCnpj(string documento)
        {
            return !string.IsNullOrEmpty(documento) ? Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00") : string.Empty;
        }

        private string FormatarDocumentoCpf(string documento)
        {
            return !string.IsNullOrEmpty(documento) ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00") : string.Empty;
        }
    }
}
