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
    public class ContatoRepository
    {
        public ContatoRepository() {
        }

        public IEnumerable<Contato> Listar()
        {
            int IntContatoID = 0;
            int IntEstadoID = 0;

            List<Contato> listaContato = new List<Contato>();

            Contato contatoItem = null;

            Banco objBanco = new Banco();

            SqlCommand comm = new SqlCommand("CONTATO_LISTAR", objBanco.conn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader readerRetorno;

                readerRetorno = comm.ExecuteReader(CommandBehavior.CloseConnection);

                if (readerRetorno.HasRows)
                {
                    while (readerRetorno.Read())
                    {
                        contatoItem = new Contato();

                        int.TryParse(readerRetorno["ContatoID"].ToString(), out IntContatoID);
                        contatoItem.ContatoID = IntContatoID;

                        contatoItem.Nome = readerRetorno["Nome"].ToString();
                        contatoItem.Empresa = readerRetorno["Empresa"].ToString();
                        contatoItem.Email = readerRetorno["Email"].ToString();
                        contatoItem.Telefone = readerRetorno["Telefone"].ToString();

                        int.TryParse(readerRetorno["EstadoID"].ToString(), out IntEstadoID);
                        contatoItem.EstadoID = IntEstadoID;

                        listaContato.Add(contatoItem);
                    }
                }

            }
            catch (Exception ex)
            {
            }


            return listaContato;
        }

        public Contato Dados(int ContatoID)
        {
            int IntContatoID = 0;
            int IntEstadoID = 0;

            List<Contato> listaContato = new List<Contato>();

            Contato contatoItem = null;

            Banco objBanco = new Banco();

            SqlCommand comm = new SqlCommand("CONTATO_DADOS", objBanco.conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@contatoId", ContatoID);

            try
            {
                SqlDataReader readerRetorno;

                readerRetorno = comm.ExecuteReader(CommandBehavior.CloseConnection);

                if (readerRetorno.HasRows)
                {
                    if (readerRetorno.Read())
                    {
                        contatoItem = new Contato();

                        int.TryParse(readerRetorno["ContatoID"].ToString(), out IntContatoID);
                        contatoItem.ContatoID = IntContatoID;

                        contatoItem.Nome = readerRetorno["Nome"].ToString();
                        contatoItem.Empresa = readerRetorno["Empresa"].ToString();
                        contatoItem.Email = readerRetorno["Email"].ToString();
                        contatoItem.Telefone = readerRetorno["Telefone"].ToString();

                        int.TryParse(readerRetorno["EstadoID"].ToString(), out IntEstadoID);
                        contatoItem.EstadoID = IntEstadoID;
                    }
                }

            }
            catch (Exception ex)
            {
            }


            return contatoItem;
        }

        public int cadastrar(Contato objContato)
        {
            int retorno = 0;
            
            Banco objBanco = new Banco();

            SqlCommand comm = new SqlCommand("CONTATO_CADASTRA", objBanco.conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Nome", objContato.Nome);
            comm.Parameters.AddWithValue("@Empresa", objContato.Empresa);
            comm.Parameters.AddWithValue("@Telefone", objContato.Telefone);
            comm.Parameters.AddWithValue("@Email", objContato.Email);
            comm.Parameters.AddWithValue("@Estado", objContato.EstadoID);

            try
            {
                SqlDataReader readerRetorno;

                readerRetorno = comm.ExecuteReader(CommandBehavior.CloseConnection);

                if (readerRetorno.HasRows)
                {
                    if (readerRetorno.Read())
                    {
                        retorno = (int)readerRetorno["retorno"];
                    }
                }

            }
            catch (Exception ex)
            {
            }
            
            return retorno;
        }

    }
}
