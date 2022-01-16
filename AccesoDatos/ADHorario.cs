using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class ADHorario
    {
        public string CadConexion { get; set; }

        public ADHorario()
        {
            CadConexion = "";
        }

        public ADHorario(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Lista detalles vitales para mostrar el horario de una sección en un 
        /// día específico, devuelve un DataTable con los resultados
        /// </summary>
        /// <param name="sec"></param>
        /// <param name="dia"></param>
        /// <returns></returns>
        public DataTable horarioPorDiaYSeccion(string sec, char dia)
        {

            DataTable datos = new DataTable();
            SqlDataAdapter adaptador;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select d.horaInicio as HoraInicio, d.horaFin as HoraFin, " +
            "a.nombre + ': ' + m.nombre + ': ' + e.nombre + ' ' + e.apellido1 as Detalle " +
            "From DetallesHorario d inner join Aulas a On d.aulaId = a.aulaId " +
            "inner join Empleados e On e.empleadoId = d.profesorId " +
            "inner join MateriasProfesores mp On mp.profesorId = e.empleadoId " +
            "inner join Materias m On m.materiaId = mp.materiaId " +
            "inner join Horarios h On d.horarioId = h.horarioId " +
            $"Where d.dia = '{dia}' and h.seccion = '{sec}'";
            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(datos);
                
            }
            catch (Exception)
            {
                throw new Exception("No se pudo realizar búsqueda de estudiante");
            }
            return datos;
        }
        /// <summary>
        /// Lista las secciones existentes, asociadoas a horarios,
        /// devuelve u DatatTable con los resultados
        /// </summary>
        /// <returns></returns>
        public DataTable secciones()
        {

            DataTable datos = new DataTable();
            SqlDataAdapter adaptador;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select seccion From Horarios Order by horarioId";
            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(datos);

            }
            catch (Exception)
            {
                throw new Exception("No se pudo realizar búsqueda de estudiante");
            }
            return datos;
        }
        /// <summary>
        /// Lista las secciones que imparte un profesor, según los horarios,
        /// devuelve un DataTable según resultados
        /// </summary>
        /// <param name="profesorId"></param>
        /// <returns></returns>
        public DataTable secciones(int profesorId)
        {

            DataTable datos = new DataTable();
            SqlDataAdapter adaptador;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select h.seccion as seccion" +
                " From Horarios h inner join DetallesHorario d On" +
                $" h.horarioId = d.horarioId  Where d.profesorId = {profesorId}" +
                " Order by h.horarioId";
            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(datos);

            }
            catch (Exception)
            {
                throw new Exception("No se pudo realizar búsqueda de estudiante");
            }
            return datos;
        }
    }
}
