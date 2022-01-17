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
                //se cargan todos los datos necesarios para
                //el proceso del permiso
                lblMateria.Text += Config.MateriaNombre;
                lblProfesor.Text += Config.NombreProfesor;
                //se obtiene el nombre del estudiante
                lblEstudiante.Text += lnE.existe(int.Parse(Session["_estudiante"].ToString()));
                ECalificacion cali;
                //se obtine la variable que guarda el Id de la calificación y cargan los datos
                cali = lnC.listar(int.Parse(Session["_modificarCalificacion"].ToString()));
                lblNotaA.Text += cali.Calificacion.ToString();
                lblEstadoA.Text += cali.Estado;
                //se carga el estado de reemplazo y
                //el cuadro de la nota de reemplazo mediante la cookie
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
            {//al enviar un permiso se confirma que la infirmación está correcta
                //y que se está de acuerdo con que el director revise el mismo
                ECalificacion cali;
                //se carga nuevamente los datos de la calificación 
                //se usa la variable de sesión que se creo al dar click en modificar
                //en el formulario wfrCalificaciones y la variable de sesión de
                //estudiante con el Id que se creo al dar click en ver
                //calificaciones en wfrListarCalificaciones
                cali = lnC.listar(int.Parse(Session["_permiso"].ToString()));
                //se crea un objeto EPermiso con todos los datos pertinentes para envío
                EPermiso permi = new EPermiso(0, Config.Profesor, Config.MateriaId,
                    int.Parse(Session["_estudiante"].ToString()),
                    int.Parse(Session["_permiso"].ToString()), cali.Calificacion,
                    decimal.Parse(txtNotaR.Text), cali.Estado, txtEstadoR.Text, cali.FechaIngreso,
                    'P', txtMotivo.Text);
                //se inserta en la tabla de permisos el nuevo registro
                //que aparecerá para revisión del director
                if (lnP.insertar(permi))
                {
                    //notificación de éxito y redirección a la lista de
                    //registros de calificaciones
                    //es preciso la anulación de la variable de sesión antes
                    Session["_exito"] = "Solicitud de Permiso de cambio de Calificación enviado exitosamente";
                    Session["_permiso"] = null;
                    Response.Redirect("wfrCalificaciones.aspx", false);
                }
                else
                {
                    Session["_err"] = "No se pudo enviar la solicitud!!";
                    Session["_permiso"] = null;
                }
            }
            catch(Exception ex)
            {
                Session["_permiso"] = null;
                Session["_err"] = ex.Message;
            }
            
                
        }
        //si se desea cancelar, se redirecciona a la lista de
        //calificaciones del estudiante, antes se anula la variable de sesión
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["_permiso"] = null;
            Response.Redirect("wfrCalificaciones.aspx", false);
        }
    }
}