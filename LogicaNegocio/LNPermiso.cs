using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;
using Entidades;
namespace LogicaNegocio
{
    public class LNPermiso
    {
        public string CadConexion { get; set; }

        public LNPermiso()
        {
            CadConexion = "";
        }

        public LNPermiso(string cad)
        {
            CadConexion = cad;
        }

        public bool insertar(EPermiso permi)
        {
            ADPermiso adP = new ADPermiso(CadConexion);

            try
            {
                return adP.insertar(permi);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable listar(int profeId, string condicion)
        {
            ADPermiso adP = new ADPermiso(CadConexion);
            try
            {
                return adP.listar(profeId, condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable listar(string condicion)
        {
            ADPermiso adP = new ADPermiso(CadConexion);
            try
            {
                return adP.listar(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool actualizar(int permisoId, char estado)
        {
            ADPermiso adP = new ADPermiso(CadConexion);

            try
            {
                return adP.actualizar(permisoId, estado);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EPermiso existe(int permisoId)
        {
            ADPermiso adP = new ADPermiso(CadConexion);

            try
            {
                return adP.existe(permisoId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


}
