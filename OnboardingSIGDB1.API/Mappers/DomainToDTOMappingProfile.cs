using AutoMapper;
using OnboardingSIGDB1.Domain.Cargos.Dtos;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Core.Extensions;

namespace OnboardingSIGDB1.API.Mappers
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<EmpresaFiltro, EmpresaFiltroDto>();
            CreateMap<Empresa, EmpresaDto>().ForMember(x => x.Cnpj, opt => opt.MapFrom(src => src.Cnpj.AplicarFormatacaoCNPJ()));
            CreateMap<Cargo, CargoDto>();
            CreateMap<Funcionario, FuncionarioDto>().ForMember(x => x.Cpf, opt => opt.MapFrom(src => src.Cpf.AplicarFormatacaoCPF()));
            CreateMap<FuncionarioFiltro, FuncionarioFiltroDto>();
            CreateMap<FuncionarioCargo, FuncionarioCargoDto>();
        }
    }
}
