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
    public partial class wfrListarAsistencias : System.Web.UI.Page
    {
        LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
        LNHorario lnH = new LNHorario(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarSecciones();
            if (IsPostBack)
            {
                cargarSecciones();
                cargarEstudiantes(txtSeccion.Text);
            }
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
    }
}