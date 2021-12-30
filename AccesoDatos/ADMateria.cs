using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace AccesoDatos
{
    public class ADMateria
    {
        public string CadConexion { get; set; }

        public ADMateria()
        {
            CadConexion = "";
        }

        public ADMateria(string cad)
        {
            CadConexion = cad;
        }

        public EMateria accederAMateria(int profeId)
        {
     
            SqlDataReader reader;
            EMateria mat = new EMateria();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select m.nombre,m.materiaId from Materias m inner join " +
                "MateriasProfesores mp On m.materiaId = mp.materiaId inner join Empleados e " +
                "On mp.profesorId = e.empleadoId Where e.empleadoId = @profesId";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@profesId", profeId);
            
            conexion.Close();
                
                try
                {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    mat.Nombre = reader.GetString(0);
                    mat.MateriaId = reader.GetByte(1);
                }

                }
                catch (Exception)
                {
                    conexion.Close();
                    throw new Exception("No se pudo realizar búsqueda de materia");
                }
                finally
                {
                    conexion.Dispose();
                    comando.Dispose();
                }
                return mat;

        }


    }
}
