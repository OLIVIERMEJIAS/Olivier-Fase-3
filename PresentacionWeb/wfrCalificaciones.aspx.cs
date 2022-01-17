using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidades;
using System.Data;

namespace PresentacionWeb
{
    public partial class wfrCalificaciones : System.Web.UI.Page
    {
        LNCalificacion lnC = new LNCalificacion(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {

            cargarCalificaciones();

        }
        //procedimiento que cargar las calificaciones del estudiante,
        //se accede a una variable de sesión para obtener el Id del estudiante
        protected void cargarCalificaciones()
        {
            try
            {
                int estuId = int.Parse(Session["_estudiante"].ToString());
                gdvCalificaciones.DataSource = lnC.listarPorEstudiante(estuId);
                gdvCalificaciones.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }

        }
        //al eliminar o tratar de hacerlo, se debe validar que no esté esta
       //calificación asociada a permisos de cambio de calificación
        protected void lnkEliminar_Command(object sender, CommandEventArgs e)
        {
            int calificacionId = int.Parse(e.CommandArgument.ToString());
            try
            {
                if (!lnC.asociados(calificacionId))
                {
                    //y así mismo a comprobar que la calificación aún existe 
                    //de existir se redirecciona a la página de confirmación
                    //de eliminación de calificaciones, no antes de guardar
                    //el Id de la calificación en la variable de sesión
                    if (lnC.existe(calificacionId))
                    {
                        Session["_eliminarCalificacion"] = calificacionId;
                        Response.Redirect("wfrEliminarCalificaciones.aspx", false);

                    }
                    else
                        Session["_wrn"] = "Esta calificación ya no existe, fue borrada!";
                }
                else
                    Session["_err"] = "No es posible borrar esta calificación, ya que se encuentra asociada a Permiso(s) de cambio de calificación!!";
            
                
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }



        protected void lnkModificar_Command(object sender, CommandEventArgs e)
        {
            //al tratar de modificar se guarda el commandArgument en un variable
            //con el Id de la calificación
            int calificacionId = int.Parse(e.CommandArgument.ToString());
            try
            {
                //si la calificación aún existe se procede a ir a la ágina de mantenimiento
                //de calificaciones, no sin antes enviar el Id por variable de sesión
                if (lnC.existe(calificacionId))
                {
                    Session["_modificarCalificacion"] = calificacionId;
                    Response.Redirect("wfrNuevaCalificaciones.aspx", false);
                }
                else
                    Session["_wrn"] = "Esta calificación ya no existe, fue borrada!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        //si no se desea eliminar o modificar ninguna calificación al 
        //clickear volver, se vuelve a la página de estudiantes por sección
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrListarCalificaciones.aspx", false);
        }
    }
}