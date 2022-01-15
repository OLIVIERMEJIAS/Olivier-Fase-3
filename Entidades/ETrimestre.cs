using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ETrimestre
    {
        public byte TrimestreId { get; set; }
        public string Numero { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public ETrimestre()
        {
            TrimestreId = 0;
            Numero = "";
            FechaInicio = new DateTime();
            FechaFin = new DateTime();
        }

        public ETrimestre(byte trim, string num,
            DateTime fechaI, DateTime fechaFi)
        {
            TrimestreId = trim;
            Numero = num;
            FechaInicio = fechaI;
            FechaFin = fechaFi;
        }
    }
}
