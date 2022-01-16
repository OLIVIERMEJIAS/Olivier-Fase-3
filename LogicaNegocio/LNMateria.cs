using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class LNMateria
    {
        public string CadConexion { get; set; }

        public LNMateria()
        {
            CadConexion = "";
        }

        public LNMateria(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Accese a la materia que imparte un profesor, con su Id, 
        /// devuelve un objeto EMateria
        /// </summary>
        /// <param name="profeId"></param>
        /// <returns></returns>
        public EMateria accederAMateria(int profeId)
        {
            
            ADMateria adm = new ADMateria(CadConexion);
            try
            {
                return adm.accederAMateria(profeId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
