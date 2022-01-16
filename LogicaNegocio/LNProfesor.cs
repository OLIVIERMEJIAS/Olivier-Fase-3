using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using AccesoDatos;
using System.Data;
namespace LogicaNegocio
{
    public class LNProfesor
    {
        public string CadConexion { get; set; }

        public LNProfesor()
        {
            CadConexion = "";
        }

        public LNProfesor(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Accede a los datos de acceso de sesión de un profesor
        /// basándose en un objeto EProfesor, devuelve un int como
        /// confirmación
        /// </summary>
        /// <param name="prof"></param>
        /// <returns></returns>
        public int accesoUsuario(EProfesor prof)
        {
            int result = -1;
            ADProfesor adp = new ADProfesor(CadConexion);
            try
            {
                result = adp.accesoUsuario(prof);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        /// <summary>
        /// Verifica el número de lecciones de un profesor, 
        /// en este caso basado en su Id, devuelve un número 
        /// de registros en los detalles de horario 
        /// que serán entenidos según profesor, 
        /// en cantidad de lecciones
        /// </summary>
        /// <param name="profId"></param>
        /// <returns></returns>
        public byte numLecciones(int profId)
        {
            ADProfesor adp = new ADProfesor(CadConexion);
            try
            {
                return adp.numLecciones(profId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Accede al Id del primer profesor que imparte una materia, 
        /// basandose en el Id de esta,
        /// devuelve un int con el mismo
        /// </summary>
        /// <param name="mateId"></param>
        /// <returns></returns>
        public int accederAProfesor(byte mateId)
        {
            ADProfesor adp = new ADProfesor(CadConexion);

            try
            {
                return adp.accederAProfesor(mateId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Verifica la disponiblidad de un profesor, 
        /// en un día específico a una hora de inicio
        /// devuelve un "" cuando esté libre, pero una cadena con una 
        /// hora de fin cuando esté ociupado que será la hora en que 
        /// termina la lección
        /// </summary>
        /// <param name="horaI"></param>
        /// <param name="dia"></param>
        /// <param name="profeId"></param>
        /// <returns></returns>
        public string disponibleHoraI(string horaI, char dia, int profeId)
        {
            ADProfesor adp = new ADProfesor(CadConexion); try
            {
                return adp.disponibleHoraI(horaI, dia, profeId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
    }
}
