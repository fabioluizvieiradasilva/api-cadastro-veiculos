using System.ComponentModel.DataAnnotations;
using api_cadastro_veiculos.Dominio.Enums;

namespace api_cadastro_veiculos.Dominio.DTO
{
    public class AdministradorDTO
    {
        [Required(ErrorMessage ="O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage ="O campo {0} está fora do padrão de email.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage ="O campo {0} é obrigatório.")]        
        public string Senha { get; set; } = default!;

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        public PerfilEnum? Perfil { get; set; } = default!;
    }
}