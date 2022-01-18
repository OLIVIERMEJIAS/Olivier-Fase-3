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
        /// <summary>
        /// Lista los datos completos de los estudiantes 
        /// de una sección, devuelve un DataTable
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="datosCompletos"></param>
        /// <returns></returns>
        public DataTable listarPorSeccion(string seccion, bool datosCompletos)
        {
            DataTable datos = new DataTable();
            SqlDataAdapter adapter;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select e.estudianteId, e.carnet, e.numIdentificacion as cedula," +
                " e.nombre + ' ' + e.apellido1 + ' ' + e.apellido2 as nombre, e.email," +
                "e.genero, e.fechaIngreso, e.fechaNacimiento, d.distrito, " +
                "e.dirExact, e.activo, e.borrado" +
                $" From Estudiantes e inner join distritos d On " +
                $"e.distritoId = d.distritoId Where seccion = '{seccion}'";


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
        /// Lista todos los detalles de un estudiante,
        /// basado en el Id, devuelve un objeto EEstudiante
        /// </summary>
        /// <param name="estId"></param>
        /// <returns></returns>
        public EEstudiante listarDetallesPorEstudiante(int estId)
        {
            EEstudiante est = new EEstudiante();
            SqlDataReader reader;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select estudianteId, carnet, numIdentificacion, " +
                " seccion, nombre, apellido1, apellido2, email," +
                "genero, fechaIngreso, fechaNacimiento, distritoId, " +
                "dirExact, activo, borrado" +
                $" From Estudiantes Where estudianteId = {estId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    est.Id = reader.GetInt32(0);
                    est.Carnet = reader.GetString(1);
                    est.NumIdentificacion = reader.GetInt64(2);
                    est.Seccion = reader.GetString(3);
                    est.Nombre = reader.GetString(4);
                    est.Apellido1 = reader.GetString(5);
                    est.Apellido2 = reader.GetString(6);
                    est.Email = reader.GetString(7);
                    est.Genero = reader.GetChar(8);
                    est.FechaIngreso = reader.GetDateTime(9).Date;
                    est.FechaNacimiento = reader.GetDateTime(10).Date;
                    est.Distrito = reader.GetInt32(11);
                    est.DirExact = reader.GetString(12);
                    est.Activo = reader.GetBoolean(13);
                    est.Borrado = reader.GetBoolean(14);
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
                //throw new Exception("No se pudo realizar búsqueda de estudiantes");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }

            return est;
        }
        /// <summary>
        /// Inserta un estudiante basado en un objeto tipo EEstudiante, devuelve
        /// un boolean de confirmación
        /// </summary>
        /// <param name="est"></param>
        /// <returns></returns>
        public bool agregar(EEstudiante est)
        {
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Insert Into Estudiantes " +
                "Values(@carnet, @numIdent, @seccion, @nombre," +
                "@ape1, @ape2, @email, @fechaIngreso, @fechaNacimiento," +
                "@distId, @dirExact, @activo, @borrado";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@carnet", est.Carnet);
            comando.Parameters.AddWithValue("@numIdent", est.NumIdentificacion);
            comando.Parameters.AddWithValue("@seccion", est.Seccion);
            comando.Parameters.AddWithValue("@nombre", est.Nombre);
            comando.Parameters.AddWithValue("@ape1", est.Apellido1);
            comando.Parameters.AddWithValue("@ape2", est.Apellido2);
            comando.Parameters.AddWithValue("@email", est.Email);
            comando.Parameters.AddWithValue("@fechaIngreso", est.FechaIngreso);
            comando.Parameters.AddWithValue("@fechaNacimiento", est.FechaNacimiento);
            comando.Parameters.AddWithValue("@distId", est.Distrito);
            comando.Parameters.AddWithValue("@dirExact", est.DirExact);
            comando.Parameters.AddWithValue("@activo", est.Activo);
            comando.Parameters.AddWithValue("@borrado", est.Borrado);

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
        /// Actualiza un estudiante basado en un objeto EEstudiante
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="est"></param>
        /// <returns></returns>
        public bool actualizar(EEstudiante est)
        {
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Update Estudiantes " +
                "Set carnet = @carnet, numIdentificacion = @numIdent, " +
                "seccion = @seccion, nombre = @nombre," +
                "@apellido1 = ape1, apellido2 = @ape2, " +
                "email = @email, fechaIngreso = @fechaIngreso, " +
                "fechaNacimiento = @fechaNacimiento," +
                "distritoId = @distId, dirExact = @dirExact, " +
                "activo = @activo, borrado = @borrado" +
                 $" Where estudianteId = {est.Id}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@carnet", est.Carnet);
            comando.Parameters.AddWithValue("@numIdent", est.NumIdentificacion);
            comando.Parameters.AddWithValue("@seccion", est.Seccion);
            comando.Parameters.AddWithValue("@nombre", est.Nombre);
            comando.Parameters.AddWithValue("@ape1", est.Apellido1);
            comando.Parameters.AddWithValue("@ape2", est.Apellido2);
            comando.Parameters.AddWithValue("@email", est.Email);
            comando.Parameters.AddWithValue("@fechaIngreso", est.FechaIngreso);
            comando.Parameters.AddWithValue("@fechaNacimiento", est.FechaNacimiento);
            comando.Parameters.AddWithValue("@distId", est.Distrito);
            comando.Parameters.AddWithValue("@dirExact", est.DirExact);
            comando.Parameters.AddWithValue("@activo", est.Activo);
            comando.Parameters.AddWithValue("@borrado", est.Borrado);

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
        /// Se elimina un estudiante basado en su número de Id
        /// devuelve un boolean confirmando
        /// </summary>
        /// <param name="estId"></param>
        /// <returns></returns>
        public bool eliminar(int estId)
        {
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Delete from Estudiantes" +
                $" Where estudianteId = {estId}";
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
        /// Verifica que exista un número de cédula
        /// devuelve un boolean para confirmar
        /// </summary>
        /// <param name="numIdentn"></param>
        /// <returns></returns>
        public bool existeCedula(long numIdent)
        {
            bool result = false;
            SqlDataReader reader;
            EEstudiante est = new EEstudiante();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 " +
                "from Estudiantes where numIdentificacion = " +
                $"{numIdent}";
                
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
        /// <summary>
        /// Verifica la existe del carnet de estudiante
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool existeCarnet(string car)
        {
            bool result = false;
            SqlDataReader reader;
            EEstudiante est = new EEstudiante();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 " +
                "from Estudiantes where carnet = " +
                $"{car}";

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
        /// <summary>
        /// Verifica que un email exista
        /// devuelve un boolean como resultado
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool existeEmail(string email)
        {
            bool result = false;
            SqlDataReader reader;
            EEstudiante est = new EEstudiante();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 " +
                $"from Estudiantes where email = '{email}'";
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
