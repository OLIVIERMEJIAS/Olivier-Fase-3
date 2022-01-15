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
    public partial class wfrEliminarCalificaciones : System.Web.UI.Page
    {
        LNCalificacion lnC = new LNCalificacion(Config.getCadConec);
        LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int calificacionId = int.Parse(Session["_eliminarCalificacion"].ToString());
                    ECalificacion cali;
                    cali = lnC.listar(calificacionId);
                    lblFecha.Text += cali.FechaIngreso;
                    lblCalificacion.Text += cali.Calificacion;
                    lblEstado.Text += cali.Estado;
                    lblTrimestre.Text += cali.TrimestreID;
                    string nombreEst = lnE.existe(cali.EstudianteID);
                    if (nombreEst != "")
                        lblEstudiante.Text += nombreEst;
                }
                catch (Exception ex)
                {

                    Session["_err"] = ex.Message;
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int calificacionId = int.Parse(Session["_eliminarCalificacion"].ToString());
            try
            {
                if (lnC.eliminar(calificacionId))
                {
                    Session["_exito"] = "Calificación Eliminada con Éxito!";
                    Response.Redirect("wfrCalificaciones.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrCalificaciones.aspx", false);
        }
    }
}