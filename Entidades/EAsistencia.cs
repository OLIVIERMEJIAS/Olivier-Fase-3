using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
   public class EAsistencia
    {
        public int AsistenciaId { get; set; }
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }
        public string FechaHora { get; set; }
        public string Estado { get; set; }

        public EAsistencia()
        {
            AsistenciaId = 0;
            EstudianteId = 0;
            MateriaId = 0;
            FechaHora = "";
            Estado = "";
        }

        public EAsistencia(int asistId, int estu,
            int mat, string estad)
        {
            AsistenciaId = asistId;
            EstudianteId = estu;
            MateriaId = mat;
            FechaHora = "";
            Estado = estad;
        }

        
    }
}
