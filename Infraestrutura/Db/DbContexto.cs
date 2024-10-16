using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cadastro_veiculos.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace api_cadastro_veiculos.Infraestrutura.Db
{
    public class DbContexto: DbContext
    {

        public DbContexto(DbContextOptions<DbContexto> options) : base(options)
        {
            
        }
        
        public DbSet<Administrador> Administradores { get; set; } = default!;

    }
}