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
            if (!IsPostBack)
            {
                LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
                if (Session["_nuevaAsistencia"] != null)
                {
                    try
                    {
                        int estudianteId = int.Parse(Session["_nuevaAsistencia"].ToString());
                        string nombreEst = lnE.existe(estudianteId);
                        if (nombreEst != "")
                            txtEstudiante.Text = nombreEst;
                        txtIdMateria.Text = Config.MateriaId.ToString();
                        txtMateria.Text = Config.MateriaNombre;
                        txtFecha.Text = DateTime.Today.ToString();

                    }
                    catch (Exception ex)
                    {

                        Session["_err"] = ex.Message;
                    }
                }
                else
                {
                    int asistenciaId = int.Parse(Session["_modificarAsistencia"].ToString());
                    try
                    {
                        EAsistencia asist;
                        asist = lnA.listar(asistenciaId);
                        int estudianteId = asist.EstudianteId;
                        string nombreEst = lnE.existe(estudianteId);
                        if (nombreEst != "")
                            txtEstudiante.Text = nombreEst;
                        txtIdMateria.Text = Config.MateriaId.ToString();
                        txtMateria.Text = Config.MateriaNombre;
                        txtFecha.Text = asist.FechaHora;
                        ddlEstados.Text = asist.Estado;

                    }
                    catch (Exception ex)
                    {

                        Session["_err"] = ex.Message;
                    }
                }
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

                EAsistencia asist = new EAsistencia(0,estudianteId, Config.MateriaId, ddlEstados.Text);
                try
                {
                    if (lnA.agregarAsistencia(asist))
                    {
                        Session["_exito"] = "Asistencia Agregada Exitosamente!!";
                        Response.Redirect("wfrAsistencias.aspx", false);
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
                int asistenciaId = int.Parse(Session["_modificarAsistencia"].ToString());
                try
                {
                    EAsistencia asist;
                    asist = lnA.listar(asistenciaId);
                    if(asist.Estado != ddlEstados.Text)
                    {
                        asist.Estado = ddlEstados.Text;
                        if (lnA.actualizarAsistencia(asist))
                        {
                            Session["_exito"] = "Se ha modificado la asistencia con éxito!!";
                            Session["_modificarAsistencia"] = null;
                            Response.Redirect("wfrAsistencias.aspx", false);
                        }
                        else
                        {
                            Session["_exito"] = "No se ha podido modificar la asistencia!!";
                            Session["_modificarAsistencia"] = null;
                            Response.Redirect("wfrAsistencias.aspx", false);
                        }
                    }
                    else
                    {
                        Session["_wrn"] = "No existen cambios que modificar!";
                    }

                }
                catch (Exception ex)
                {

                    Session["_err"] = ex.Message;
                }
            }
        }
    }
}