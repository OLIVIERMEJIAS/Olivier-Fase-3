using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;

namespace LogicaNegocio
{
    class LNAula
    {
        public string CadConexion { get; set; }
        
        public LNAula()
        {
            CadConexion = "";
        }

        public LNAula(string cad)
        {
            CadConexion = cad;
        }

        public bool disponible(string horaI, string horaF, char dia, byte aulaId)
        {
            ADAula adA = new ADAula(CadConexion);
            try
            {
                return adA.disponible(horaI, horaF, dia, aulaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
