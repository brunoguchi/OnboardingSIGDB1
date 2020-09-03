using AutoMapper;
using OnboardingSIGDB1.Domain.Cargos.Dtos;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;

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
            CreateMap<FuncionarioCargo, FuncionarioCargoDto>();
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
