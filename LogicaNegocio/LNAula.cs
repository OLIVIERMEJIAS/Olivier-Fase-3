using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;

namespace LogicaNegocio
{
    public class LNAula
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

        public bool disponibleHoraI(string horaI, char dia, byte aulaId)
        {
            ADAula adA = new ADAula(CadConexion);
            try
            {
                return adA.disponibleHoraI(horaI, dia, aulaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool disponibleHoraF(string horaF, char dia, byte aulaId)
        {
            ADAula adA = new ADAula(CadConexion);
            try
            {
                return adA.disponibleHoraF(horaF, dia, aulaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
