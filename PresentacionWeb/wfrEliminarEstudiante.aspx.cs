using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using LogicaNegocio;
namespace PresentacionWeb
{
    public partial class wfrEliminarEstudiante : System.Web.UI.Page
    {
        LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
        LNDistrito lnD = new LNDistrito(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            int estId = int.Parse(Session["_eliminarEstudiante"].ToString());
            try
            {
                EEstudiante est = new EEstudiante();
                est = lnE.listarDetallesPorEstudiante(estId);
                lblCarnet.Text += est.Carnet;
                lblNumIdent.Text += est.NumIdentificacion.ToString();
                lblSeccion.Text += est.Seccion;
                lblNombre.Text += est.Nombre;
                lblApellido1.Text += est.Apellido1;
                lblApellido2.Text += est.Apellido2;
                lblEmail.Text += est.Email;
                lblGenero.Text += est.Genero.ToString();
                lblFechaIng.Text += est.FechaIngreso.ToString();
                lblFechaNac.Text += est.FechaNacimiento.ToString();
                ckbActivo.Checked = est.Activo;
                ckbBorrado.Checked = est.Borrado;
                int disId = est.Distrito;
                lblDistrito.Text += lnD.nombre(disId).Distrito;
                lblDirec.Text = est.DirExact;
                
                
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        /// <summary>
        /// Confirma la eliminación y procede
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int estId = int.Parse(Session["_eliminarEstudiante"].ToString());

            try
            {
                if (lnE.eliminar(estId))
                {
                    Session["exito"] = "Estudiante Eliminado con éxito!!";
                    Session["_eliminarEstudiante"] = null;
                
                    Response.Redirect("wfrListarEstudiantes,aspx", false);
                }
                else
                    Session["_err"] = "El estudiante ya no existe, no fue posible la eliminación";
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["_eliminarEstudiante"] = null;
            Response.Redirect("wfrListarEstudiantes,aspx", false);

        }
    }
}