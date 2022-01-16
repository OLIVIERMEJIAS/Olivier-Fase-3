using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using AccesoDatos;

namespace LogicaNegocio
{
    public class LNAsistente
    {
        public string CadConexion { get; set; }

        public LNAsistente()
        {
            CadConexion = "";
        }

        public LNAsistente(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Confirma los datos de acceso de sesión del asistente de dirección
        /// devuelve un boolean confirmando
        /// </summary>
        /// <param name="asis"></param>
        /// <returns></returns>
        public bool accesoUsuario(EAsistente asis)
        {
            bool result = false;
            ADAsistente ada = new ADAsistente(CadConexion);
            try
            {
                result = ada.accesoUsuario(asis);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
