using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ECalificacion
    {
        public int CalificacionId { get; set; }
        public int EstudianteID { get; set; }
        public int MateriaID { get; set; }
        public string FechaIngreso { get; set; }
        public string Estado { get; set; }
        public decimal Calificacion { get; set; }
        public byte TrimestreID { get; set; }

        public ECalificacion()
        {
            CalificacionId = 0;
            EstudianteID = 0;
            MateriaID = 0;
            FechaIngreso = "";
            Estado = "";
            Calificacion = 0;
            TrimestreID = 0;
        }

        public ECalificacion(int cali, int estu,
             int mat, decimal calif, string estad
             , byte trim)
        {
            CalificacionId = cali;
            EstudianteID = estu;
            MateriaID = mat;
            Estado = estad;
            Calificacion = calif;
            TrimestreID = trim;
        }

        
    }
}
