﻿using AutoMapper;
using OnboardingSIGDB1.Domain.Cargos.Dtos;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;

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
