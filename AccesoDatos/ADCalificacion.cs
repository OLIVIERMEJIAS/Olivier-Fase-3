using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace AccesoDatos
{
    public class ADCalificacion
    {
        public string CadConexion { get; set; }

        public ADCalificacion()
        {
            CadConexion = "";
        }

        public ADCalificacion(string cad)
        {
            CadConexion = cad;
        }

        public ECalificacion listar(int calificacionId)
        {
            ECalificacion cali = new ECalificacion();
            SqlDataReader datos;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select calificacionId, estudianteId, fechaIngreso," +
                " calificacion, estado, trimestreId" +
                $" From Calificaciones Where calificacionId = {calificacionId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                datos = comando.ExecuteReader();
                if (datos.HasRows)
                {
                    datos.Read();
                    cali.CalificacionId = datos.GetInt32(0);
                    cali.EstudianteID = datos.GetInt32(1);
                    cali.FechaIngreso = datos.GetDateTime(2).ToString();
                    cali.Calificacion = datos.GetDecimal(3);
                    cali.Estado = datos.GetString(4);
                    cali.TrimestreID = datos.GetByte(5);
                }
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se pudo realizar conexión de datos");
            }

            return cali;
        }


        public DataTable listarPorEstudiante(int estudianteId)
        {

            DataTable datos = new DataTable();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select c.calificacionId as calificacionId," +
                " m.nombre as materia, e.nombre + ' ' + e.apellido1 + ' ' +" +
                " e.apellido2 as nombre, c.fechaIngreso as fecha, " +
                " c.calificacion as calificacion, c.estado as estado," +
                " c.trimestreId as trimestre" +
                " From Calificaciones c inner join Estudiantes e On e.estudianteId" +
                " = c.estudianteId inner join materias m On c.materiaId = m.materiaId" +
                $" Where c.estudianteId = {estudianteId}";
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



        public bool agregar(ECalificacion cali)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Insert Into Calificaciones(estudianteId," +
                "materiaId, fechaIngreso, calificacion, estado, trimestreId) " +
                "Values(@estId,@matId,getdate(),@cali, @estado, @trimestre)";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@estId", cali.EstudianteID);
            comando.Parameters.AddWithValue("@matId", cali.MateriaID);
            comando.Parameters.AddWithValue("@cali", cali.Calificacion);
            comando.Parameters.AddWithValue("@estado", cali.Estado);
            comando.Parameters.AddWithValue("@trimestre", cali.TrimestreID);

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
                throw new Exception("No se pudo realizar conexión de datos, o la asistencia del día de hoy ya existe");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;
        }

        public bool actualizar(ECalificacion cali)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Update Calificaciones Set calificacion = @cali," +
                " estado = @estado " +
                $"Where calificacionId = {cali.CalificacionId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@cali", cali.Calificacion);
            comando.Parameters.AddWithValue("@estado", cali.Estado);
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

        public bool eliminar(int calificacionId)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Delete From Calificaciones " +
                $"Where calificacionId = {calificacionId}";
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
                throw new Exception("No se pudo realizar conexión de datos o no se puede borrar, ya que hay permisos de cambio de calificación asociados");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;
        }

        public bool existe(int calificacionId)
        {
            Object scalar;
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 From Calificaciones " +
                $"Where calificacionId = {calificacionId}";
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
