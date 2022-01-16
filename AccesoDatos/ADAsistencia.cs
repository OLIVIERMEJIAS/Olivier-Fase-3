using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class ADAsistencia
    {
        public string CadConexion { get; set; }

        public ADAsistencia()
        {
            CadConexion = "";
        }

        public ADAsistencia(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Recopila datos importantes de una asistencia basado en un Id de asistencia
        /// devuelve un objeto EAsistencia
        /// </summary>
        /// <param name="asistenciaId"></param>
        /// <returns></returns>
        public EAsistencia listar(int asistenciaId)
        {
            EAsistencia asist = new EAsistencia();
            SqlDataReader datos;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select asistenciaId, estudianteId, fecha, estado" +
                $" From Asistencias Where asistenciaId = {asistenciaId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                datos = comando.ExecuteReader();
                if (datos.HasRows)
                {
                    datos.Read();
                    asist.AsistenciaId = datos.GetInt32(0);
                    asist.EstudianteId = datos.GetInt32(1);
                    asist.FechaHora = datos.GetDateTime(2).ToString();
                    asist.Estado = datos.GetString(3);
                }
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se pudo realizar conexión de datos");
            }

            return asist;
        }

        /// <summary>
        /// Lista todas las asistecnias de un estudiante, basándose en un Id estudiante
        /// devuelve un DataTable
        /// </summary>
        /// <param name="estudianteId"></param>
        /// <returns></returns>
        public DataTable listarPorEstudiante(int estudianteId)
        {

            DataTable datos = new DataTable();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select a.asistenciaId as asistenciaId," +
                " m.nombre as materia, e.nombre + ' ' + e.apellido1 + ' ' +" +
                " e.apellido2 as nombre, a.fecha as fecha, a.estado as estado" +
                " From Asistencias a inner join Estudiantes e On e.estudianteId" +
                " = a.estudianteId inner join materias m On a.materiaId = m.materiaId" +
                $" Where a.estudianteId = {estudianteId}";
            SqlDataAdapter adaptador = new SqlDataAdapter(sentencia, conexion);

            try
            {
                adaptador.Fill(datos);
                adaptador.Dispose();
            }
            catch (Exception)
            {
                adaptador.Dispose();
                throw new Exception("No se pudo realizar conexión de datos");
            }

            return datos;
        }

        
        /// <summary>
        /// Inserta en la base de datos una asistencia, basándose en un objeto EAsistencia
        /// devuelve un boolean de confirmación de la tarea
        /// </summary>
        /// <param name="asist"></param>
        /// <returns></returns>
        public bool agregar(EAsistencia asist)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Insert Into Asistencias(estudianteId," +
                "materiaId, fecha, estado) Values(@estId,@matId,getdate(),@estado)";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@estId", asist.EstudianteId);
            comando.Parameters.AddWithValue("@matId", asist.MateriaId);
            comando.Parameters.AddWithValue("@estado", asist.Estado);

            try
            {
                conexion.Open();
                if (comando.ExecuteNonQuery() != 0)
                {
                    result = true;
                }
                
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
        /// <summary>
        /// Actualiza una asistencia, basándose en un objeto EAsistencia,
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <param name="asist"></param>
        /// <returns></returns>
        public bool actualizar(EAsistencia asist)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Update Asistencias Set estado = @estado " +
                $"Where asistenciaId = {asist.AsistenciaId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@estado", asist.Estado);
            try
            {
                conexion.Open();
                if (comando.ExecuteNonQuery() != 0)
                {
                    result = true;
                }

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
        /// <summary>
        /// Elimina una asistencia, basándose en un Id de asistencia,
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="asistenciaId"></param>
        /// <returns></returns>
        public bool eliminar(int asistenciaId)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Delete From Asistencias " +
                $"Where asistenciaId = {asistenciaId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
       

            try
            {
                
                conexion.Open();
                if (comando.ExecuteNonQuery() != 0)
                {
                    result = true;
                }
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
        /// <summary>
        /// Verifica si existe una asistencia, basándose en un Id de asistencia,
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="asistenciaId"></param>
        /// <returns></returns>
        public bool existe(int asistenciaId)
        {
            Object scalar;
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 From Asistencias " +
                $"Where asistenciaId = {asistenciaId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);


            try
            {

                conexion.Open();
                scalar = comando.ExecuteScalar();
                if (scalar != null)
                {
                    result = true;
                }
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
