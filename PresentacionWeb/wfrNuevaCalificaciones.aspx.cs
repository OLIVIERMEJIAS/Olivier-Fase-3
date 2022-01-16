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
    public partial class wfrNuevaCalificaciones : System.Web.UI.Page
    {
        LNCalificacion lnC = new LNCalificacion(Config.getCadConec);
        LNTrimestre lnT = new LNTrimestre(Config.getCadConec);
        LNPermiso lnP = new LNPermiso(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
                if (Session["_nuevaCalificacion"] != null)
                {
                    try
                    {
                        int estudianteId = int.Parse(Session["_nuevaCalificacion"].ToString());
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
                    int calificacionId = int.Parse(Session["_modificarCalificacion"].ToString());
                    try
                    {
                        ECalificacion cali;
                        cali = lnC.listar(calificacionId);
                        int estudianteId = cali.EstudianteID;
                        string nombreEst = lnE.existe(estudianteId);
                        if (nombreEst != "")
                            txtEstudiante.Text = nombreEst;
                        txtIdMateria.Text = Config.MateriaId.ToString();
                        txtMateria.Text = Config.MateriaNombre;
                        txtFecha.Text = cali.FechaIngreso;
                        txtCalificacion.Text = cali.Calificacion.ToString();
                        ddlEstados.Text = cali.Estado;
                        ddlTrimestre.Text = cali.TrimestreID.ToString();
                        ddlTrimestre.Enabled = false;

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
            Session["_nuevaCalificacion"] = null;
            Session["_modificarCalificacion"] = null;
            Response.Redirect("wfrListarCalificaciones.aspx", false);
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            if (Session["_nuevaCalificacion"] != null)
            {

                int estudianteId = int.Parse(Session["_nuevaCalificacion"].ToString());

                ECalificacion cali = new ECalificacion(0, estudianteId, Config.MateriaId, decimal.Parse(txtCalificacion.Text), ddlEstados.Text, byte.Parse(ddlTrimestre.Text));
                try
                {
                    if (lnC.agregar(cali))
                    {
                        Session["_exito"] = "Calificación Agregada Exitosamente!!";
                        Response.Redirect("wfrCalificaciones.aspx", false);
                        Session["_nuevaCalificacion"] = null;
                    }

                }
                catch (Exception ex)
                {

                    Session["_err"] = ex.Message;
                }
            }
            else
            {
                int calificacionId = int.Parse(Session["_modificarCalificacion"].ToString());
                try
                {
                    byte trimId = byte.Parse(ddlTrimestre.Text);
                    DateTime fechaFin = lnT.listar(trimId).FechaFin;
                    if (DateTime.Compare(fechaFin, DateTime.Today) > 0)
                    {
                        ECalificacion cali;
                        cali = lnC.listar(calificacionId);
                        if (cali.Estado != ddlEstados.Text ||
                            cali.Calificacion != decimal.Parse(txtCalificacion.Text))
                        {
                            if (cali.Estado != ddlEstados.Text)
                                cali.Estado = ddlEstados.Text;
                            if (cali.Calificacion != decimal.Parse(txtCalificacion.Text))
                                cali.Calificacion = decimal.Parse(txtCalificacion.Text);
                            if (lnC.actualizar(cali))
                            {
                                Session["_exito"] = "Se ha modificado la calificacion con éxito!!";
                                Session["_modificarCalificacion"] = null;
                                Response.Redirect("wfrCalificaciones.aspx", false);
                            }
                            else
                            {
                                Session["_err"] = "No se ha podido modificar la calificación!!";
                                Session["_modificarCalificacion"] = null;
                                Response.Redirect("wfrCalificaciones.aspx", false);
                            }
                        }
                        else
                        {
                            Session["_wrn"] = "No existen cambios que modificar!";
                        }

                    }
                    else
                    {
                        Session["_err"] = "No puede modificar esta calificación debido" +
                            "a que ya terminó este trimestre!! Desea enviar una" +
                            "solicitud de permiso al director??";
                        Session["_permiso"] = Session["_modificarCalificacion"].ToString();
                        HttpCookie cookie = new HttpCookie("MyCookie");
                        cookie["_calificacion"] = txtCalificacion.Text;
                        cookie["_estadoR"] = ddlEstados.Text;
                        Response.Cookies.Add(cookie);
                        Response.Redirect("wfrPermiso.aspx", false);
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
        
