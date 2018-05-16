using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ERPCRM.Models;

namespace ERPCRM.Repository
{
    public class ProdutoRepository: IProdutoRepository
    {
        /// <summary>
        /// Cadastra o novo produto
        /// </summary>
        /// <param name="objProduto"></param>
        /// <returns></returns>
        public int Inserir(Produto objProduto)
        {
            int retorno = 0;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PRODUTO_CADASTRAR", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", objProduto.nome);
            cmd.Parameters.AddWithValue("@descricao", objProduto.descricao);
            cmd.Parameters.AddWithValue("@ean", objProduto.EAN);
            cmd.Parameters.AddWithValue("@preco", objProduto.valor_unitario);
            cmd.Parameters.AddWithValue("@unidade", objProduto.id_unidade);

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
                Trace.WriteLine("Erro - Produto: Cadastra - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return retorno;
        }

        /// <summary>
        /// Alteração dos dados do produto
        /// </summary>
        /// <param name="objProduto">Dados do produto a ser alterado</param>
        /// <returns></returns>
        public int Alterar(Produto objProduto)
        {
            int retorno = 0;

            Banco objBanco = new Banco();
            
            SqlCommand cmd = new SqlCommand("PRODUTO_ALTERAR", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", objProduto.id);
            cmd.Parameters.AddWithValue("@nome", objProduto.nome);
            cmd.Parameters.AddWithValue("@descricao", objProduto.descricao);
            cmd.Parameters.AddWithValue("@ean", objProduto.EAN);
            cmd.Parameters.AddWithValue("@preco", objProduto.valor_unitario);
            cmd.Parameters.AddWithValue("@unidade", objProduto.id_unidade);

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
                Trace.WriteLine("Erro - Produto: Alterar - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return retorno;
        }

        /// <summary>
        /// Apaga o produto selecionado
        /// </summary>
        /// <param name="idProduto">id do produto selecionado</param>
        /// <returns></returns>
        public int Apagar(int idProduto)
        {
            int retorno = 0;

            Banco objBanco = new Banco();
            
            SqlCommand cmd = new SqlCommand("PRODUTO_APAGA", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@produto", idProduto);

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
                Trace.WriteLine("Erro - Produto: Apagar - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return retorno;
            
        }

        /// <summary>
        /// Dados do produto selecionado
        /// </summary>
        /// <param name="idProduto">id do produto</param>
        /// <returns>objeto produto com os dados</returns>
        public Produto Dados(int idProduto)
        {
            decimal dPreco;
            int iUnidade;
            
            Produto objProduto = null;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PRODUTO_DADOS", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@produto", idProduto);

            try
            {
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if(reader.Read())
                    {
                        dPreco = 0;

                        objProduto = new Produto();
                        objProduto.id = (int)reader["id"];
                        objProduto.nome = reader["nome"].ToString();
                        objProduto.descricao = reader["descricao"].ToString();
                        objProduto.EAN = reader["EAN"].ToString();

                        if (reader["dte_ins"] != DBNull.Value)
                            objProduto.dte_ins = (DateTime)reader["dte_ins"];

                        decimal.TryParse(reader["preco"].ToString(), out dPreco);
                        objProduto.valor_unitario = dPreco;

                        int.TryParse(reader["unidade"].ToString(), out iUnidade);
                        objProduto.id_unidade = iUnidade;

                        objProduto.unidadeDescricao = reader["unidadeDescricao"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Produto: Lista - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return objProduto;
        }

        /// <summary>
        /// Lista de produtos cadastrados
        /// </summary>
        /// <returns>Objeto lista com os produtos cadastrados</returns>
        public IEnumerable<Produto> ListaTodos()
        {
            decimal dPreco;
            int iUnidade;

            List<Produto> list = new List<Produto>();
            Produto objProduto = null;

            Banco objBanco = new Banco();
            
            SqlCommand cmd = new SqlCommand("PRODUTO_LISTA", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            try
            {

                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        dPreco = 0;

                        objProduto = new Produto();
                        objProduto.id = (int)reader["id"];
                        objProduto.nome = reader["nome"].ToString();
                        objProduto.unidadeDescricao = reader["descricao"].ToString();
                        objProduto.EAN = reader["EAN"].ToString();

                        if (reader["dte_ins"] != DBNull.Value)
                            objProduto.dte_ins = (DateTime)reader["dte_ins"];
                        
                        decimal.TryParse(reader["valor_unitario"].ToString(), out dPreco);
                        objProduto.valor_unitario = dPreco;

                        int.TryParse(reader["unidade"].ToString(), out iUnidade);
                        objProduto.id_unidade = iUnidade;

                        objProduto.unidadeDescricao = reader["unidadeDescricao"].ToString();
                        
                        list.Add(objProduto);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Produto: Lista - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return list;
        }

        /// <summary>
        /// Lista os produtos da pesquisa
        /// </summary>
        /// <param name="pesquisa">String com a pesquisa a ser feita, no nome ou descrição do produto</param>
        /// <returns>Lista de produtos com relação a pesquisa</returns>
        public IEnumerable<Produto> ListaPesquisa(string pesquisa)
        {
            decimal dPreco;
            int iUnidade;

            List<Produto> list = new List<Produto>();
            Produto objProduto = null;

            Banco objBanco = new Banco();

            SqlCommand cmd = new SqlCommand("PRODUTO_PESQUISA", objBanco.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pesquisa", pesquisa);

            try
            {

                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        dPreco = 0;

                        objProduto = new Produto();
                        objProduto.id = (int)reader["id"];
                        objProduto.nome = reader["nome"].ToString();
                        objProduto.descricao = reader["descricao"].ToString();
                        objProduto.EAN = reader["EAN"].ToString();

                        if (reader["dte_ins"] != DBNull.Value)
                            objProduto.dte_ins = (DateTime)reader["dte_ins"];

                        decimal.TryParse(reader["valor_unitario"].ToString(), out dPreco);
                        objProduto.valor_unitario = dPreco;

                        int.TryParse(reader["unidade"].ToString(), out iUnidade);
                        objProduto.id_unidade = iUnidade;

                        objProduto.unidadeDescricao = reader["unidadeDescricao"].ToString();

                        list.Add(objProduto);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Produto: ListaPesquisa - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }

            return list;
        }


    }
}
