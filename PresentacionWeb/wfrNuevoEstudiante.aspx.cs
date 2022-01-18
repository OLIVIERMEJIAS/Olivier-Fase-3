using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace PresentacionWeb
{
    public partial class wfrNuevoEstudiante : System.Web.UI.Page
    {
        LNHorario lnH = new LNHorario(Config.getCadConec);
        LNDistrito lnD = new LNDistrito(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDistritos();
            cargarSecciones();
        }
        /// <summary>
        /// Carga los distritos en el grid view de la
        /// búsqueda de distritos
        /// </summary>
        protected void cargarDistritos()
        {
            try
            {
                gdvDistritos.DataSource = lnD.listar();
                gdvDistritos.DataBind();
            }
            catch (Exception ex) 
            {

                Session["_err"] = ex.Message;
            }
            
        }
        /// <summary>
        /// Carga las secciones en el grid view
        /// para la búsqueda de secciones
        /// </summary>
        protected void cargarSecciones()
        {
            try
            {
                gdvSecciones.DataSource = lnH.secciones();
                gdvSecciones.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }

        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Asignación de la sección del grif view al textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSeleccionarSeccion_Command(object sender, CommandEventArgs e)
        {
            txtSeccion.Text = e.CommandArgument.ToString();
            
        }
        /// <summary>
        /// Asignación del Id de distrito al textBox
        /// Se busca el nombre del Distrito basado en el Id
        /// y lo agrega en el textBox del nombre de distrito
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSeleccionarDistrito_Command(object sender, CommandEventArgs e)
        {
            txtIdDistrito.Text = e.CommandArgument.ToString();
            int disId = int.Parse(txtIdDistrito.Text);
            try
            {
                txtDistrito.Text = lnD.nombre(disId).Distrito;
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
    }
}