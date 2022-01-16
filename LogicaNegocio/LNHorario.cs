using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using System.Data;
namespace LogicaNegocio
{
    public class LNHorario
    {
        public string CadConexion { get; set; }

        public LNHorario()
        {
            CadConexion = "";
        }

        public LNHorario(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Lista los detalles de un horario importantes para mostar, 
        /// devuelve un DataTable con los detalles de una sección en un día 
        /// específico
        /// </summary>
        /// <param name="sec"></param>
        /// <param name="dia"></param>
        /// <returns></returns>
        public DataTable horarioPorDiaYSeccion(string sec, char dia)
        {
            ADHorario adH = new ADHorario(CadConexion);
            try
            {
                return adH.horarioPorDiaYSeccion(sec, dia);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Lista las secciones que están asociadas a horarios, la existentes
        /// devuelve un DataTable con los resultados
        /// </summary>
        /// <returns></returns>
        public DataTable secciones()
        {
            ADHorario adH = new ADHorario(CadConexion);

            try
            {
                return adH.secciones();
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }
        /// <summary>
        /// Lista las secciones que imparte un profesor, según los horarios
        /// y en base a su Id
        /// devuelve un DataTable con los resultados
        /// </summary>
        /// <param name="profesorId"></param>
        /// <returns></returns>
        public DataTable secciones(int profesorId)
        {
            ADHorario adH = new ADHorario(CadConexion);
            try
            {
                return adH.secciones(profesorId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
