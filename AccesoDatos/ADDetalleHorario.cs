using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace AccesoDatos
{
    public class ADDetalleHorario
    {
        public string CadConexion { get; set; }

        public ADDetalleHorario()
        {
            CadConexion = "";
        }

        public ADDetalleHorario(string cad)
        {
            CadConexion = cad;
        }

        /// <summary>
        /// Inserta un detalle de horario, en base a un objeto tipo EDetalleHorario,
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <param name="det"></param>
        /// <returns></returns>
        public bool agregar(EDetalleHorario det)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Insert Into DetallesHorario Values(@horId,@profId,@aulaId,@dia,@horI,@horF)";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@horId", det.HorarioId);
            comando.Parameters.AddWithValue("@profId", det.ProfesorID);
            comando.Parameters.AddWithValue("@aulaId", det.AulaID);
            comando.Parameters.AddWithValue("@dia", det.Dia);
            comando.Parameters.AddWithValue("@horI", det.HoraInicio);
            comando.Parameters.AddWithValue("@horF", det.HoraFin);

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

       

       
        /// <summary>
        /// Confirma si hay registros en la entidad de la base de datos de detalles de horario
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <returns></returns>
        public bool hayRegistros()
        {
            SqlDataReader reader;
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 from DetallesHorario";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
             
            try
            {
               conexion.Open();
                reader = comando.ExecuteReader();
                if ( reader.HasRows)
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

        /// <summary>
        /// Elimina todos los registros de la entidad detalles de horario, 
        /// ya que cuando se presiona el botón generar
        /// horarios, si existe deben ser cambiados desde cero
        /// </summary>
        /// <returns></returns>
        public bool eliminarRegistros()
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Delete From DetallesHorario";
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

        
        /// <summary>
        /// Conforma disponibilidad de un grupo, con base en hora de inciio, día y Id de horario
        /// devuelve un string vacío si esta libre a esa hora de inicio, devuelve un string con 
        /// una hora de finalización de lección si esta ocupada esa lección en ese momento
        /// </summary>
        /// <param name="horaI"></param>
        /// <param name="dia"></param>
        /// <param name="horId"></param>
        /// <returns></returns>
        public string disponibleHoraI(string horaI, char dia, int horId)
        {

            string result = "";
            Object dato;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select horaFin From DetallesHorario Where " +
                $"horaInicio = '{horaI}' and dia = '{dia}' and horarioId = " +
                $"'{horId}'";
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
