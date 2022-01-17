using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

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
        /// <param name="seccion"></param>
        /// <returns></returns>
        public DataTable listar()
        {
            DataTable datos = new DataTable();
            SqlDataAdapter adapter;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select e.encargadoId, e.carnet, e.numIdentificacion as cedula," +
                " e.nombre + ' ' + e.apellido1 + ' ' + e.apellido2 as nombre, e.email," +
                "e.genero, e.fechaIngreso, e.fechaNacimiento, d.distrito, e.activo, e.borrado" +
                $" From Encargados e inner join distritos d On " +
                "e.distritoId = d.distritoId'";


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
    }
}
