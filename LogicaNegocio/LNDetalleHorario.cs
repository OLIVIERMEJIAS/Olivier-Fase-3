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

        public bool agregar(EDetalleHorario det)
        {
            ADDetalleHorario add = new ADDetalleHorario(CadCadena);
            try
            {
                return add.agregar(det);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool agregarDetallesHorarrio(ArrayList detalles)
        {
            ADDetalleHorario add = new ADDetalleHorario(CadCadena);
            bool result = false;
            try
            {
                foreach (EDetalleHorario item in detalles)
                {
                    result = add.agregar(item);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        public DataTable listarPorSeccion(string sec)
        {
            ADDetalleHorario add = new ADDetalleHorario(CadCadena);

            try
            {
                return add.listarPorSeccion(sec);
            }
            catch (Exception)
            {

                throw;
            }
        }

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

        public bool existe(EDetalleHorario det)
        {
            ADDetalleHorario adDH = new ADDetalleHorario(CadCadena);

            try
            {
                return adDH.existe(det);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
