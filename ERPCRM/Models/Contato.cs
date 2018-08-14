using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ERPCRM.Models
{
    public class Contato
    {
        [Key]
        public int ContatoID { get; set; }

        [Display(Name = "Nome Contato")]
        [StringLength(255, MinimumLength = 3)]
        [Required(ErrorMessage = "Digite o nome do contato")]
        public string Nome { get; set; }

        [Display(Name = "Empresa")]
        [StringLength(255, MinimumLength = 3)]
        [Required(ErrorMessage = "Digite o nome da empresa do contato")]
        public string Empresa { get; set; }


        [Display(Name = "Telefone")]
        [StringLength(13, MinimumLength = 10)]
        public string Telefone { get; set; }
        
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Estado")]
        public int EstadoID { get; set; }

        public IEnumerable<SelectListItem> Estados { get; set; }

    }
}
