using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_cadastro_veiculos.Dominio.Entidades
{
    public class Veiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]        
        [StringLength(50)]        
        public string Modelo { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Fabricante { get; set; } = default!;

        [Required]
        public int Ano { get; set; } = default!;
    }
}