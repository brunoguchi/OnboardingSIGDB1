using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Cargos.Dtos;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Consultas;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Servicos;

namespace OnboardingSIGDB1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private readonly IArmazenadorDeCargos armazenadorDeCargos;
        private readonly IAtualizadorDeCargos atualizadorDeCargos;
        private readonly IRemovedorDeCargos removedorDeCargos;
        private readonly IConsultasDeCargos consultasDeCargos;
        private readonly IMapper iMapper;

        public CargosController(IMapper iMapper,
                        IArmazenadorDeCargos armazenadorDeCargos,
                        IAtualizadorDeCargos atualizadorDeCargos,
                        IRemovedorDeCargos removedorDeCargos,
                        IConsultasDeCargos consultasDeCargos)
        {
            this.armazenadorDeCargos = armazenadorDeCargos;
            this.atualizadorDeCargos = atualizadorDeCargos;
            this.removedorDeCargos = removedorDeCargos;
            this.consultasDeCargos = consultasDeCargos;
            this.iMapper = iMapper;
        }

        /// <summary>
        /// api/cargos
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dados = await consultasDeCargos.ListarTodos();
            
            return Ok(dados);
        }

        /// <summary>
        /// api/cargos/10
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await consultasDeCargos.RecuperarPorId(id);

            return Ok(resultado);
        }

        /// <summary>
        /// api/cargos/
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CargoDto dto)
        {
            await armazenadorDeCargos.Adicionar(dto);

            return Ok();
        }

        /// <summary>
        /// api/cargos/2
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CargoDto dto)
        {
            dto.Id = id;
            await atualizadorDeCargos.Atualizar(dto);

            return Ok();
        }

        /// <summary>
        /// api/cargos/9
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await removedorDeCargos.Deletar(id);
            return Ok();
        }
    }
}
