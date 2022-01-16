using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace LogicaNegocio
{
    public class LNAsistencia
    {
        public string CadConexion { get; set; }

        public LNAsistencia()
        {
            CadConexion = "";
        }

        public LNAsistencia(string cad)
        {
            CadConexion = cad;
        }

        /// <summary>
        /// Accede a las asistencias de un estudiante con base en su Id, 
        /// devuelve un DataTable con las mismas
        /// </summary>
        /// <param name="estudianteId"></param>
        /// <returns></returns>
        public DataTable listarPorEstudiante(int estudianteId)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);
            try
            {
                return ada.listarPorEstudiante(estudianteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Inserta una asistencia basado en un objeto EAsistencia, devuelve un 
        /// boolean de confirmación
        /// </summary>
        /// <param name="asist"></param>
        /// <returns></returns>
        public bool agregar(EAsistencia asist)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);
            try
            {
                return ada.agregar(asist);
            }
            catch ( Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Actualiza una asistencia, basándose en un objeto EAsistencia, 
        /// devuelve un boolean como confirnmación
        /// </summary>
        /// <param name="asist"></param>
        /// <returns></returns>
        public bool actualizar(EAsistencia asist)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);
            try
            {
                return ada.actualizar(asist);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Elimina una asistencia en base a su Id, 
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <param name="asistenciaId"></param>
        /// <returns></returns>
        public bool eliminar(int asistenciaId)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);
            try
            {
                return ada.eliminar(asistenciaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Lista detalles importante de posterior procesamiento
        /// basándose en un Id de asistencia, 
        /// devuelve un objeto EAsistencia
        /// </summary>
        /// <param name="asistenciaId"></param>
        /// <returns></returns>
        public EAsistencia listar(int asistenciaId)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);

            try
            {
                return ada.listar(asistenciaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Confirma que exista una asistencia, en base a su Id, 
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <param name="asistenciaId"></param>
        /// <returns></returns>
        public bool existe(int asistenciaId)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);

            try
            {
                return ada.existe(asistenciaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
