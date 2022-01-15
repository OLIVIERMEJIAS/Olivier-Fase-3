using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace LogicaNegocio
{
    public class LNAsistencia
    {
        public string CadConexion { get; set; }

        public LNAsistencia()
        {
            CadConexion = "";
        }

        public LNAsistencia(string cad)
        {
            CadConexion = cad;
        }

        
        public DataTable listarPorEstudiante(int estudianteId)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);
            try
            {
                return ada.listarPorEstudiante(estudianteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool agregar(EAsistencia asist)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);
            try
            {
                return ada.agregar(asist);
            }
            catch ( Exception ex)
            {

                throw ex;
            }
        }

        public bool actualizar(EAsistencia asist)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);
            try
            {
                return ada.actualizar(asist);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool eliminar(int asistenciaId)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);
            try
            {
                return ada.eliminar(asistenciaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EAsistencia listar(int asistenciaId)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);

            try
            {
                return ada.listar(asistenciaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool existe(int asistenciaId)
        {
            ADAsistencia ada = new ADAsistencia(CadConexion);

            try
            {
                return ada.existe(asistenciaId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
