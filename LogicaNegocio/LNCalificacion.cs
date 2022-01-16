using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class LNCalificacion
    {
        public string CadConexion { get; set; }

        public LNCalificacion()
        {
            CadConexion = "";
        }

        public LNCalificacion(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Verfica si existen permisos de cambio de calificación
        /// asociados a está calificación,
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="caliId"></param>
        /// <returns></returns>
        public bool asociados(int caliId)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion);

            try
            {
                return adC.asociados(caliId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Lista las calificaciones de un estudiante, 
        /// devuelve un DataTable con los resultados de las mismas
        /// </summary>
        /// <param name="estudianteId"></param>
        /// <returns></returns>
        public DataTable listarPorEstudiante(int estudianteId)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion);
            try
            {
                return adC.listarPorEstudiante(estudianteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Agrega una calificación, en base a un objeto ECalificacion, 
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="cali"></param>
        /// <returns></returns>
        public bool agregar(ECalificacion cali)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion); 
            try
            {
                return adC.agregar(cali);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Actualiza una calificación en base en un objeto ECalificacion, 
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="cali"></param>
        /// <returns></returns>
        public bool actualizar(ECalificacion cali)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion);
            try
            {
                return adC.actualizar(cali);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Elimina una calificación con base en un Id, 
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="calificacionId"></param>
        /// <returns></returns>
        public bool eliminar(int calificacionId)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion); 
            try
            {
                return adC.eliminar(calificacionId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Lista detalles importantes para
        /// procesar después de una calificación basado en un Id
        /// devuelve un objeto ECalificacion
        /// </summary>
        /// <param name="calificacionId"></param>
        /// <returns></returns>
        public ECalificacion listar(int calificacionId)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion);
            try
            {
                return adC.listar(calificacionId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Verifica si una calificación existe, 
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="calificacionId"></param>
        /// <returns></returns>
        public bool existe(int calificacionId)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion);
            try
            {
                return adC.existe(calificacionId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
