using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EPersona
    {
        public int PersonaId { get; set; }
        public long NumIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public char Genero { get; set; }
        public string Email { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Distrito { get; set; }
        public string DirExact { get; set; }
        public bool Activo { get; set; }
        public bool Borrado { get; set; }

        public EPersona()
        {
            PersonaId = 0;
            NumIdentificacion = 0;
            Nombre = "";
            Apellido1 = "";
            Apellido2 = "";
            Genero = ' ';
            Email = "";
            FechaIngreso = new DateTime();
            FechaNacimiento = new DateTime();
            Distrito = 0;
            DirExact = "";
            Activo = true;
            Borrado = false;
        }

        public EPersona(int id, int ident, string nom,
            string ape1, string ape2, char gen, string email,
            DateTime fechIng, DateTime fechNac, int dist,
            string dirExact, bool act, bool bor)
        {
            PersonaId = id;
            NumIdentificacion = ident;
            Nombre = nom;
            Apellido1 = ape1;
            Apellido2 = ape2;
            Genero = gen;
            Email = email;
            FechaIngreso = fechIng;
            FechaNacimiento = fechNac;
            Distrito = dist;
            DirExact = dirExact;
            Activo = act;
            Borrado = bor;
        }

        





    }
}
