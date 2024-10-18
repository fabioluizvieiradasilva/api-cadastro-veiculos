using api_cadastro_veiculos.Dominio.DTO;
using api_cadastro_veiculos.Dominio.Entidades;
using api_cadastro_veiculos.Dominio.Interfaces;
using api_cadastro_veiculos.Infraestrutura.Db;

namespace api_cadastro_veiculos.Dominio.Servicos
{
    public class AdministradorService : IAdministradorService
    {
        private readonly DbContexto _contexto;
        public AdministradorService(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public void Atualizar(Administrador administrador)
        {
            _contexto.Administradores.Update(administrador);
            _contexto.SaveChanges();
        }

        public Administrador? BuscaPorId(int id)
        {
            return _contexto.Administradores.Find(id);
        }

        public void Excluir(Administrador administrador)
        {
            _contexto.Administradores.Remove(administrador);
            _contexto.SaveChanges();
        }

        public void Incluir(Administrador administrador)
        {
            _contexto.Administradores.Add(administrador);
            _contexto.SaveChanges();
        }

        public List<Administrador> ListarTodos(int? pagina, string? email=null)
        {
            var queryAdm = _contexto.Administradores.AsQueryable();
            
            
            if(!string.IsNullOrEmpty(email))
                queryAdm = queryAdm.Where(adm => adm.Email.ToLower().Contains(email!.ToLower()));
            
            int itensPorPagina = 10;

            if(pagina != null)
                queryAdm = queryAdm.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

            return queryAdm.ToList();

        }

        public Administrador Login(LoginDTO loginDTO)
        {
            var adm = _contexto.Administradores.Where(
                a => a.Email == loginDTO.Email && 
                a.Senha == loginDTO.Senha
            ).FirstOrDefault();

            return adm;
        }
    }
}