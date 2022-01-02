﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class ADAula
    {
        public string CadConexion { get; set; }

        public ADAula()
        {
            CadConexion = "";
        }

        public ADAula(string cad)
        {
            CadConexion = cad;
        }

        public string disponibleHoraI(string horaI, char dia, byte aulaId)
        {
            
            string result = "";
            Object dato;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select horaFin From DetallesHorario Where " +
                $"aulaId = {aulaId} and dia = '{dia}' " +
                $"and horaInicio = '{horaI}'";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                dato = comando.ExecuteScalar();
                if (dato != null)
                {
                    result = dato.ToString();
                }
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se pudo realizar conexión de datos");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;
        }

        
    }
}
