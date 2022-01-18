using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace AccesoDatos
{
    public class ADDistrito
    {
        public string CadConexion { get; set; }

        public ADDistrito()
        {
            CadConexion = "";
        }

        public ADDistrito(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Lista todos los distritos y los devuelve en un DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable listar()
        {

            DataTable datos = new DataTable();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select d.distritoId, d.distrito, c.canton From Distritos d" +
                " inner join Cantones c on d.cantonId = c.cantonId";
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
        /// Genera un objeto EDistrito con el nombre del distrito
        /// basado en el Id de un distrito
        /// </summary>
        /// <param name="disId"></param>
        /// <returns></returns>
        public EDistrito nombre(int disId)
        {
            EDistrito dis = new EDistrito();
            SqlDataReader reader;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select distrito From Distritos" +
                $" Where distritoId = {disId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    dis.Distrito = reader.GetString(0);
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

            return dis;
        }
    }
}
