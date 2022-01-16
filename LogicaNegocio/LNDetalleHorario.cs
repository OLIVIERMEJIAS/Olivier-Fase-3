using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using System.Collections;
using AccesoDatos;
using System.Data;
using System.Data.SqlClient;

namespace LogicaNegocio
{
    public class LNDetalleHorario
    {
        private string CadCadena { get; set; }

        public LNDetalleHorario()
        {
            CadCadena = "";
        }

        public LNDetalleHorario(string cad)
        {
            CadCadena = cad;
        }
        /// <summary>
        /// Agrega un detalle de horario, en base con un objeto EDetalleHorario
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="det"></param>
        /// <returns></returns>
        public bool agregar(EDetalleHorario det)
        {
            ADDetalleHorario add = new ADDetalleHorario(CadCadena);
            try
            {
                return add.agregar(det);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
        /// <summary>
        /// Verifica que existan registros en la entidad
        ///  de detalles de horario de la base de datos
        /// </summary>
        /// <returns></returns>
        public bool hayRegistros()
        {
            ADDetalleHorario adDH = new ADDetalleHorario(CadCadena);
            try
            {
                return adDH.hayRegistros();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Verifica que una sección este disponible a un día en una hora
        /// de inicio, mediante si Id de horario,
        /// devuelve "" si está libre y una cadena con una hora de fin de 
        /// lección cuando está ocupada
        /// </summary>
        /// <param name="horaI"></param>
        /// <param name="dia"></param>
        /// <param name="horId"></param>
        /// <returns></returns>
        public string disponibleHoraI(string horaI, char dia, int horId)
        {
            ADDetalleHorario adDH = new ADDetalleHorario(CadCadena);

            try
            {
                return adDH.disponibleHoraI(horaI, dia, horId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Elimina todos los registros de la tabla detalles de horario
        /// para cuando sea preciso cambiar todos los horarios por otros
        /// </summary>
        /// <returns></returns>
        public bool eliminarRegistros()
        {
            ADDetalleHorario adDH = new ADDetalleHorario(CadCadena);
            try
            {
                return adDH.eliminarRegistros();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
    }
}
