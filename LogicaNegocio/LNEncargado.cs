using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using System.Data;
using Entidades;
namespace LogicaNegocio
{
    public class LNEncargado
    {
        public string CadConexion { get; set; }

        public LNEncargado()
        {
            CadConexion = "";
        }

        public LNEncargado(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Listar los encargados con datos completos
        /// devuelve un DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable listar(string condicion = "")
        {
            ADEncargado ade = new ADEncargado(CadConexion);
            try
            {
                return ade.listar(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Verifica que exista un encargado con su Id, 
        /// devuelve boolean para confirmar
        /// </summary>
        /// <param name="encId"></param>
        /// <returns></returns>
        public bool existe(int encId)
        {
            ADEncargado ade = new ADEncargado(CadConexion);

            try
            {
                return ade.existe(encId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Lista todos los detalles de un encargado,
        /// basado en su Id, devuelve un objeto EPersona
        /// </summary>
        /// <param name="encId"></param>
        /// <returns></returns>
        public EPersona listarDetallesPorEncargado(int encId)
        {
            ADEncargado ade = new ADEncargado(CadConexion);
            try
            {
                return ade.listarDetallesPorEncargado(encId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Inserta un registro de encargado, basado en un objeto EPersona
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public bool agregar(EPersona per)
        {
            ADEncargado ade = new ADEncargado(CadConexion);

            try
            {
                return ade.agregar(per);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Actualiza un registro de encargado, basado en un 
        /// objeto EPersona, devuelve un boolean de confirmación
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public bool actualizar(EPersona per)
        {
            ADEncargado ade = new ADEncargado(CadConexion);

            try
            {
                return ade.actualizar(per);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Elimina un registro de encargado,
        /// basado en su Id, devuelve un boolean confirmando
        /// </summary>
        /// <param name="encId"></param>
        /// <returns></returns>
        public bool eliminar(int encId)
        {
            ADEncargado ade = new ADEncargado(CadConexion);

            try
            {
                return ade.eliminar(encId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
