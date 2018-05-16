using System;
using System.Collections.Generic;
using ERPCRM.Models;

namespace ERPCRM.Repository
{
    public interface IPedidoItemRepository
    {
        int Inserir(PedidoItem objPedidoItem);
        int Alterar(PedidoItem objPedidoItem);
        int Apagar(int idPedidoItem);
        PedidoItem Dados(int idPedidoItem);
        IEnumerable<PedidoItem> ListaTodos(int idPedido);
    }
}
