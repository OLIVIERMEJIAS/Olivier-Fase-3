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
                {   //se eliminar la calificaciones,
                    //se cargará esta página de confirmación,
                    //donde se cargarán todos los datos necesarios para
                    //un buen reconocimiento de lo que se va a borrar
                    int calificacionId = int.Parse(Session["_eliminarCalificacion"].ToString());
                    ECalificacion cali;
                    cali = lnC.listar(calificacionId);
                    //se lista la calificación con detalles
                    //con base a su Id
                    lblFecha.Text += cali.FechaIngreso;
                    lblCalificacion.Text += cali.Calificacion;
                    lblEstado.Text += cali.Estado;
                    lblTrimestre.Text += cali.TrimestreID;
                    //se lista el nombre del estudiante
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
            {   //al confirmar la eliminación se borra y se redirije
                //a la página de cregistros de calificaciones como tal
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
            //al cancelar la eliminación solo se redirije a los
            //registros de calificaciones del estudiante
            Response.Redirect("wfrCalificaciones.aspx", false);
        }
    }
}