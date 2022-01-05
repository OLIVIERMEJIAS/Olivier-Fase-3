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
        public int accesoUsuario(EProfesor prof)
        {
            int result = -1;
            SqlDataReader reader;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select empleadoId from Empleados where puesto =" +
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
                    result = reader.GetInt16(0);
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

        public DataTable listar()
        {
            DataTable datos = new DataTable();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select e.empleadoId, e.numIdentificacion," +
                " e.fechaIngreso, e.fechaNacimiento, m.nombre, e.nombre, " +
                "e.apellido1, e.apellido2, e.genero, e.email, e.nombreUsuario," +
                " e.contrasena, d.distrito, e.dirExact," +
                 " e.activo, e.borrado from Empleados e inner join MateriasProfesores mp" +
                 " On e.empleadoId = mp.profesorId inner join Materias m " +
                 "On mp.materiaId = m.materiaId inner join Distritos d " +
                 "On d.distritoId = e.distritoId";
            SqlDataAdapter adapter;
            try
            {
                adapter = new SqlDataAdapter(sentencia, conexion);
                adapter.Fill(datos);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return datos;
        }

        public DataTable listar(string condicion)
        {
            DataTable datos = new DataTable();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select e.empleadoId, e.numIdentificacion," +
                " e.fechaIngreso, e.fechaNacimiento, m.nombre, e.nombre, " +
                "e.apellido1, e.apellido2, e.genero, e.email, e.nombreUsuario," +
                " e.contrasena, d.distrito, e.dirExact," +
                 " e.activo, e.borrado from Empleados e inner join MateriasProfesores mp" +
                 " On e.empleadoId = mp.profesorId inner join Materias m " +
                 "On mp.materiaId = m.materiaId inner join Distritos d " +
                 "On d.distritoId = e.distritoId " +
                 $"where m.nombre = {condicion}";
            SqlDataAdapter adapter;
            try
            {
                adapter = new SqlDataAdapter(sentencia, conexion);
                adapter.Fill(datos);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return datos;
        }
    }
}
