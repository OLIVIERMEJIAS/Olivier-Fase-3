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
    public partial class wfrNuevaAsistencia : System.Web.UI.Page
    {
        LNAsistencia lnA = new LNAsistencia(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
            if(Session["_nuevaAsistencia"] != null)
            {
                try
                {
                    int estudianteId = int.Parse(Session["_nuevaAsistencia"].ToString());
                    string nombreEst = lnE.existe(estudianteId);
                    if(nombreEst != "")
                        txtEstudiante.Text = nombreEst;
                    txtIdMateria.Text = Config.MateriaId.ToString();
                    txtMateria.Text = Config.MateriaNombre;

                }
                catch (Exception ex)
                {

                    Session["_err"] = ex.Message;
                }
            }
            else
            {

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["_nuevaAsistencia"] = null;
            Session["_modificarAsistencia"] = null;
            Response.Redirect("wfrListarAsistencias.aspx", false);
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            if (Session["_nuevaAsistencia"] != null)
            {
                int estudianteId = int.Parse(Session["_nuevaAsistencia"].ToString());

                EAsistencia asist = new EAsistencia(estudianteId, Config.MateriaId, ddlEstados.Text);
                try
                {
                    if (lnA.agregarAsistencia(asist))
                    {
                        Session["_exito"] = "Asistencia Agregada Exitosamente!!";
                        Response.Redirect("wfrListarAsistencias.aspx", false);
                        Session["_nuevaAsistencia"] = null;
                    }

                }
                catch (Exception ex)
                {

                    Session["_err"] = ex.Message;
                }
            }
            else
            {

            }
        }
    }
}