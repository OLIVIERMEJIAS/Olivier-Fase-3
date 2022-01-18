using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EDistrito
    {
        public int DistritoId { get; set; }
        public string Distrito { get; set; }
        public byte CantonId { get; set; }

        public EDistrito()
        {
            Distrito = "";
            DistritoId = 0;
            CantonId = 0;
        }

        public EDistrito(int disId, string dis, byte can)
        {
            Distrito = dis;
            DistritoId = disId;
            CantonId = can;
        }
    }
}
