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
                //se evalúa si la asistencia es para crear una nueva o modificar
                if (Session["_nuevaAsistencia"] != null)
                {
                    try
                    {//para cerear una nueva se carga el nombre del estudiante
                        //con su Id envíado en una variable de sesión enviada
                        //desde wfrListarAsistencias
                        int estudianteId = int.Parse(Session["_nuevaAsistencia"].ToString());
                        string nombreEst = lnE.existe(estudianteId);
                        if (nombreEst != "")
                            txtEstudiante.Text = nombreEst;
                        txtIdMateria.Text = Config.MateriaId.ToString();
                        txtMateria.Text = Config.MateriaNombre;
                        txtFecha.Text = DateTime.Now.ToString();

                    }
                    catch (Exception ex)
                    {

                        Session["_err"] = ex.Message;
                    }
                }
                else
                {//de ser modificación
                    //con el Id de la asistencia cargado en una variable de sesión
                    //enviada desde wfrAsistencias
                    int asistenciaId = int.Parse(Session["_modificarAsistencia"].ToString());
                    try
                    {
                        //se cargan los datos de la asistencia, 
                        //en un nuevo objeto EAsistencia
                        EAsistencia asist;
                        asist = lnA.listar(asistenciaId);
                        int estudianteId = asist.EstudianteId;
                        //se carga el nombre del estudiante
                        string nombreEst = lnE.existe(estudianteId);
                        if (nombreEst != "")
                            txtEstudiante.Text = nombreEst;
                        //datos de la clase static global Config
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
        /// <summary>
        /// De cancelar la modificación o la inserción de un nuevo registro
        /// las variables de sesión se anula y se redirije a la lista de los estudiantes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["_nuevaAsistencia"] = null;
            Session["_modificarAsistencia"] = null;
            Response.Redirect("wfrListarAsistencias.aspx", false);
        }
        /// <summary>
        /// Para asignar se evalúa si es modificación o creación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            if (Session["_nuevaAsistencia"] != null)
            {//de ser creación

                int estudianteId = int.Parse(Session["_nuevaAsistencia"].ToString());
                //se crea un nuevo objeto EAsistencia para
                //guardar los datos de entrada
                EAsistencia asist = new EAsistencia(0,estudianteId, Config.MateriaId, ddlEstados.Text);
                try
                {//se inserta la asistencia nueva
                    if (lnA.agregar(asist))
                    {
                        //notificación
                        Session["_exito"] = "Asistencia Agregada Exitosamente!!";
                        //anulación de la variable
                        Session["_nuevaAsistencia"] = null;
                        //redirección a la lista de asistencias
                        Response.Redirect("wfrAsistencias.aspx", false);
                        
                    }

                }
                catch (Exception ex)
                {

                    Session["_err"] = ex.Message;
                }
            }
            else
            {//para modificar
                //se guarda en una variable int, el Id de la sistencia,
                //enviado desde wfrAsistencias en la variable de sesión
                int asistenciaId = int.Parse(Session["_modificarAsistencia"].ToString());
                try
                {//se carga un objeto de tipo EAsistencia
                    EAsistencia asist;
                    asist = lnA.listar(asistenciaId);
                    //se validan los cambios
                    if(asist.Estado != ddlEstados.Text)
                    {//de haberlos se reasignar al objeto
                        asist.Estado = ddlEstados.Text;
                        //y se procede a actualizar
                        if (lnA.actualizar(asist))
                        {
                            //de ser exitoso el proceso:
                            //se notifica
                            Session["_exito"] = "Se ha modificado la asistencia con éxito!!";
                            //se anula la variable de sesión ligada al proceso
                            Session["_modificarAsistencia"] = null;
                            //se redirecciona a la página de la lista de asistencias
                            Response.Redirect("wfrAsistencias.aspx", false);
                        }
                        else
                        {//si falla el proceso de actualización
                            Session["_err"] = "No se ha podido modificar la asistencia!!";
                            Session["_modificarAsistencia"] = null;
                            Response.Redirect("wfrAsistencias.aspx", false);
                        }
                    }
                    else
                    {//de no existir cambios que guardar
                        Session["_wrn"] = "No existen cambios que modificar!";
                    }

                }
                catch (Exception ex)
                {//en caso de una excepción
                    Session["_modificarAsistencia"] = null;
                    Session["_nuevaAsistencia"] = null;
                    Session["_err"] = ex.Message;
                }
            }
        }
    }
}