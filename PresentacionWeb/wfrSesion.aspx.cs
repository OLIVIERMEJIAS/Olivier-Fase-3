using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidades;

namespace PresentacionWeb
{
    public partial class wfrSesion : System.Web.UI.Page
    {
        LNDirector lnd = new LNDirector(Config.getCadConec);
        LNProfesor lnp = new LNProfesor(Config.getCadConec);
        LNAsistente lna = new LNAsistente(Config.getCadConec);
        LNMateria lnM = new LNMateria(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["_director"] = null;
            Session["_asistente"] = null;
            Session["_profesor"] = null;
        }
        /// <summary>
        /// Corroboración de la existencia de estos
        /// datos de acceso del director,
        /// de ser correctos se redirije a la página de inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccesoDirector_Click(object sender, EventArgs e)
        {
            try
            {//se crea un objeto EDirector con los datos
                EDirector dir = new EDirector(txtUsuario.Text,
                    txtContrasena.Text, 'D');
                //de ser correctos
                if (lnd.accesoUsuario(dir))
                {
                    //se crea una variable de sesión identificando que
                    //el que inició sesión fue el director
                    Session["_director"] = "Acceso";
                    Response.Redirect("wfrInicio.aspx",false);
                }
                else
                    Session["_wrn"] = "Su Nombre de Usuario y/o su Contraseña son erroneos!!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        /// <summary>
        /// Corroboración de la existencia de estos
        /// datos de acceso del asistente,
        /// de ser correctos se redirije a la página de inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccesoAsistente_Click(object sender, EventArgs e)
        {
            try
            {//se crea un objeto EAsistente con los datos
                EAsistente asist = new EAsistente(txtUsuario.Text,
                    txtContrasena.Text, 'A');
                //si son correctos se crea un variable de sesión que identifica
                //que la sesión la inicó el asistente
                if (lna.accesoUsuario(asist))
                {
                    Session["_asistente"] = "Acceso";
                    Response.Redirect("wfrInicio.aspx",false);
                }
                else
                    Session["_wrn"] = "Su Nombre de Usuario y/o su Contraseña son erroneos!!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        /// <summary>
        /// Corroboración de la existencia de estos
        /// datos de acceso de un profesor,
        /// de ser correctos se redirije a la página de inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccesoProfesor_Click(object sender, EventArgs e)
        {
            try
            {
                //se crea un objeto EProfesor con los datos de acceso
                EProfesor prof = new EProfesor('P',txtUsuario.Text,
                    txtContrasena.Text);
                int profesorId = lnp.accesoUsuario(prof);
               //Se busca el acceso de datos, de existir estos
               //se devuelve un Id de profesor 
               
                if (profesorId != -1)
                {
                    EMateria mate = lnM.accederAMateria(profesorId);
                    //así como la creación de una variable de sesión
                    //para identificar el inicio de sesión de un profesor
                    Session["_profesor"] = "Acceso";
                    //de existir el profesor con esos datos se procede a:
                   //averiguar que materia imparte para guardarla en unas
                   //variables globales de una clase static Config, así como el Id del profesor
                    Config.Profesor = profesorId;
                    Config.MateriaNombre = mate.Nombre;
                    Config.MateriaId = mate.MateriaId;
                    Config.NombreProfesor = $"{prof.Nombre} {prof.Apellido1} {prof.Apellido2}";
                    Response.Redirect("wfrInicio.aspx",false);
                }
                else
                    Session["_wrn"] = "Su Nombre de Usuario y/o su Contraseña son erroneos!!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        
    }
}