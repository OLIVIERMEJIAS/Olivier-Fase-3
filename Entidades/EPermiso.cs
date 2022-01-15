using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
	public class EPermiso
	{
		public int PermisoId { get; set; }
		public int ProfesorId { get; set; }
		public byte MateriaId { get; set; }
		public int EstudianteId { get; set; }
		public int CalificacionId { get; set; }
		public decimal NotaActual { get; set; }
		public decimal NotaReemplazo { get; set; }
		public string EstadoCalificacionActual { get; set; }
		public string EstadoCalificacionReemplazo { get; set; }
		public string Fecha { get; set; }
		public char EstadoPermiso { get; set; }
		public string Motivo { get; set; }

		public EPermiso()
		{
			PermisoId = 0;
			ProfesorId = 0;
			MateriaId = 0;
			EstudianteId = 0;
			CalificacionId = 0;
			NotaActual = 0;
			NotaReemplazo = 0;
			EstadoCalificacionActual = "";
			EstadoCalificacionReemplazo = "";
			Fecha = "";
			EstadoPermiso = ' ';
			Motivo = "";
		}

		public EPermiso(int perm, int profe, byte materia,
			int estud, int cali,decimal notaA, decimal notaR,
			string estaA, string estadoR,
			string fech, char estadoP, string moti)
		{
			PermisoId = perm;
			ProfesorId = profe;
			MateriaId = materia;
			EstudianteId = estud;
			CalificacionId = cali;
			NotaActual = notaA;
			NotaReemplazo = notaR;
			EstadoCalificacionActual = estaA;
			EstadoCalificacionReemplazo = estadoR;
			Fecha = fech;
			EstadoPermiso = estadoP;
			Motivo = moti;
		}
	}
}
