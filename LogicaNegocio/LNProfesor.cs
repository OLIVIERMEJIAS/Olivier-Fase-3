﻿using System;
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

        public DataTable listar()
        {
            ADProfesor adp = new ADProfesor(CadConexion);

            try
            {
                return adp.listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

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
