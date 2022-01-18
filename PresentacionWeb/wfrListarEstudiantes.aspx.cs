using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LogicaNegocio;

namespace PresentacionWeb
{
    public partial class wfrListarEstudiantes : System.Web.UI.Page
    {
        LNHorario lnH = new LNHorario(Config.getCadConec);
        LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarSecciones();
            if (!IsPostBack)
            {
                cargarEstudiantes(txtSeccion.Text);
            }
        }

        protected void cargarSecciones()
        {
            DataTable datos;
            try
            {   //se listan todas las secciones existenctes
                datos = lnH.secciones();
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
                gdvEstudiantes.DataSource = lnE.listarPorSeccion(seccion, true);
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
        /// Envía a la página de eliminación, 
        /// no sin antes verificar que exista aún el estudiante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEliminar_Command(object sender, CommandEventArgs e)
        {
            int estId = int.Parse(e.CommandArgument.ToString());
            try
            {
                if(lnE.existe($"estudianteId = {estId}"))
                {
                    Session["_eliminarEstudiante"] = e.CommandArgument.ToString();
                    Response.Redirect("wfrEliminarEstudiante.aspx", false);
                }
                else
                    Session["_err"] = "Ya no existe este estudiante";
            
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        /// <summary>
        /// Envía a la página de mantenimiento de Estudiantes
        /// primero vefica que exista el estudiante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkModificar_Command(object sender, CommandEventArgs e)
        {
            int estId = int.Parse(e.CommandArgument.ToString());
            try
            {
                if (lnE.existe($"estudianteId = {estId}"))
                {
                    Session["_modificarEstudiante"] = e.CommandArgument.ToString();
                    Response.Redirect("wfrNuevoEstudiante.aspx", false);
                }
                else
                    Session["_err"] = "Ya no existe este estudiante";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        /// <summary>
        /// Envía a la página de encargados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkVerEncargados_Command(object sender, CommandEventArgs e)
        {
            Session["_estudianteId"] = e.CommandArgument.ToString();
            Response.Redirect("wfrListarEncargados.aspx",false);
        }
        /// <summary>
        /// Enviar a la página de nuevo estudiante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrNuevoEstudiante.aspx", false);
        }
    }
}