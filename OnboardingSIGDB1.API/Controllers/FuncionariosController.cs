using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IArmazenadorDeFuncionarios armazenadorDeFuncionarios;
        private readonly IAtualizadorDeFuncionarios atualizadorDeFuncionarios;
        private readonly IRemovedorDeFuncionarios removedorDeFuncionarios;
        private readonly IVinculadorDeFuncionarioCargo vinculadorDeFuncionarioCargo;
        private readonly IVinculadorDeFuncionarioEmpresa vinculadorDeFuncionarioEmpresa;
        private readonly IConsultaDeFuncionarios consultaDeFuncionarios;
        private readonly IMapper iMapper;

        public FuncionariosController(IMapper iMapper,
            IArmazenadorDeFuncionarios armazenadorDeFuncionarios,
            IAtualizadorDeFuncionarios atualizadorDeFuncionarios,
            IRemovedorDeFuncionarios removedorDeFuncionarios,
            IVinculadorDeFuncionarioCargo vinculadorDeFuncionarioCargo,
            IVinculadorDeFuncionarioEmpresa vinculadorDeFuncionarioEmpresa,
            IConsultaDeFuncionarios consultaDeFuncionarios)
        {
            this.iMapper = iMapper;
            this.armazenadorDeFuncionarios = armazenadorDeFuncionarios;
            this.atualizadorDeFuncionarios = atualizadorDeFuncionarios;
            this.removedorDeFuncionarios = removedorDeFuncionarios;
            this.vinculadorDeFuncionarioCargo = vinculadorDeFuncionarioCargo;
            this.vinculadorDeFuncionarioEmpresa = vinculadorDeFuncionarioEmpresa;
            this.consultaDeFuncionarios = consultaDeFuncionarios;
        }

        /// <summary>
        /// api/funcionarios
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var consulta = await consultaDeFuncionarios.ListarTodos();
            
            return Ok(consulta);
        }

        /// <summary>
        /// api/funcionarios/10
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var consulta = await consultaDeFuncionarios.RecuperarPorIdComUltimoCargo(id);

            return Ok(consulta);
        }

        /// <summary>
        /// api/funcionarios/pesquisar
        /// </summary>
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] FuncionarioFiltroDto filtro)
        {
            var consulta = await consultaDeFuncionarios.RecuperarPorFiltro(filtro);

            return Ok(consulta);
        }

        /// <summary>
        /// api/funcionarios/
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FuncionarioDto dto)
        {
            await armazenadorDeFuncionarios.Adicionar(dto);

            return Ok();
        }

        /// <summary>
        /// api/funcionarios/2
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FuncionarioDto dto)
        {
            dto.Id = id;
            await atualizadorDeFuncionarios.Atualizar(dto);

            return Ok();
        }

        /// <summary>
        /// api/funcionarios/9
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await removedorDeFuncionarios.Deletar(id);
            return Ok();
        }

        /// <summary>
        /// api/funcionarios/vincularEmpresa
        /// </summary>
        [HttpPut("vincularEmpresa")]
        public async Task<IActionResult> PutVincularEmpresa([FromBody] FuncionarioDto dto)
        {
            await vinculadorDeFuncionarioEmpresa.VincularFuncionarioAEmpresa(dto);
            return Ok();
        }

        /// <summary>
        /// api/funcionarios/vincularCargo
        /// </summary>
        [HttpPut("vincularCargo")]
        public async Task<IActionResult> PutVincularCargo([FromBody] FuncionarioCargoDto dto)
        {
            await vinculadorDeFuncionarioCargo.VincularFuncionarioAoCargo(dto);
            return Ok();
        }
    }
}
