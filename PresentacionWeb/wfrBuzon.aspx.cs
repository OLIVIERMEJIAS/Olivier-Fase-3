using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidades;
namespace PresentacionWeb
{

    public partial class wfrBuzon : System.Web.UI.Page
    {
        LNPermiso lnP = new LNPermiso(Config.getCadConec);
        LNCalificacion lnC = new LNCalificacion(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cargarPendientesD();
                cargarPendientesP();
                cargarAceptados();
                cargarRechazados();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        protected void lnkAceptar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                EPermiso permi = lnP.existe(int.Parse(e.CommandArgument.ToString()));
                if (permi.CalificacionId != 0)
                {
                    if (lnP.actualizar(int.Parse(e.CommandArgument.ToString()), 'A'))
                    {
                        ECalificacion cali = new ECalificacion();
                        cali.CalificacionId = permi.CalificacionId;
                        cali.Estado = permi.EstadoCalificacionReemplazo;
                        cali.Calificacion = permi.NotaReemplazo;
                        if (lnC.actualizar(cali))
                        {
                            Session["_exito"] = "Permiso aceptado y calificación actualizada!!";
                            Page_Load(sender, e);
                        }
                        else
                            Session["_err"] = "Hubo un problema actualizando la calificación!";

                    }
                    else
                        Session["_err"] = "Hubo un problema actualizando el permiso a: Aceptado!";

                }
                else
                    Session["_wrn"] = "Ya no existe este permiso!!";
            }
            catch (Exception ex)
            {

                Session["err"] = ex.Message;
            }
        }

        protected void lnkRechazar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                EPermiso permi = lnP.existe(int.Parse(e.CommandArgument.ToString()));
                if (permi.CalificacionId != 0)
                {
                    if (lnP.actualizar(int.Parse(e.CommandArgument.ToString()), 'R'))
                    {
                        Session["_exito"] = "Se ha rechazado la solitud de cambio de calificación!";
                        Page_Load(sender, e);
                    }
                    else
                        Session["_err"] = "Hubo un problema actualizando el permiso a: Rechazado!";

                }
                else
                    Session["_wrn"] = "Ya no existe este permiso!!";
            }
            catch (Exception ex)
            {

                Session["err"] = ex.Message;
            }
        }

        protected void cargarPendientesP()
        {
            string pendiente = "P";
            try
            {
                gdvPendientesP.DataSource = lnP.listar(Config.Profesor,pendiente);
                gdvPendientesP.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
            
        }

        protected void cargarPendientesD()
        {
            string pendiente = "P";
            try
            {
                gdvpendientesD.DataSource = lnP.listar(pendiente);
                gdvpendientesD.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        protected void cargarAceptados()
        {
            string aceptados = "A";
            try
            {
                if(Session["_director"] != null) {
                    gdvAceptados.DataSource = lnP.listar(aceptados);
                    gdvAceptados.DataBind();
                }
                else
                {
                    gdvAceptados.DataSource = lnP.listar(Config.Profesor,aceptados);
                    gdvAceptados.DataBind();
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
           
        }

        protected void cargarRechazados()
        {
            string recha = "R";
            try
            {
                if (Session["_director"] != null)
                {
                    gdvRechazados.DataSource = lnP.listar(recha);
                    gdvRechazados.DataBind();
                }
                else
                {
                    gdvRechazados.DataSource = lnP.listar(Config.Profesor, recha);
                    gdvRechazados.DataBind();
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
    }
}