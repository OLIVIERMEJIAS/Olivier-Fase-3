using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class LNDirector
    {
        public string CadConexion { get; set; }

        public LNDirector()
        {
            CadConexion = "";
        }

        public LNDirector(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Verifca los datos de acceso e sesíón del ditector,
        /// devuelve un boolean como confirmación
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public bool accesoUsuario(EDirector dir)
        {
            bool result = false;
            ADDirector add = new ADDirector(CadConexion);
            try
            {
                result = add.accesoUsuario(dir);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
