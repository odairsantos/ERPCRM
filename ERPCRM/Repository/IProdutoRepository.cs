using System;
using System.Collections.Generic;
using ERPCRM.Models;

namespace ERPCRM.Repository
{
    public interface IProdutoRepository
    {
        int Inserir(Produto objProduto);
        int Alterar(Produto objProduto);
        int Apagar(int idProduto);
        Produto Dados(int idProduto);
        IEnumerable<Produto> ListaTodos();
        IEnumerable<Produto> ListaPesquisa(string pesquisa);
    }
}
