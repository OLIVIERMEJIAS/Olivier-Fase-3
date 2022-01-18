using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using System.Data;
using Entidades;

namespace LogicaNegocio
{
    public class LNDistrito
    {
        public string CadConexion { get; set; }

        public LNDistrito()
        {
            CadConexion = "";
        }

        public LNDistrito(string cad)
        {
            CadConexion = cad;
        }

        public EDistrito nombre(int disId)
        {
            ADDistrito add = new ADDistrito(CadConexion);
            try
            {
                return add.nombre(disId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Lista todos los distritos, devuelve un DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable listar()
        {
            ADDistrito add = new ADDistrito(CadConexion);
            try
            {
                return add.listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
