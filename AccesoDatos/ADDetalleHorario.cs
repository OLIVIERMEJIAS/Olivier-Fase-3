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

        public bool buscarHorEspecialidadSegDia(int horId, int profeId, ref string horI,
           ref byte limInfAula, ref char diaSem)
        {
            DataTable datos = new DataTable();
            SqlDataAdapter adaptador;
            bool result = false;
            string aula;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select horaInicio, aulaId, dia from DetallesHorario" +
                $" where profesorId = {profeId} and horarioId = {horId}";

            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(datos);
                if (datos != null)
                {

                    horI = datos.Rows[1][0].ToString();
                    aula = datos.Rows[1][1].ToString();
                    diaSem = char.Parse(datos.Rows[1][2].ToString());
                    limInfAula = byte.Parse(aula);
                    result = true;
                }

            }
            catch (Exception)
            {

                throw new Exception("No se pudo realizar conexión de datos");
            }

            return result;
        }

        public bool buscarHorEspecialidadPrimerDia(int horId, int profeId, ref string horI, 
            ref byte limInfAula, ref char diaSem)
        {
            DataTable datos = new DataTable();
            SqlDataAdapter adaptador;
            bool result = false;
            string aula;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select horaInicio, aulaId, dia from DetallesHorario" +
                $" where profesorId = {profeId} and horarioId = {horId}";

            try
            {
               adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(datos);
                if (datos.Rows.Count != 0)
                {

                    horI = datos.Rows[0][0].ToString();
                    aula = datos.Rows[0][1].ToString();
                    diaSem = char.Parse(datos.Rows[0][2].ToString());
                    limInfAula = byte.Parse(aula);
                    result = true;
                }
                
            }
            catch (Exception)
            {
            
                throw new Exception("No se pudo realizar conexión de datos");
            }
            
            return result;
        }

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

        public DataTable listarPorSeccion(string sec)
        {

            DataTable datos = new DataTable();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select d.dia, m.materia, d.horaInicio, d.horaFin " +
                "from Horarios h inner join DetallesHorario d On h.horarioId = d.horarioId " +
                "inner join MateriasProfesores mp " +
                "On d.profesorId =  mp.profesorId inner join Materias m " +
                "On mp.materiaId = m.materiaId Where";
            sentencia = $"{sentencia} h.seccion = '{sec}'";
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
