using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPCRM.Models
{
    public class Produto
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Digite o nome do produto")]
        public string nome { get; set; }
        public string descricao { get; set; }
        public int id_unidade { get; set; }
        [Display(Name = "Unidade")]
        public string unidadeDescricao { get; set; }
        [Display(Name = "EAN")]
        public string EAN { get; set; }
        [Required]
        [Display(Name = "Quantidade")]
        public decimal quantidade { get; set; }
        [Required]
        [Display(Name = "Valor Unitário")]
        [DataType(DataType.Currency)]
        public decimal valor_unitario { get; set; }
        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        public decimal valor_total { get; set; }
        public DateTime dte_ins { get; set; }
        public bool ativo { get; set; }
    }
}