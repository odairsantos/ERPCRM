using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ERPCRM.Models;

namespace ERPCRM.Repository
{
    public class EstadoRepository
    {
        public EstadoRepository()
        {
        }
        
        public List<SelectListItem> ListaEstados() {

            Banco objBanco = new Banco();

            List<SelectListItem> listaEstados = new List<SelectListItem>();

            SqlCommand comm = new SqlCommand("ESTADOS_LISTAR", objBanco.conn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                using (var readerUF = comm.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (readerUF.HasRows)
                    {
                        while (readerUF.Read())
                        {
                            var t = new SelectListItem()
                            {
                                Value = readerUF.GetInt32(readerUF.GetOrdinal("EstadoID")).ToString(),
                                Text = readerUF.GetString(readerUF.GetOrdinal("UF"))
                            };
                            listaEstados.Add(t);
                        }
                    }
                }
            } catch (Exception ex) {
            }


            return listaEstados;
        }

    }
}
