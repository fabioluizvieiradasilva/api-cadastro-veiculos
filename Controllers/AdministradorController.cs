using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api_cadastro_veiculos.Dominio.DTO;
using api_cadastro_veiculos.Dominio.Entidades;
using api_cadastro_veiculos.Dominio.Enums;
using api_cadastro_veiculos.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace api_cadastro_veiculos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AdministradorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAdministradorService _administradorService;

        public AdministradorController(IAdministradorService administradorService, IConfiguration configuration)
        {
            _administradorService = administradorService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var login  = _administradorService.Login(loginDTO);
            if(login != null)
            {
                var key = _configuration.GetSection("Key").ToString();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
                {
                    new Claim("Email", login.Email),
                    new Claim(ClaimTypes.Role, login.Perfil),
                    new Claim("Perfil", login.Perfil)
                };

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

                var tokenGerado = new JwtSecurityTokenHandler().WriteToken(token); 

                var loginAutenticado = new LoginAutenticadoDTO {
                    Email = login.Email,
                    Perfil = login.Perfil,
                    Token = tokenGerado
                };

                return Ok(loginAutenticado);
            }
            else
            {
                return BadRequest( new {Error="Não foi possivel realizar o login"});
            }                     
            
        }
        [Authorize(Roles = "Adm")]
        [HttpPost]
        public IActionResult Incluir(AdministradorDTO administradorDTO)
        {
            var administrador = new Administrador {
                Email = administradorDTO.Email,
                Senha = administradorDTO.Senha,
                Perfil = administradorDTO.Perfil.ToString() ?? PerfilEnum.Editor.ToString()
            };

            _administradorService.Incluir(administrador);
            return CreatedAtAction(nameof(BuscaPorId), new {Id = administrador.Id}, administrador);
        }
        
        [HttpGet]
        public IActionResult ListarTodos(int? pagina)
        {
            var administradores = _administradorService.ListarTodos(pagina);
            return Ok(administradores);
        }

        [HttpGet("{id}")]
        public IActionResult BuscaPorId(int id)
        {
            var administrador = _administradorService.BuscaPorId(id);

            if(administrador == null)
                return NotFound();
            
            return Ok(administrador);
        }


    }
}