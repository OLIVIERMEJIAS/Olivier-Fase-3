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
        /// <summary>
        /// Verifica la disponibilidad de un aula, en una hora de incio, específica
        /// devuelve "" si esta disponible y una cadena con una hora de fin cuando esté ocupada
        /// </summary>
        /// <param name="horaI"></param>
        /// <param name="dia"></param>
        /// <param name="aulaId"></param>
        /// <returns></returns>
        public string disponibleHoraI(string horaI, char dia, byte aulaId)
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

        
    }
}
