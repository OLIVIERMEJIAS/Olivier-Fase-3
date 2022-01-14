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
