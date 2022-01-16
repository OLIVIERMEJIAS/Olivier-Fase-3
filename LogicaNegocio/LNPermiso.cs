using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;
using Entidades;
namespace LogicaNegocio
{
    public class LNPermiso
    {
        public string CadConexion { get; set; }

        public LNPermiso()
        {
            CadConexion = "";
        }

        public LNPermiso(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Inserta un registro de permiso de cambio de 
        /// calificación con base en un objeto EPermiso,
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="permi"></param>
        /// <returns></returns>
        public bool insertar(EPermiso permi)
        {
            ADPermiso adP = new ADPermiso(CadConexion);

            try
            {
                return adP.insertar(permi);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Lista los permisos que tiene pendientes, aceptados o rachazados
        /// un profesor con su Id, en base al estado o condición del permiso, 
        /// devuelve un DataTable
        /// </summary>
        /// <param name="profeId"></param>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public DataTable listar(int profeId, string condicion)
        {
            ADPermiso adP = new ADPermiso(CadConexion);
            try
            {
                return adP.listar(profeId, condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Lista todos los permisos que estén pendientes, rechazados o aceptados, 
        /// según su estado o condición, devuelve un DataTable con los
        ///  resultados
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public DataTable listar(string condicion)
        {
            ADPermiso adP = new ADPermiso(CadConexion);
            try
            {
                return adP.listar(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Actualiza un permiso con base en su Id y 
        /// una abreviatura char de sus estado a actualizar
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="permisoId"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public bool actualizar(int permisoId, char estado)
        {
            ADPermiso adP = new ADPermiso(CadConexion);

            try
            {
                return adP.actualizar(permisoId, estado);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Verifica si existe un permiso, con base en su Id,
        /// devuelve un objeto EPermiso con datos de posterior análisis
        /// </summary>
        /// <param name="permisoId"></param>
        /// <returns></returns>
        public EPermiso existe(int permisoId)
        {
            ADPermiso adP = new ADPermiso(CadConexion);

            try
            {
                return adP.existe(permisoId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


}
