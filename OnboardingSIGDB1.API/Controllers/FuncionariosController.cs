using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.API.Dto;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Interfaces.Domain;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IServicoDeDominioDeFuncionarios servicoDeDominioDeFuncionarios;
        private readonly IMapper iMapper;

        public FuncionariosController(IServicoDeDominioDeFuncionarios servicoDeDominioDeFuncionarios,
                                  IMapper iMapper)
        {
            this.servicoDeDominioDeFuncionarios = servicoDeDominioDeFuncionarios;
            this.iMapper = iMapper;
        }

        /// <summary>
        /// api/empresas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dados = servicoDeDominioDeFuncionarios.ListarTodos();

            return Ok(iMapper.Map<List<FuncionarioDto>>(dados));
        }

        /// <summary>
        /// api/empresas/10
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = servicoDeDominioDeFuncionarios.RecuperarPorId(id);
            return Ok(iMapper.Map<FuncionarioDto>(resultado));
        }

        /// <summary>
        /// api/empresas/pesquisar
        /// </summary>
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FuncionarioFiltroDto filtro)
        {
            var resultado = servicoDeDominioDeFuncionarios.PesquisarPorFiltro(iMapper.Map<FuncionarioFiltro>(filtro));
            return Ok(iMapper.Map<List<FuncionarioDto>>(resultado));
        }

        /// <summary>
        /// api/empresas/
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FuncionarioDto dto)
        {
            servicoDeDominioDeFuncionarios.Adicionar(iMapper.Map<Funcionario>(dto));

            return Ok();
        }

        /// <summary>
        /// api/empresas/2
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FuncionarioDto dto)
        {
            dto.Id = id;
            servicoDeDominioDeFuncionarios.Atualizar(iMapper.Map<Funcionario>(dto));

            return Ok();
        }

        /// <summary>
        /// api/empresas/9
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            servicoDeDominioDeFuncionarios.Deletar(id);
            return Ok();
        }

        /// <summary>
        /// api/empresas/2
        /// </summary>
        [HttpPut("vincularEmpresa")]
        public async Task<IActionResult> PutVincular([FromBody] FuncionarioDto dto)
        {
            servicoDeDominioDeFuncionarios.VincularFuncionarioAEmpresa(iMapper.Map<Funcionario>(dto));
            return Ok();
        }
    }
}
