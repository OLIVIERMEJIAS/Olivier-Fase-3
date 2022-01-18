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
        /// Valida que exista un carnet de estudiante
        /// devuelve un boolean de resultado
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool existeCarnet(string car)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);

            try
            {
                return ade.existeCarnet(car);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Verifica si una cédula existe
        /// devuelve un boolean como resultado
        /// </summary>
        /// <param name="numIdent"></param>
        /// <returns></returns>
        public bool existeCedula(long numIdent)
        { 
            ADEstudiante ade = new ADEstudiante(CadConexion);
            try
            {
                return ade.existeCedula(numIdent);
            }
            catch (Exception ex)
            { 

                throw ex;
            }

        }

        

        /// <summary>
        /// Verifica si un email existe
        /// +devuelve un boolean como resultado
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool existeEmail(string email)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);
            try
            {
                return ade.existeEmail(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
        /// <summary>
        /// Lista todos los detalles de un estudiante,
        /// basado en su Id, devuelve un objeto EEstudiante
        /// </summary>
        /// <param name="estId"></param>
        /// <returns></returns>
        public EEstudiante listarDetallesPorEstudiante(int estId)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);
            try
            {
                return ade.listarDetallesPorEstudiante(estId);
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }
        /// <summary>
        /// Inserta un registro de estudiante, basado en un objeto EEstudiante
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <param name="est"></param>
        /// <returns></returns>
        public bool agregar(EEstudiante est)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);

            try
            {
                return ade.agregar(est);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Actualiza un registro de estudiante, basado en un 
        /// objeto EEstudiante, devuelve un boolean de confirmación
        /// </summary>
        /// <param name="est"></param>
        /// <returns></returns>
        public bool actualizar(EEstudiante est)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);

            try
            {
                return ade.actualizar(est);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Elimina un registro de estudiante,
        /// basado en su Id, devuelve un boolean confirmando
        /// </summary>
        /// <param name="estId"></param>
        /// <returns></returns>
        public bool eliminar(int estId)
        {
            ADEstudiante ade = new ADEstudiante(CadConexion);

            try
            {
                return ade.eliminar(estId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
