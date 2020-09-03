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
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<EmpresaFiltroDto, EmpresaFiltro>().ForMember(x => x.Cnpj, opt => opt.MapFrom(src => src.Cnpj.RemoverFormatacaoDocumento()));
            CreateMap<EmpresaDto, Empresa>().ForMember(x => x.Cnpj, opt => opt.MapFrom(src => src.Cnpj.RemoverFormatacaoDocumento()));
            CreateMap<CargoDto, Cargo>();
            CreateMap<FuncionarioDto, Funcionario>().ForMember(x => x.Cpf, opt => opt.MapFrom(src => src.Cpf.RemoverFormatacaoDocumento()));
            CreateMap<FuncionarioFiltroDto, FuncionarioFiltro>().ForMember(x => x.Cpf, opt => opt.MapFrom(src => src.Cpf.RemoverFormatacaoDocumento()));
            CreateMap<FuncionarioCargoDto, FuncionarioCargo>();
        }
    }
}
