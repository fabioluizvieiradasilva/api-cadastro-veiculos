using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cadastro_veiculos.Dominio.Entidades;

namespace api_cadastro_veiculos.Dominio.Interfaces
{
    public interface IVeiculoService
    {
        List<Veiculo>ListarTodos(int? pagina = 1, string? modelo=null, string? fabricante=null);
        Veiculo? BuscarPorId(int id);
        void Incluir(Veiculo veiculo);
        void Atualizar(Veiculo veiculo);
        void Apagar(Veiculo veiculo);

    }
}