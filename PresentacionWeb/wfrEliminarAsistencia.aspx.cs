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
            if (!IsPostBack)
            {
                try
                {   //esta página es para la confirmación de
                    //la eliminación de un registro de asistencia
                    //se obtiene el Id de la asistencia de la v
                    //ariable se sesión y con ella se carga un
                    //objeto EAsistencia con detalles importantes
                    //para mostrar
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
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int asistenciaId = int.Parse(Session["_eliminarAsistencia"].ToString());
            try
            {//al confirmar se procede a la eliminación y
             //se redirije a la página de registros de a
             //sistencia como tal de estudiante
                if (lnA.eliminar(asistenciaId))
                {
                    Session["_exito"] = "Asistencia Eliminada con Éxito!";
                    Response.Redirect("wfrAsistencias.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //al cancelar la eliminación se procede a la redirección
            //a la página de registros de asistencias como tal del estudiante
            Response.Redirect("wfrAsistencias.aspx", false);
        }
    }
}