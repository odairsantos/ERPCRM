using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERPCRM.Models
{
    public class PedidoItens
    {
        public Pedido pedido { get; set; }
        public IEnumerable<PedidoItem> listPedidoItem { get; set; }
    }
}