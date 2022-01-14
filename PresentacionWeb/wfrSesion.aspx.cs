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
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["_director"] = null;
            Session["_asistente"] = null;
            Session["_profesor"] = null;
        }

        protected void btnAccesoDirector_Click(object sender, EventArgs e)
        {
            try
            {
                EDirector dir = new EDirector(txtUsuario.Text,
                    txtContrasena.Text, 'D');
                if (lnd.accesoUsuario(dir))
                {
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

        protected void btnAccesoAsistente_Click(object sender, EventArgs e)
        {
            try
            {
                EAsistente asist = new EAsistente(txtUsuario.Text,
                    txtContrasena.Text, 'A');
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

        protected void btnAccesoProfesor_Click(object sender, EventArgs e)
        {
            try
            {

                EProfesor prof = new EProfesor('P',txtUsuario.Text,
                    txtContrasena.Text);
                int profesorId = lnp.accesoUsuario(prof);
                if (profesorId != -1)
                {
                    Session["_profesor"] = "Acceso";
                    Config.Profesor = profesorId;
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