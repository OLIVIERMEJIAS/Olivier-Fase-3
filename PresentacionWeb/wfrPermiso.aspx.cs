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
    public partial class wfrPermiso : System.Web.UI.Page
    {
        LNPermiso lnP = new LNPermiso(Config.getCadConec);
        LNCalificacion lnC = new LNCalificacion(Config.getCadConec);
        LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMateria.Text += Config.MateriaNombre;
                lblProfesor.Text += Config.NombreProfesor;
                lblEstudiante.Text += lnE.existe(int.Parse(Session["_estudiante"].ToString()));
                ECalificacion cali;
                cali = lnC.listar(int.Parse(Session["_modificarCalificacion"].ToString()));
                lblNotaA.Text += cali.Calificacion.ToString();
                lblEstadoA.Text += cali.Estado;
                txtEstadoR.Text = Request.Cookies["MyCookie"]["_estadoR"];
                txtNotaR.Text = Request.Cookies["MyCookie"]["_calificacion"];
                

            }
            catch(Exception ex)
            {
                Session["_err"] = ex.Message;
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                ECalificacion cali;
                cali = lnC.listar(int.Parse(Session["_modificarCalificacion"].ToString()));
                EPermiso permi = new EPermiso(0, Config.Profesor, Config.MateriaId,
                    int.Parse(Session["_estudiante"].ToString()),
                    int.Parse(Session["_modificarCalificacion"].ToString()), cali.Calificacion,
                    decimal.Parse(txtNotaR.Text), cali.Estado, txtEstadoR.Text, cali.FechaIngreso,
                    'P', txtMotivo.Text);
                if (lnP.insertar(permi))
                {
                    Session["_exito"] = "Solicitud de Permiso de cambio de Calificación enviado exitosamente";
                    Response.Redirect("wfrCalificaciones.aspx", false);
                }
                else
                    Session["_err"] = "No se pudo enviar la solicitud!!";
            }
            catch(Exception ex)
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