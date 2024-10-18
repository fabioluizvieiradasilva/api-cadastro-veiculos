using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api_cadastro_veiculos.Dominio.DTO;
using api_cadastro_veiculos.Dominio.Entidades;
using api_cadastro_veiculos.Dominio.Interfaces;
using api_cadastro_veiculos.Infraestrutura.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api_cadastro_veiculos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculoController( IVeiculoService veiculoService)
        {            
            _veiculoService = veiculoService;
        }

        [HttpPost]
        [Authorize(Roles = "Adm")]
        public IActionResult Incluir(VeiculoDTO veiculoDTO)
        {
            var veiculo = new Veiculo {
                Modelo = veiculoDTO.Modelo,
                Fabricante = veiculoDTO.Fabricante,
                Ano = veiculoDTO.Ano               
            };
            
            _veiculoService.Incluir(veiculo);
            return CreatedAtAction(nameof(ObterPorId), new {Id = veiculo.Id}, veiculo);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId (int id)
        {
            var veiculo = _veiculoService.BuscarPorId(id);
            if(veiculo == null)
                return NotFound();

            return Ok(veiculo);
        }

        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos(int? pagina)
        {
            var veiculos = _veiculoService.ListarTodos(pagina);
            return Ok(veiculos);
        }
        
        [Authorize(Roles = "Adm")]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, VeiculoDTO veiculoDTO)
        {
            var veiculo = _veiculoService.BuscarPorId(id);
            if(veiculo == null)
                return NotFound();
            
            veiculo.Fabricante = veiculoDTO.Fabricante;
            veiculo.Modelo = veiculoDTO.Modelo;
            veiculo.Ano = veiculoDTO.Ano;

            _veiculoService.Atualizar(veiculo);

            return NoContent();
        }

        [Authorize(Roles = "Adm")]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var veiculo = _veiculoService.BuscarPorId(id);
            if(veiculo == null)
                return NotFound();

            _veiculoService.Apagar(veiculo);
            return NoContent();
        }
    }
}