using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace AccesoDatos
{
    public class ADEncargado
    {
        public string CadConexion { get; set; }

        public ADEncargado()
        {
            CadConexion = "";
        }

        public ADEncargado(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Lista todos los encargados
        /// devuelve un DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable listar(string condicion = "")
        {
            DataTable datos = new DataTable();
            SqlDataAdapter adapter;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select e.encargadoId, e.numIdentificacion as cedula," +
                " e.nombre + ' ' + e.apellido1 + ' ' + e.apellido2 as nombre, e.email," +
                "e.genero, e.fechaIngreso, e.fechaNacimiento, d.distrito, e.activo, e.borrado" +
                $" From Encargados e inner join distritos d On " +
                "e.distritoId = d.distritoId";
            if (condicion != "")
                sentencia = $"{sentencia} where e.nombre like '%{condicion}%'"; 

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
        /// <summary>
        /// Lista encargados por estudiante,
        /// devuelve un DataTable
        /// </summary>
        /// <param name="estuId"></param>
        /// <returns></returns>
        public DataTable listarPorEstudiante(int estuId)
        {
            DataTable datos = new DataTable();
            SqlDataAdapter adapter;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select e.encargadoId, e.numIdentificacion as cedula," +
                " e.nombre + ' ' + e.apellido1 + ' ' + e.apellido2 as nombre, e.email," +
                "e.genero, e.fechaIngreso, e.fechaNacimiento, d.distrito, e.activo, e.borrado" +
                $" From Encargados e inner join distritos d On " +
                "e.distritoId = d.distritoId" +
                " inner join EncargadosEstudiantes ee" +
                " on ee.encargadoId = e.encargadoId" +
                $" where ee.estudianteId = {estuId}";
            
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

        /// <summary>
        /// Lista todos los detalles de un encargado,
        /// basado en el Id, devuelve un objeto EPersona
        /// </summary>
        /// <param name="encId"></param>
        /// <returns></returns>
        public EPersona listarDetallesPorEncargado(int encId)
        {
            EPersona per = new EPersona();
            SqlDataReader reader;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select encargadoId, numIdentificacion" +
                " nombre, apellido1, apellido2, email," +
                "genero, fechaIngreso, fechaNacimiento, distritoId, " +
                "dirExact, activo, borrado" +
                $" From Encargados Where encargadoId = '{encId}'";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    per.PersonaId = reader.GetInt32(0);
                    per.NumIdentificacion = reader.GetInt64(1);
                    per.Nombre = reader.GetString(2);
                    per.Apellido1 = reader.GetString(3);
                    per.Apellido2 = reader.GetString(4);
                    per.Email = reader.GetString(5);
                    per.Genero = reader.GetChar(7);
                    per.FechaIngreso = reader.GetDateTime(8);
                    per.FechaNacimiento = reader.GetDateTime(9);
                    per.Distrito = reader.GetInt32(10);
                    per.DirExact = reader.GetString(11);
                    per.Activo = reader.GetBoolean(12);
                    per.Borrado = reader.GetBoolean(14);
                }

            }
            catch (Exception)
            {
                throw new Exception("No se pudo realizar búsqueda de estudiantes");
            }

            return per;
        }
        /// <summary>
        /// Inserta un encargado basado en un objeto tipo EPersona, devuelve
        /// un boolean de confirmación
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public bool agregar(EPersona per)
        {
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Insert Into Encargados " +
                "Values(@numIdent,@nombre," +
                "@ape1, @ape2, @email, @fechaIngreso, @fechaNacimiento," +
                "@distId, @dirExact, @activo, @borrado";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@numIdent", per.NumIdentificacion);
            comando.Parameters.AddWithValue("@nombre", per.Nombre);
            comando.Parameters.AddWithValue("@ape1", per.Apellido1);
            comando.Parameters.AddWithValue("@ape2", per.Apellido2);
            comando.Parameters.AddWithValue("@email", per.Email);
            comando.Parameters.AddWithValue("@fechaIngreso", per.FechaIngreso);
            comando.Parameters.AddWithValue("@fechaNacimiento", per.FechaNacimiento);
            comando.Parameters.AddWithValue("@distId", per.Distrito);
            comando.Parameters.AddWithValue("@dirExact", per.DirExact);
            comando.Parameters.AddWithValue("@activo", per.Activo);
            comando.Parameters.AddWithValue("@borrado", per.Borrado);

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
        /// Actualiza un encargado basado en un objeto EPersona
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public bool actualizar(EPersona per)
        {
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Update Encargados " +
                "Set numeroIdentificacion = @numIdent, " +
                "nombre = @nombre," +
                "@apellido1 = ape1, apellido2 = @ape2, " +
                "email = @email, fechaIngreso = @fechaIngreso, " +
                "fechaNacimiento = @fechaNacimiento," +
                "distritoId = @distId, dirExact = @dirExact, " +
                "activo = @activo, borrado = @borrado" +
                 $" Where encargadoId = {per.PersonaId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@numIdent", per.NumIdentificacion);
            comando.Parameters.AddWithValue("@nombre", per.Nombre);
            comando.Parameters.AddWithValue("@ape1", per.Apellido1);
            comando.Parameters.AddWithValue("@ape2", per.Apellido2);
            comando.Parameters.AddWithValue("@email", per.Email);
            comando.Parameters.AddWithValue("@fechaIngreso", per.FechaIngreso);
            comando.Parameters.AddWithValue("@fechaNacimiento", per.FechaNacimiento);
            comando.Parameters.AddWithValue("@distId", per.Distrito);
            comando.Parameters.AddWithValue("@dirExact", per.DirExact);
            comando.Parameters.AddWithValue("@activo", per.Activo);
            comando.Parameters.AddWithValue("@borrado", per.Borrado);

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
        /// Se elimina un encargado basado en su número de Id
        /// devuelve un boolean confirmando
        /// </summary>
        /// <param name="estId"></param>
        /// <returns></returns>
        public bool eliminar(int encId)
        {
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Delete from Encargados" +
                $" Where encargadoId = {encId}";
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
        /// Verifica que un encargado exista, 
        /// basándose un un Id de encargado,
        /// devuelve boolean para confirmar
        /// </summary>
        /// <param name="estudianteId"></param>
        /// <returns></returns>
        public bool existe(int encId)
        {
            bool result = false;
            SqlDataReader reader;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 " +
                $"from Encargados where encargadoId = {encId}";

            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.HasRows)
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
    }
}
