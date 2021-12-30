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

        public bool disponible(string horaI, string horaF, char dia, byte aulaId)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 From DetallesHorario Where " +
                $"aulaId = {aulaId} and dia = {dia} and horaInicio = " +
                $"'{horaI}' and horaFin = '{horaF}'";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                if (comando.ExecuteNonQuery() != 0)
                {
                    result = true;
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
