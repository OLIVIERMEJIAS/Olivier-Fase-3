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
    public partial class wfrEliminarAsistencia : System.Web.UI.Page
    {
        LNAsistencia lnA = new LNAsistencia(Config.getCadConec);
        LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int asistenciaId = int.Parse(Session["_eliminarAsistencia"].ToString());
                EAsistencia asist;
                asist = lnA.listar(asistenciaId);
                lblFecha.Text += asist.FechaHora;
                lblEstado.Text += asist.Estado;
                string nombreEst = lnE.existe(asist.EstudianteId);
                if (nombreEst != "")
                    lblEstudiante.Text += nombreEst; 
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int asistenciaId = int.Parse(Session["_eliminarAsistencia"].ToString());
            try
            {
                if (lnA.eliminarAsistencia(asistenciaId))
                {
                    Session["_exito"] = "Asistencia Eliminada con Éxito!";
                    Response.Redirect("wfrListarAsistencia.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrListarAsistencias.aspx", false);
        }
    }
}