﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace AccesoDatos
{
    public class ADPermiso
    {
        public string CadConexion { get; set; }

        public ADPermiso()
        {
            CadConexion = "";
        }

        public ADPermiso(string cad)
        {
            CadConexion = cad;
        }
        /// <summary>
        /// Lista los permisos de cambio de calificación, con base en un Id de profesor,
        /// y una condicación,
        /// la cual puede ser rechazado, aceptado o pendiente
        /// devuelve un DataTable con los resultados y los datos importante para cada registro
        /// </summary>
        /// <param name="profeId"></param>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public DataTable listar(int profeId, string condicion)
        {

            DataTable datos = new DataTable();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select p.permisoId as permisoId, em.nombre + ' ' + " +
                "em.apellido1 + ' ' + em.apellido2 as profesor, m.nombre as materia," +
                " e.nombre + ' ' + e.apellido1 + ' ' + e.apellido2 as estudiante," +
                " e.seccion as seccion, " +
                " p.notaActual as notaA, p.notaReemplazo as notaR," +
                " p.estadoCalificacionActual as EstadoA, p.estadoCalificacionReemplazo" +
                " as EstadoR, p.fecha as fecha" +
                ", p.motivo as motivo From Permisos p inner join Estudiantes e" +
                " On e.estudianteId = p.estudianteId inner join Materias m" +
                " On m.materiaId = p.materiaId inner join Empleados em" +
                " On em.empleadoId = p.profesorId" +
                $" Where p.estadoPermiso = '{condicion}' and p.profesorId = {profeId}";
            SqlDataAdapter adaptador = new SqlDataAdapter(sentencia, conexion);

            try
            {
                adaptador.Fill(datos);
                adaptador.Dispose();
            }
            catch (Exception)
            {
                adaptador.Dispose();
                throw new Exception("No se pudo realizar conexión de datos");
            }

            return datos;
        }
        /// <summary>
        /// Inserta un permiso de cambio de calificación, en base con un objeto EPermiso
        /// devuelve un boolean de confirmación
        /// </summary>
        /// <param name="permi"></param>
        /// <returns></returns>
        public bool insertar(EPermiso permi)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Insert Into Permisos Values(@profeId, @mateId," +
                "@estuId, @caliId, @notaA, @notaR, @estadoCaliA, @estadoCaliR, " +
                "getdate(), 'P', @motivo)";
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@profeId", permi.ProfesorId);
            comando.Parameters.AddWithValue("@mateId", permi.MateriaId);
            comando.Parameters.AddWithValue("@estuId", permi.EstudianteId);
            comando.Parameters.AddWithValue("@caliId", permi.CalificacionId);
            comando.Parameters.AddWithValue("@notaA", permi.NotaActual);
            comando.Parameters.AddWithValue("@notaR", permi.NotaReemplazo);
            comando.Parameters.AddWithValue("@estadoCaliA", permi.EstadoCalificacionActual);
            comando.Parameters.AddWithValue("@estadoCaliR", permi.EstadoCalificacionReemplazo);
            comando.Parameters.AddWithValue("@motivo", permi.Motivo);

            try
            {
                conexion.Open();
                if (comando.ExecuteNonQuery() != 0)
                    result = true;
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
        /// <summary>
        /// Lista todos los permisos dependiendo de la condificón o estado del permiso, aceptado, 
        /// rechazado, o pendiente
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public DataTable listar(string condicion)
        {

            DataTable datos = new DataTable();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select p.permisoId as permisoId, em.nombre + ' ' + " +
                "em.apellido1 + ' ' + em.apellido2 as profesor, m.nombre as materia," +
                " e.nombre + ' ' + e.apellido1 + ' ' + e.apellido2 as estudiante," +
                " e.seccion as seccion, "+
                " p.notaActual as notaA, p.notaReemplazo as notaR," +
                " p.estadoCalificacionActual as EstadoA, p.estadoCalificacionReemplazo" +
                " as EstadoR, p.fecha as fecha" +
                ", p.motivo as motivo From Permisos p inner join Estudiantes e" +
                " On e.estudianteId = p.estudianteId inner join Materias m" +
                " On m.materiaId = p.materiaId inner join Empleados em" +
                " On em.empleadoId = p.profesorId" +
                $" Where p.estadoPermiso = '{condicion}'";
            SqlDataAdapter adaptador = new SqlDataAdapter(sentencia, conexion);

            try
            {
                adaptador.Fill(datos);
                adaptador.Dispose();
            }
            catch (Exception)
            {
                adaptador.Dispose();
                throw new Exception("No se pudo realizar conexión de datos");
            }

            return datos;
        }
        /// <summary>
        /// Verifica si un permiso existe, en base con su Id de permiso
        /// devuelve un objeto EPerimiso con datos vitales para procesamiento posterior
        /// </summary>
        /// <param name="permisoId"></param>
        /// <returns></returns>
        public EPermiso existe(int permisoId)
        {
            SqlDataReader reader;
            EPermiso permi = new EPermiso();
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Select calificacionId, estadoCalificacionReemplazo," +
                " notaReemplazo From Permisos "   +
            $"Where permisoId = {permisoId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    permi.CalificacionId = reader.GetInt32(0);
                    permi.EstadoCalificacionReemplazo = reader.GetString(1);
                    permi.NotaReemplazo = reader.GetDecimal(2);
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

            return permi;
        }
        /// <summary>
        /// Actualiza el estado de un permiso según un Id de permiso
        /// devuelve un boolean confirmando
        /// </summary>
        /// <param name="permisoId"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public bool actualizar(int permisoId, char estado)
        {

            bool result = false;
            SqlConnection conexion = new SqlConnection(CadConexion);
            string sentencia = "Update Permisos Set estadoPermiso = " +
                $"'{estado}' Where permisoId = {permisoId}";
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            try
            {
                conexion.Open();
                if (comando.ExecuteNonQuery() != 0)
                    result = true;
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
