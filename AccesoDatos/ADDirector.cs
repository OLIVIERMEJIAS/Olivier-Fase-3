using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
namespace AccesoDatos
{
    public class ADDirector
    {
        public string CadConexion { get; set; }

        public ADDirector()
        {
            CadConexion = "";
        }

        public ADDirector(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Confirma que los datos de acceso de sesión sean correctos,
        /// basdo en un objeto EDirector, devuelve un boolean confirmando
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public bool accesoUsuario(EDirector dir)
        {
            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select 1 from Empleados where puesto =" +
                " @puesto and contrasena = @contrasena and nombreUsuario = @nombreU";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@puesto", dir.Puesto);
            comando.Parameters.AddWithValue("@contrasena", dir.Contrasena);
            comando.Parameters.AddWithValue("@nombreU", dir.NombreUsuario);
            try
            {
                conexion.Open();
                if(comando.ExecuteScalar() != null)
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
