using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class LNCalificacion
    {
        public string CadConexion { get; set; }

        public LNCalificacion()
        {
            CadConexion = "";
        }

        public LNCalificacion(string cad)
        {
            CadConexion = cad;
        }

        public DataTable listarPorEstudiante(int estudianteId)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion);
            try
            {
                return adC.listarPorEstudiante(estudianteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool agregar(ECalificacion cali)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion); 
            try
            {
                return adC.agregar(cali);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool actualizar(ECalificacion cali)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion);
            try
            {
                return adC.actualizar(cali);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool eliminar(int calificacionId)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion); 
            try
            {
                return adC.eliminar(calificacionId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ECalificacion listar(int calificacionId)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion);
            try
            {
                return adC.listar(calificacionId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool existe(int calificacionId)
        {
            ADCalificacion adC = new ADCalificacion(CadConexion);
            try
            {
                return adC.existe(calificacionId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
