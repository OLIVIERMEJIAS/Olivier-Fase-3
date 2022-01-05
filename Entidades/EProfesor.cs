using System;
using System.Collections.Generic;
using System.Text;


namespace Entidades
{
    public class EProfesor : EPersona
    {
        public char Puesto { get; set; }
        
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public byte Materia { set; get; }

        public EProfesor():base()
        {
            Puesto = ' ';
            NombreUsuario = "";
            Contrasena = "";
            Materia = 0;
        }

        public EProfesor(char puest, string nomU,
            string contra, byte mat):base()
        {
            Puesto = puest;
            NombreUsuario = nomU;
            Contrasena = contra;
            Materia = mat;
        }
        public EProfesor(char puest, string nomU,
           string contra) : base()
        {
            Puesto = puest;
            NombreUsuario = nomU;
            Contrasena = contra;
            
        }


        
    }
}
