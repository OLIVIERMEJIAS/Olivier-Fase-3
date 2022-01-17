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
        /// <summary>
        /// Se encarga de cargar las secciones que un profesor imparte
        /// para mostrarlas en la búsqueda de estudiantes por seccion
        /// </summary>
        protected void cargarSecciones()
        {
            DataTable datos;
            try
            {   //Config.Profesor guardar el Id del profesor
                //al iniciar sesión
                datos = lnH.secciones(Config.Profesor);
                txtSeccion.Text = datos.Rows[0][0].ToString();
                //se coloca la primera sección del DataTable en 
                //el cuadro de texto de búsqueda de secciones para
                //busque estudiantes al iniciar la carga de la página
                gdvSecciones.DataSource = datos;
                gdvSecciones.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        /// <summary>
        /// Cargar los estudiante de acuerdo a la sección
        /// seleccionada
        /// </summary>
        /// <param name="seccion"></param>
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
        /// <summary>
        /// Al seleccionar de la búsqueda de secciones
        /// , carga estudiantes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSeleccionarSeccion_Command(Object sender, CommandEventArgs e)
        {
            cargarEstudiantes(e.CommandArgument.ToString());
        }
        /// <summary>
        /// Se comprueba que el estudiante existe, de ser así
        /// se crea una variable de sesión para enviar su Id,
        /// y se dirije a la página de registros calificaciones 
        /// como tal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Se valida que es estudiante exista, de ser así
        /// se crea la variable de sesión para enviar el Id,
        /// se redirije a la página de mantenimiento de calificaciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAsignar_Command(object sender, CommandEventArgs e)
        {

            int estuId = int.Parse(e.CommandArgument.ToString());
            if (lnE.existe($"estudianteId = {estuId}"))
            {
                Session["_estudiante"] = e.CommandArgument.ToString();
                Session["_nuevaCalificacion"] = e.CommandArgument.ToString();
                Response.Redirect("wfrNuevaCalificaciones.aspx", false);
            }
            else
                Session["_wrn"] = "Este estudiante ya no existe, fue borrado!";
        }
    }
}