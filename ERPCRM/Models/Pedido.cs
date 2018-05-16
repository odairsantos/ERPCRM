using System;
using System.ComponentModel.DataAnnotations;


namespace ERPCRM.Models
{
    public class Pedido
    {
        [Key]
        [Display(Name = "ID do Pedido")]
        public int id { get; set; }
        [Required]
        public int idUsuario { get; set; }
        [Display(Name = "Nome")]
        public string nomeUsuario { get; set; }
        public int id_status { get; set; }
        [Display(Name = "Status do pedido")]
        public string statusDescricao { get; set; }
        [Display(Name = "Data de Cadastro")]
        public DateTime dteIns { get; set; }
        public Boolean ativo { get; set; }

    }
}
