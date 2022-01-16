using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace AccesoDatos
{
    public class ADAsistente
    {
        public string CadConexion { get; set; }

        public ADAsistente()
        {
            CadConexion = "";
        }

        public ADAsistente(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Verifica que la contraseña y el usuario del asistente existan, para iniciar sesión
        /// devuelve un boolean con confirmación
        /// </summary>
        /// <param name="asis"></param>
        /// <returns></returns>
        public bool accesoUsuario(EAsistente asis)
        {
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 from Empleados where puesto =" +
                " @puesto and contrasena = @contrasena and nombreUsuario = @nombreU";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@puesto", asis.Puesto);
            comando.Parameters.AddWithValue("@contrasena", asis.Contrasena);
            comando.Parameters.AddWithValue("@nombreU", asis.NombreUsuario);
            try
            {
                conexion.Open();
                if (comando.ExecuteScalar() != null)
                {
                    result = true;
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
    }
}
