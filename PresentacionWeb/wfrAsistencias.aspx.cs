using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
namespace PresentacionWeb
{
    public partial class wfrAsistencias : System.Web.UI.Page
    {
        LNAsistencia lnA = new LNAsistencia(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            
            cargarAsistencias();
            
        }

        protected void cargarAsistencias()
        {
            try
            {//carga las asistencia de un estudiante, con el Id
             //guardado en la variable de sesión
                int estuId = int.Parse(Session["_estudiante"].ToString());
                gdvAsistencias.DataSource = lnA.listarPorEstudiante(estuId);
                gdvAsistencias.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
             
        }
        /// <summary>
        /// Con el commandArgument que guarda el Id de la asistencia
        /// se ve si existe, si existe se procede a la eliminación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEliminar_Command1(object sender, CommandEventArgs e)
        {
            int asistenciaId = int.Parse(e.CommandArgument.ToString());
            try
            {
                if (lnA.existe(asistenciaId))
                {
                    Response.Redirect("wfrEliminarAsistencia.aspx", false);
                    Session["_eliminarAsistencia"] = asistenciaId;
                }
                else
                    Session["_wrn"] = "Esta asistencia ya no existe, fue borrada!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

       
        /// <summary>
        /// Si se desea modificar se procede a ver si existe, de existir 
        /// se envía a la página de mantenimento de asistencias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkModificar_Command1(object sender, CommandEventArgs e)
        {
            int asistenciaId = int.Parse(e.CommandArgument.ToString());
            try
            {
                if (lnA.existe(asistenciaId))
                {
                    Session["_modificarAsistencia"] = asistenciaId;
                    Response.Redirect("wfrNuevaAsistencia.aspx", false);
                }
                else
                    Session["_wrn"] = "Esta asistencia ya no existe, fue borrada!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        /// <summary>
        /// Para volver se redirije a la página de la 
        /// lista de registros de asistencias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrListarAsistencias.aspx", false);
        }
    }
}