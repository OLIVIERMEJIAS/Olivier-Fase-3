using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class ADAula
    {
        public string CadConexion { get; set; }

        public ADAula()
        {
            CadConexion = "";
        }

        public ADAula(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Verifica que en una hora de inicio de lección un aula no este ocupada,
        /// devuelve un string con la hora final si está ocupada, y "" si no lo está,
        /// se basa en una hora de inicio, un día y un Id de aula
        /// </summary>
        /// <param name="horaI"></param>
        /// <param name="dia"></param>
        /// <param name="aulaId"></param>
        /// <returns></returns>
        public string disponibleHoraI(string horaI, char dia, byte aulaId)
        {
            
            string result = "";
            Object dato;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select horaFin From DetallesHorario Where " +
                $"aulaId = {aulaId} and dia = '{dia}' " +
                $"and horaInicio = '{horaI}'";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                dato = comando.ExecuteScalar();
                if (dato != null)
                {
                    result = dato.ToString();
                }
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se pudo realizar conexión de datos");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;
        }

        
    }
}
