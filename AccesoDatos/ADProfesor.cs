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
            string sentencia = "contarLecciones";

            SqlConnection conexion = new SqlConnection(CadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@profId", profId).Direction = ParameterDirection.Input;

            //DEFINIR QUE EL TIPO DE COMANDO A EJECUTAR ES UN STORE PROCEDURE
            comando.CommandType = CommandType.StoredProcedure;

            // ↓ Parametro de SALIDA
            comando.Parameters.Add("@cont", SqlDbType.TinyInt).Direction = ParameterDirection.InputOutput;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                result = byte.Parse(comando.Parameters["@cont"].Value.ToString());
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("Se ha presentando un error con el Procedimiento Almacenado");
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

            Object escalar;
            int result = -1;
            EMateria mat = new EMateria();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select profesorId From MateriasProfesores mp " +
                $"where materiaId = {mateId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            conexion.Close();

            try
            {
                conexion.Open();
                escalar = comando.ExecuteScalar();
                if (escalar != null)
                {
                    result = int.Parse(escalar.ToString());
                }

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
    }
}
