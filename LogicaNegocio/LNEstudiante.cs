using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using Entidades;
using System.Data;

namespace LogicaNegocio
{
   public class LNEstudiante
    {
        public string CadConexion { get; set; }

        public LNEstudiante()
        {
            CadConexion = "";
        }

        public LNEstudiante(string cad)
        {
            CadConexion = cad;
        }
        public bool existe(string condicion)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);
            try
            {
                return ade.existe(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable listarPorSeccion(string seccion)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);
            try
            {
                return ade.listarPorSeccion(seccion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
