using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
   public class ADEstudiante
    {
        public string CadConexion { get; set; }

        public ADEstudiante()
        {
            CadConexion = "";
        }

        public ADEstudiante(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Verifica si un estudiante existe, en base a un Id de estudiante, 
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public bool existe(string condicion)
        {
            bool result = false;
            SqlDataReader reader;
            EEstudiante est = new EEstudiante();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select estudianteId " +
                "from Estudiantes where " +
                condicion;
            SqlCommand comando = new SqlCommand(sentencia, conexion);
         
            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if(reader.HasRows)
                {
                    result = true;
                }
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se pudo realizar búsqueda de estudiante");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;
        }
        /// <summary>
        /// Verifica que un estudiante exista, basándose un un Id de estudiante,
        /// devuelve una cadena con su nombre completo
        /// </summary>
        /// <param name="estudianteId"></param>
        /// <returns></returns>
        public string existe(int estudianteId)
        {
            string result = "";
            SqlDataReader reader;
            EEstudiante est = new EEstudiante();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select nombre + ' ' + apellido1 + ' ' + apellido2 as nombre " +
                $"from Estudiantes where estudianteId = {estudianteId}";
                
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result = reader.GetString(0);
                }
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se pudo realizar búsqueda de estudiante");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;
        }
        /// <summary>
        /// Lista los estudiantes que componen una sección, basado en una sección tipo string
        /// devuelve un DataTable
        /// </summary>
        /// <param name="seccion"></param>
        /// <returns></returns>
        public DataTable listarPorSeccion(string seccion)
        {
            DataTable datos = new DataTable();
            SqlDataAdapter adapter;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select estudianteId, carnet, numIdentificacion as cedula," +
                " nombre + ' ' + apellido1 + ' ' + apellido2 as nombre, email" +
                $" From Estudiantes Where seccion = '{seccion}'";
           

            try
            {
                adapter = new SqlDataAdapter(sentencia, conexion);
                adapter.Fill(datos);
                
            }
            catch (Exception)
            {
                throw new Exception("No se pudo realizar búsqueda de estudiantes");
            }
            
            return datos;
        }
    }
}
