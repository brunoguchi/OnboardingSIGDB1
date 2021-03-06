﻿using System;
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
    public class CargosController : ControllerBase
    {
        private readonly IServicoDeDominioDeCargos servicoDeDominioDeCargos;
        private readonly IMapper iMapper;

        public CargosController(IServicoDeDominioDeCargos servicoDeDominioDeCargos,
                                  IMapper iMapper)
        {
            this.servicoDeDominioDeCargos = servicoDeDominioDeCargos;
            this.iMapper = iMapper;
        }

        /// <summary>
        /// api/empresas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dados = servicoDeDominioDeCargos.ListarTodos();

            return Ok(iMapper.Map<List<CargoDto>>(dados));
        }

        /// <summary>
        /// api/empresas/10
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = servicoDeDominioDeCargos.RecuperarPorId(id);
            return Ok(iMapper.Map<CargoDto>(resultado));
        }

        /// <summary>
        /// api/empresas/
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CargoDto dto)
        {
            servicoDeDominioDeCargos.Adicionar(iMapper.Map<Cargo>(dto));

            return Ok();
        }

        /// <summary>
        /// api/empresas/2
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CargoDto dto)
        {
            dto.Id = id;
            servicoDeDominioDeCargos.Atualizar(iMapper.Map<Cargo>(dto));

            return Ok();
        }

        /// <summary>
        /// api/empresas/9
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            servicoDeDominioDeCargos.Deletar(id);
            return Ok();
        }
    }
}
