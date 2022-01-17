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
    }
}