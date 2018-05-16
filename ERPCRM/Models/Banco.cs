using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ERPCRM.Models
{
    public class Banco
    {
        private String strConexao = WebConfigurationManager.ConnectionStrings["conexao"].ConnectionString;

        public SqlConnection conn = null;

        public Banco()
        {
            AbrirConexao();
        }

        /// <summary>
        /// Abre a conexão com o banco de dados
        /// </summary>
        public void AbrirConexao()
        {
            try
            {
                if(conn.State == ConnectionState.Closed)
                { 
                    conn = new SqlConnection(strConexao);
                    conn.Open();
                }
                
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Erro - Banco: AbrirConexao - Mensagem:" + ex.Message + " - Exeção: " + ex.InnerException);
            }
        }
    }
}