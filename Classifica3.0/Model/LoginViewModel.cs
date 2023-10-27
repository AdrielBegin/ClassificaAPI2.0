using System.ComponentModel.DataAnnotations;

namespace Classifica3._0.Model
{
    public class LoginViewModel
    {
        //[Required(ErrorMessage = "Email é obrigatorio")]
        [EmailAddress(ErrorMessage = "Senha obrigatorio")]
        public string? Email { get; set; }

        //[Required(ErrorMessage = "Senha é obrigatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembra-me")]
        public bool RememberMe { get; set; }

    }
}
