using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cadastro_veiculos.Dominio.DTO;
using api_cadastro_veiculos.Dominio.Entidades;

namespace api_cadastro_veiculos.Dominio.Interfaces
{
    public interface IAdministradorService
    {
        Administrador Login (LoginDTO loginDTO);
        void Incluir(Administrador administrador);
        void Atualizar (Administrador administrador);
        void Excluir(Administrador administrador);
        List<Administrador> ListarTodos(int? pagina, string? email = null);
        Administrador? BuscaPorId (int id);
    }
}