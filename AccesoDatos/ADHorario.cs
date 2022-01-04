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
    }
}
