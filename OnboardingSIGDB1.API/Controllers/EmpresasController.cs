using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IArmazenadorDeEmpresas armazenadorDeEmpresas;
        private readonly IAtualizadorDeEmpresas atualizadorDeEmpresas;
        private readonly IRemovedorDeEmpresas removedorDeEmpresas;
        private readonly IConsultasDeEmpresas consultasDeEmpresas;
        private readonly IMapper iMapper;

        public EmpresasController(IMapper iMapper,
            IArmazenadorDeEmpresas armazenadorDeEmpresas,
            IAtualizadorDeEmpresas atualizadorDeEmpresas,
            IRemovedorDeEmpresas removedorDeEmpresas,
            IConsultasDeEmpresas consultasDeEmpresas)
        {
            this.iMapper = iMapper;
            this.armazenadorDeEmpresas = armazenadorDeEmpresas;
            this.atualizadorDeEmpresas = atualizadorDeEmpresas;
            this.removedorDeEmpresas = removedorDeEmpresas;
            this.consultasDeEmpresas = consultasDeEmpresas;
        }

        /// <summary>
        /// api/empresas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await consultasDeEmpresas.ListarTodas();

            return Ok(resultado);
        }

        /// <summary>
        /// api/empresas/10
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await consultasDeEmpresas.RecuperarPorId(id);

            return Ok(resultado);
        }

        /// <summary>
        /// api/empresas/pesquisar
        /// </summary>
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] EmpresaFiltroDto filtro)
        {
            var resultado = await consultasDeEmpresas.RecuperarPorFiltro(filtro);

            return Ok(resultado);
        }

        /// <summary>
        /// api/empresas/
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpresaDto dto)
        {
            await armazenadorDeEmpresas.Adicionar(iMapper.Map<Empresa>(dto));

            return Ok();
        }

        /// <summary>
        /// api/empresas/2
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmpresaDto dto)
        {
            dto.Id = id;
            await atualizadorDeEmpresas.Atualizar(iMapper.Map<Empresa>(dto));

            return Ok();
        }

        /// <summary>
        /// api/empresas/9
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await removedorDeEmpresas.Deletar(id);
            return Ok();
        }
    }
}
