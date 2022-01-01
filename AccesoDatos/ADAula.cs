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

        public bool disponibleHoraF(string horaF, char dia, byte aulaId)
        {
            
            bool result = true;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 From DetallesHorario Where " +
                $"aulaId = {aulaId} and dia = '{dia}' " +
                $"and horaFin = '{horaF}'";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                if (comando.ExecuteScalar() != null)
                {
                    result = false;
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

        public bool disponibleHoraI(string horaI, char dia, byte aulaId)
        {

            bool result = true;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 From DetallesHorario Where " +
                $"aulaId = {aulaId} and dia = '{dia}' and horaInicio = " +
                $"'{horaI}'";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                if (comando.ExecuteScalar() != null)
                {
                    result = false;
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
