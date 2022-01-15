using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using System.Data;

namespace PresentacionWeb
{
    public partial class wfrListarCalificaciones : System.Web.UI.Page
    {
        LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
        LNHorario lnH = new LNHorario(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarSecciones();
            if(!IsPostBack)
                cargarEstudiantes(txtSeccion.Text);

        }

        protected void cargarSecciones()
        {
            DataTable datos;
            try
            {
                datos = lnH.secciones(Config.Profesor);
                txtSeccion.Text = datos.Rows[0][0].ToString();
                gdvSecciones.DataSource = datos;
                gdvSecciones.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        protected void cargarEstudiantes(string seccion)
        {
            try
            {
                gdvEstudiantes.DataSource = lnE.listarPorSeccion(seccion);
                gdvEstudiantes.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }

        }

        protected void lnkSeleccionarSeccion_Command(Object sender, CommandEventArgs e)
        {
            cargarEstudiantes(e.CommandArgument.ToString());
        }

        protected void lnkVerCalificaciones_Command(object sender, CommandEventArgs e)
        {
            int estuId = int.Parse(e.CommandArgument.ToString());
            if (lnE.existe($"estudianteId = {estuId}"))
            {
                Session["_estudiante"] = e.CommandArgument.ToString();
                Response.Redirect("wfrCalificaciones.aspx", false);
            }
            else
                Session["_wrn"] = "Este estudiante ya no existe, fue borrado!";
        }

        protected void lnkAsignar_Command(object sender, CommandEventArgs e)
        {

            int estuId = int.Parse(e.CommandArgument.ToString());
            if (lnE.existe($"estudianteId = {estuId}"))
            {
                Session["_nuevaCalificacion"] = e.CommandArgument.ToString();
                Response.Redirect("wfrNuevaCalificacion.aspx", false);
            }
            else
                Session["_wrn"] = "Este estudiante ya no existe, fue borrado!";
        }
    }
}