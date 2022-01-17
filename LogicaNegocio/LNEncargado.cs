using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using System.Data;

namespace LogicaNegocio
{
    public class LNEncargado
    {
        public string CadConexion { get; set; }

        public LNEncargado()
        {
            CadConexion = "";
        }

        public LNEncargado(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Listar los encargados con datos completos
        /// devuelve un DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable listar()
        {
            ADEncargado ade = new ADEncargado(CadConexion);
            try
            {
                return ade.listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
