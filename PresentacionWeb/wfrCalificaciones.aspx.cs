using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidades;
using System.Data;

namespace PresentacionWeb
{
    public partial class wfrCalificaciones : System.Web.UI.Page
    {
        LNCalificacion lnC = new LNCalificacion(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {

            cargarCalificaciones();

        }

        protected void cargarCalificaciones()
        {
            try
            {
                int estuId = int.Parse(Session["_estudiante"].ToString());
                gdvCalificaciones.DataSource = lnC.listarPorEstudiante(estuId);
                gdvCalificaciones.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }

        }

        protected void lnkEliminar_Command(object sender, CommandEventArgs e)
        {
            int calificacionId = int.Parse(e.CommandArgument.ToString());
            try
            {
                if (lnC.existe(calificacionId))
                {
                    Response.Redirect("wfrEliminarCalificaciones.aspx", false);
                    Session["_eliminarCalificacion"] = calificacionId;
                }
                else
                    Session["_wrn"] = "Esta calificación ya no existe, fue borrada!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }



        protected void lnkModificar_Command(object sender, CommandEventArgs e)
        {
            int calificacionId = int.Parse(e.CommandArgument.ToString());
            try
            {
                if (lnC.existe(calificacionId))
                {
                    Session["_modificarCalificacion"] = calificacionId;
                    Response.Redirect("wfrNuevaCalificaciones.aspx", false);
                }
                else
                    Session["_wrn"] = "Esta calificación ya no existe, fue borrada!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrListarCalificaciones.aspx", false);
        }
    }
}