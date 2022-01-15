using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class ADTrimestre
    {
        public string CadConexion { get; set; }

        public ADTrimestre()
        {
            CadConexion = "";
        }

        public ADTrimestre(string cad)
        {
            CadConexion = cad;
        }

        public ETrimestre listar(byte numTrim)
        {
            ETrimestre trim = new ETrimestre();
            SqlDataReader datos;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select fechaFin From Trimestres" +
                $" Where numero = {numTrim}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                datos = comando.ExecuteReader();
                if (datos.HasRows)
                {
                    datos.Read();                    
                    trim.FechaFin = datos.GetDateTime(0);
                 
                }
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se pudo realizar conexión de datos");
            }

            return trim;
        }
        }
}
