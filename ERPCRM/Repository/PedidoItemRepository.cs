using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ERPCRM.Models;

namespace ERPCRM.Repository
{
    public class PedidoItemRepository : IPedidoItemRepository
    {
        /// <summary>
        /// Cadastra o novo produto
        /// </summary>
        /// <param name="objPedidoItem"></param>
        /// <returns></returns>
        public int Inserir(PedidoItem objPedidoItem)
        {
            int retorno = 0;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO_ITEM_CADASTRAR", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idpedido", objPedidoItem.id_pedido);
            cmd.Parameters.AddWithValue("@idproduto", objPedidoItem.id_produto);
            cmd.Parameters.AddWithValue("@quantidade", objPedidoItem.quantidade);
            cmd.Parameters.AddWithValue("@valor", objPedidoItem.valor_unitario);
            

            try
            {
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            retorno = (int)reader["retorno"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Pedido: Cadastra - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return retorno;
        }

        /// <summary>
        /// Alteração dos dados do item do pedido
        /// </summary>
        /// <param name="objPedidoItem">Dados do item do pedido a ser alterado</param>
        /// <returns></returns>
        public int Alterar(PedidoItem objPedidoItem)
        {
            int retorno = 0;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO_ITEM_ALTERAR", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", objPedidoItem.id);
            cmd.Parameters.AddWithValue("@idpedido", objPedidoItem.id_pedido);
            cmd.Parameters.AddWithValue("@idproduto", objPedidoItem.id_produto);
            cmd.Parameters.AddWithValue("@quantidade", objPedidoItem.quantidade);
            cmd.Parameters.AddWithValue("@valor", objPedidoItem.valor_unitario);

            try
            {
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            retorno = (int)reader["retorno"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Pedido Item: Alterar - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return retorno;
        }

        /// <summary>
        /// Apaga o item do pedido selecionado da lista do pedido
        /// </summary>
        /// <param name="idPedidoItem">id do item do pedido selecionado</param>
        /// <returns></returns>
        public int Apagar(int idPedidoItem)
        {
            int retorno = 0;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO_ITEM_APAGA", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@produto", idPedidoItem);

            try
            {
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            if ((int)reader["retorno"] == 1)
                            {
                                retorno = 1;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Pedido Item: Apagar - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return retorno;

        }

        /// <summary>
        /// Dados do produto selecionado
        /// </summary>
        /// <param name="idPedidoItem">id do produto</param>
        /// <returns>objeto produto com os dados</returns>
        public PedidoItem Dados(int idPedidoItem)
        {
            decimal dPreco;
            decimal dQuantidade;
            int iUnidade;

            PedidoItem objPedidoItem = null;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO_ITEM_DADOS", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@produto", idPedidoItem);

            try
            {
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        dPreco = 0;

                        objPedidoItem = new PedidoItem();
                        objPedidoItem.id = (int)reader["id"];
                        objPedidoItem.id_pedido = (int)reader["id_pedido"];
                        objPedidoItem.id_produto = (int)reader["id_produto"];
                        objPedidoItem.nome = reader["nome_produto"].ToString();
                        objPedidoItem.EAN = reader["EAN"].ToString();

                        if (reader["dte_ins"] != DBNull.Value)
                            objPedidoItem.dte_ins = (DateTime)reader["dte_ins"];

                        decimal.TryParse(reader["valor_unitario"].ToString(), out dPreco);
                        objPedidoItem.valor_unitario = dPreco;

                        decimal.TryParse(reader["quantidade"].ToString(), out dQuantidade);
                        objPedidoItem.quantidade = dQuantidade;

                        int.TryParse(reader["unidade"].ToString(), out iUnidade);
                        objPedidoItem.id_unidade = iUnidade;

                        objPedidoItem.unidadeDescricao = reader["unidadeDescricao"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Pedido Item: Dados - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return objPedidoItem;
        }

        /// <summary>
        /// Lista de itens do pedido cadastrados para o pedido
        /// </summary>
        /// <returns>Objeto lista com os itens cadastrados para o pedido</returns>
        public IEnumerable<PedidoItem> ListaTodos(int idPedido)
        {
            decimal dPreco;
            decimal dQuantidade;
            int iUnidade;

            PedidoItem objPedidoItem = null;
            
            List<PedidoItem> list = new List<PedidoItem>();
            
            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO_ITEM_LISTA", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        dPreco = 0;

                        objPedidoItem = new PedidoItem();
                        objPedidoItem.id = (int)reader["id"];
                        objPedidoItem.id_pedido = (int)reader["id_pedido"];
                        objPedidoItem.id_produto = (int)reader["id_produto"];
                        objPedidoItem.nome = reader["nome_produto"].ToString();
                        objPedidoItem.EAN = reader["EAN"].ToString();

                        if (reader["dte_ins"] != DBNull.Value)
                            objPedidoItem.dte_ins = (DateTime)reader["dte_ins"];

                        decimal.TryParse(reader["valor_unitario"].ToString(), out dPreco);
                        objPedidoItem.valor_unitario = dPreco;

                        decimal.TryParse(reader["quantidade"].ToString(), out dQuantidade);
                        objPedidoItem.quantidade = dQuantidade;

                        int.TryParse(reader["unidade"].ToString(), out iUnidade);
                        objPedidoItem.id_unidade = iUnidade;

                        objPedidoItem.unidadeDescricao = reader["unidadeDescricao"].ToString();

                        list.Add(objPedidoItem);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Pedido Item: Lista - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return list;
        }
    }
}