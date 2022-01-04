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
        public DataTable horarioPorDiaYSeccion(string sec, char dia)
        {
            ADHorario adH = new ADHorario(CadConexion);
            try
            {
                return adH.horarioPorDiaYSeccion(sec, dia);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
