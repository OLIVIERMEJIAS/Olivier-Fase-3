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
            if(!IsPostBack)
                cargarEstudiantes(txtSeccion.Text);
            
        }

        protected void cargarSecciones()
        {
            DataTable datos;
            try
            {//se cargan las secciones que imparte el profesor,
                //con la variable static de la clase Config, 
                //que guarda el Id del profesor
                //se posiciona en el buscador de secciones,
                //la promera sección que aparezca en la carga,
                //estas estarán en orden
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
        //cada vez que se elija una sección del buscador
        //y al inicio de la carga de la página,
        // los estudiantes se cargarán con este método
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
        //al seleccionar una sección del buscador,
        //se cargarán los respectivos estudiantes
        protected void lnkSeleccionarSeccion_Command(Object sender, CommandEventArgs e)
        {
            cargarEstudiantes(e.CommandArgument.ToString());
        }
        //al ver asistencias, con el Id del estudiante,
        //guardado en el commanArgument,, se procede a ver si este aún existe
        //de existir se cargará una variable de sesión con el Id de este
        //así como la redirección a la página de los registros como tal de
        //las asistencias
        protected void lnkVerAsistencias_Command(object sender, CommandEventArgs e)
        {
            int estuId = int.Parse(e.CommandArgument.ToString());
            if (lnE.existe($"estudianteId = {estuId}")){
                Session["_estudiante"] = e.CommandArgument.ToString();
                Response.Redirect("wfrAsistencias.aspx", false);
            }
            else
                Session["_wrn"] = "Este estudiante ya no existe, fue borrado!";
        }

        protected void lnkAsignar_Command(object sender, CommandEventArgs e)
        {
           //al desear asignar una nueva asistencia, se procede a verificar
           //si el estudiante aún existe, si lo está se guarda su Id
           //en variable de sesión y se redirje a mantenimiento de asistencias
            int estuId = int.Parse(e.CommandArgument.ToString());
            if (lnE.existe($"estudianteId = {estuId}"))
            {
                Session["_estudiante"] = e.CommandArgument.ToString();
                Session["_nuevaAsistencia"] = e.CommandArgument.ToString();
                Response.Redirect("wfrNuevaAsistencia.aspx", false);
            }
            else
                Session["_wrn"] = "Este estudiante ya no existe, fue borrado!";
        }
    }
    
}