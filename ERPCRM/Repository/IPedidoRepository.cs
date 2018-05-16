using System;
using System.Collections.Generic;
using ERPCRM.Models;

namespace ERPCRM.Repository
{
    public interface IPedidoRepository
    {
        int Inserir(int idUsuario);
        int Alterar(Pedido objPedido);
        int Apagar(int idPedido);
        Pedido Dados(int idPedido);
        IEnumerable<Pedido> ListaTodos();
    }
}