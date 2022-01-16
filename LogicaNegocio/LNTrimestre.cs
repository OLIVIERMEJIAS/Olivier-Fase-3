using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using AccesoDatos;

namespace LogicaNegocio
{
    public class LNTrimestre
    {
        public string CadConexion { get; set; }

        public LNTrimestre()
        {
            CadConexion = "";
        }

        public LNTrimestre(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Lista la fecha de fin de un trimeste basándose en el número 
        /// de este, devuelve un objeto ETrimestre
        /// </summary>
        /// <param name="numTrim"></param>
        /// <returns></returns>
        public ETrimestre listar(byte numTrim)
        {
            ADTrimestre adT = new ADTrimestre(CadConexion);
            try
            {
                return adT.listar(numTrim);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
