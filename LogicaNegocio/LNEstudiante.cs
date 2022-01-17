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
        /// <summary>
        /// Lista datos completos de estudiantes por sección 
        /// devuelve un DataTable con los resultados
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="datosCompletos"></param>
        /// <returns></returns>
        public DataTable listarPorSeccion(string seccion, bool datosCompletos)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);
            try
            {
                return ade.listarPorSeccion(seccion, datosCompletos);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Valida si un estudiante existe en base a su Id,
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Lista los estudiantes que existan en una sección, 
        /// los devuelve en un DataTable 
        /// </summary>
        /// <param name="seccion"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Verifica que exista un estudiante con su Id, 
        /// devuelve una cadena con su nombre completo de encontrarse
        /// </summary>
        /// <param name="estudianteId"></param>
        /// <returns></returns>
        public string existe(int estudianteId)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);

            try
            {
                return ade.existe(estudianteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
