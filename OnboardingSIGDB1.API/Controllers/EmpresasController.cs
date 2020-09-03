using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IServicoDeDominioDeEmpresas servicoDeDominioDeEmpresas;
        private readonly IMapper iMapper;

        public EmpresasController(IServicoDeDominioDeEmpresas servicoDeDominioDeEmpresas,
                                  IMapper iMapper)
        {
            this.servicoDeDominioDeEmpresas = servicoDeDominioDeEmpresas;
            this.iMapper = iMapper;
        }

        /// <summary>
        /// api/empresas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dados = servicoDeDominioDeEmpresas.ListarTodas();

            return Ok(iMapper.Map<List<EmpresaDto>>(dados));
        }

        /// <summary>
        /// api/empresas/10
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = servicoDeDominioDeEmpresas.RecuperarPorId(id);
            return Ok(iMapper.Map<EmpresaDto>(resultado));
        }

        /// <summary>
        /// api/empresas/pesquisar
        /// </summary>
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] EmpresaFiltroDto filtro)
        {
            var resultado = servicoDeDominioDeEmpresas.PesquisarPorFiltro(iMapper.Map<EmpresaFiltro>(filtro));
            return Ok(iMapper.Map<List<EmpresaDto>>(resultado));
        }

        /// <summary>
        /// api/empresas/
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpresaDto dto)
        {
            servicoDeDominioDeEmpresas.Adicionar(iMapper.Map<Empresa>(dto));

            return Ok();
        }

        /// <summary>
        /// api/empresas/2
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmpresaDto dto)
        {
            dto.Id = id;
            servicoDeDominioDeEmpresas.Atualizar(iMapper.Map<Empresa>(dto));

            return Ok();
        }

        /// <summary>
        /// api/empresas/9
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            servicoDeDominioDeEmpresas.Deletar(id);
            return Ok();
        }
    }
}
