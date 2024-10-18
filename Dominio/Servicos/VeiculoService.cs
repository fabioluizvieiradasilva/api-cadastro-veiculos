using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cadastro_veiculos.Dominio.Entidades;
using api_cadastro_veiculos.Dominio.Interfaces;
using api_cadastro_veiculos.Infraestrutura.Db;

namespace api_cadastro_veiculos.Dominio.Servicos
{
    public class VeiculoService : IVeiculoService
    {
        private readonly DbContexto _contexto;

        public VeiculoService(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public void Apagar(Veiculo veiculo)
        {
            _contexto.Veiculos.Remove(veiculo);
            _contexto.SaveChanges();
        }

        public void Atualizar(Veiculo veiculo)
        {
            _contexto.Veiculos.Update(veiculo);
            _contexto.SaveChanges();
        }

        public Veiculo? BuscarPorId(int id)
        {
            var veiculo = _contexto.Veiculos.Find(id);
            return veiculo;
        }

        public void Incluir(Veiculo veiculo)
        {
            _contexto.Veiculos.Add(veiculo);
            _contexto.SaveChanges();
        }

        public List<Veiculo> ListarTodos(int? pagina = 1, string? modelo = null, string? fabricante = null)
        {
            var queryVeiculos = _contexto.Veiculos.AsQueryable();
            
            
            if(!string.IsNullOrEmpty(modelo))
                queryVeiculos = queryVeiculos.Where(v => v.Modelo.ToLower().Contains(modelo!.ToLower()));
            
            int itensPorPagina = 10;

            if(pagina != null)
                queryVeiculos = queryVeiculos.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

            return queryVeiculos.ToList();

        }
    }
}