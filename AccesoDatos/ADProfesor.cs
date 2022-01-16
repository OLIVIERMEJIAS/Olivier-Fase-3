using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class ADProfesor
    {
        public string CadConexion { get; set; }

        public ADProfesor()
        {
            CadConexion = "";
        }

        public ADProfesor(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Verifica los datos de acceso de sesión de un profesor, en base con un objeto
        /// EProfesor, devuelve un int de confirmación, si devuelve algo diferente a -1 el profesor
        /// es correcto y será su Id de empleaod, así mismo modifica atributos del objeto entrante
        /// </summary>
        /// <param name="prof"></param>
        /// <returns></returns>
        public int accesoUsuario(EProfesor prof)
        {
            int result = -1;
            SqlDataReader reader;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select empleadoId," +
                " nombre, apellido1, apellido2 " +
                "from Empleados where puesto =" +
                " @puesto and contrasena = @contrasena and nombreUsuario = @nombreU";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@puesto", prof.Puesto);
            comando.Parameters.AddWithValue("@contrasena", prof.Contrasena);
            comando.Parameters.AddWithValue("@nombreU", prof.NombreUsuario);
            
            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result = reader.GetInt32(0);
                    prof.Nombre = reader.GetString(1);
                    prof.Apellido1 = reader.GetString(2);
                    prof.Apellido2 = reader.GetString(3);
                }
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se pudo realizar conexión de acceso");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;
        }
        /// <summary>
        /// Devuelve el número de lecciones que está impartiendo un profesor, hay diferente
        /// números para cada profesor, pero lo que devuelve es la cantidad de registros existentes
        /// en la entidad detalles de horario, se base en un Id de profesor
        /// </summary>
        /// <param name="profId"></param>
        /// <returns></returns>
        public byte numLecciones(int profId)
        {
            byte result = 0;
            Object dato;
            string sentencia = $"Select Count(profesorId) From DetallesHorario Where profesorId = {@profId}";

            SqlConnection conexion = new SqlConnection(CadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                dato = comando.ExecuteScalar();
                if(dato != null)
                     result = byte.Parse(dato.ToString());
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("Se ha presentando un error con el acceso a datos");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }

            return result;
        }
        /// <summary>
        /// Accesde al primer profesor por los cuales una materia es impartida,
        /// devuelve un int con el Id del profesor
        /// </summary>
        /// <param name="mateId"></param>
        /// <returns></returns>
        public int accederAProfesor(byte mateId)
        {

            Object dato;
            int result = -1;
            string aux;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select profesorId From MateriasProfesores " +
                $"where materiaId = {mateId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            try
            {
                conexion.Open();
                dato = comando.ExecuteScalar();
                if (dato != null)
                {
                 
                    aux = dato.ToString();
                    result = int.Parse(aux);
                }
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se pudo realizar búsqueda de profesor");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;

        }

       
        /// <summary>
        /// Verifica si un profesor esta ocupado en un día específico, 
        /// con base en una hora de inicio y un Id de profesor, si devuelve "" 
        /// es que esta libre, si no devuelve una cadena con la hora de fnalización 
        /// de esas leciones que está impartiendo
        /// </summary>
        /// <param name="horaI"></param>
        /// <param name="dia"></param>
        /// <param name="profeId"></param>
        /// <returns></returns>
        public string disponibleHoraI(string horaI, char dia, int profeId)
        {

            string result = "";
            Object dato;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select horaFin From DetallesHorario Where " +
                $"profesorId = {profeId} and dia = '{dia}' and horaInicio = " +
                $"'{horaI}'";
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
