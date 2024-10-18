using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace api_cadastro_veiculos.Dominio.DTO
{
    public class VeiculoDTO    
    {        
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [MinLength(2, ErrorMessage ="O tamanho mínimo do campo {0} foi atingido")]
        [StringLength(50,MinimumLength =2)]  
        public string Modelo { get; set; } = default!;

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [MinLength(2, ErrorMessage ="O tamanho mínimo do campo {0} foi atingido")]
        [StringLength(50,MinimumLength =2)]          
        public string Fabricante { get; set; } = default!;

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [Range(1900, 2024, ErrorMessage ="O valor do campo {0} deve ser entre {1} e {2}.")]        
        public int Ano { get; set; } = default!;                
    }
}