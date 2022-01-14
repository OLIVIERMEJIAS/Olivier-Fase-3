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
            if(IsPostBack){
                cargarAsistencias();
            }
        }

        protected void cargarAsistencias()
        {
            try
            {
                int estuId = int.Parse(Session["_estudiante"].ToString());
                gdvAsistencias.DataSource = lnA.listarPorEstudiante(estuId);
                gdvAsistencias.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
             
        }

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

       
    }
}