using System;
using System.ComponentModel.DataAnnotations;

namespace ERPCRM.Models
{
    public class Usuario
    {
        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " {0} precisa ter no mínimo {2} characteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação não são iguais.")]
        public string ConfirmPassword { get; set; }
    }
}