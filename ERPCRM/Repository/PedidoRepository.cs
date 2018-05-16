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
    public class PedidoRepository: IPedidoRepository
    {
        /// <summary>
        /// Cadastra o novo pedido
        /// </summary>
        /// <param name="objPedido">Dados do novo pedido</param>
        /// <returns>0 - não cadastrou / 0 > id do novo pedido </returns>
        public int Inserir(int idUsuario)
        {
            int retorno = 0;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO_CADASTRAR", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usuario", idUsuario);
            
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
        /// Alteração dos dados do pedido
        /// </summary>
        /// <param name="objPedido">Dados do pedido a ser alterado</param>
        /// <returns></returns>
        public int Alterar(Pedido objPedido)
        {
            int retorno = 0;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO_ALTERAR", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", objPedido.id);
            cmd.Parameters.AddWithValue("@idusuario", objPedido.idUsuario);
            cmd.Parameters.AddWithValue("@status", objPedido.id_status);
            cmd.Parameters.AddWithValue("@ativo", objPedido.ativo);

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
                Trace.WriteLine("Erro - Pedido: Alterar - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return retorno;
        }

        /// <summary>
        /// Apaga o pedido selecionado
        /// </summary>
        /// <param name="idProduto">id do pedido selecionado</param>
        /// <returns></returns>
        public int Apagar(int idPedido)
        {
            int retorno = 0;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pedido", idPedido);

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
                Trace.WriteLine("Erro - Pedido: Apagar - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return retorno;

        }

        /// <summary>
        /// Dados do pedido selecionado
        /// </summary>
        /// <param name="idPedido">id do pedido</param>
        /// <returns>objeto pedido com os dados</returns>
        public Pedido Dados(int idPedido)
        {
            bool bAtivo;

            Pedido objPedido = null;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO_DADOS", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pedido", idPedido);

            try
            {
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        objPedido = new Pedido();
                        objPedido.id = (int)reader["id"];
                        objPedido.idUsuario = (int)reader["id_usuario"];
                        objPedido.id_status = (int)reader["status"];
                        objPedido.statusDescricao = reader["status_descricao"].ToString();
                        
                        if (reader["dte_ins"] != DBNull.Value)
                            objPedido.dteIns = (DateTime)reader["dte_ins"];
                        
                        bool.TryParse(reader["ativo"].ToString(), out bAtivo);
                        objPedido.ativo = bAtivo;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Pedido: Lista - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return objPedido;
        }

        /// <summary>
        /// Lista de pedidos cadastrados
        /// </summary>
        /// <returns>Objeto lista com os pedidos cadastrados</returns>
        public IEnumerable<Pedido> ListaTodos()
        {
            bool bAtivo;

            List<Pedido> list = new List<Pedido>();
            Pedido objPedido = null;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PEDIDO_LISTA", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {

                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        objPedido = new Pedido();
                        objPedido.id = (int)reader["id"];
                        objPedido.idUsuario = (int)reader["id_usuario"];
                        objPedido.id_status = (int)reader["status"];
                        objPedido.statusDescricao = reader["status_descricao"].ToString();

                        if (reader["dte_ins"] != DBNull.Value)
                            objPedido.dteIns = (DateTime)reader["dte_ins"];

                        bool.TryParse(reader["ativo"].ToString(), out bAtivo);
                        objPedido.ativo = bAtivo;

                        list.Add(objPedido);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Pedido: Lista - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return list;
        }

      
    }
}