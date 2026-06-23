using System.ComponentModel.DataAnnotations;

namespace MVC_GAMEHUB.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
