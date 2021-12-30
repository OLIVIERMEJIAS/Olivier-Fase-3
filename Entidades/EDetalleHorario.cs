using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EDetalleHorario
    {
        public byte HorarioId { get; set; }
        public byte ProfesorID { get; set; }
        public byte AulaID { get; set; }
        public char Dia { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }

        public EDetalleHorario()
        {
            HorarioId = 0;
            ProfesorID = 0;
            AulaID = 0;
            Dia = ' ';
            HoraInicio = "";
            HoraFin = "";
        }

        public EDetalleHorario(byte horarId,
            byte prof, byte aul, char diaSem,
            string horI, string horF)
        {
            HorarioId = horarId;
            ProfesorID = prof;
            AulaID = aul;
            Dia = diaSem;
            HoraInicio = horI;
            HoraFin = horF;
        }
    }
}
