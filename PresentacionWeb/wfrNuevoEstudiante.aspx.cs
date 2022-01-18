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
    public partial class wfrNuevoEstudiante : System.Web.UI.Page
    {
        LNHorario lnH = new LNHorario(Config.getCadConec);
        LNDistrito lnD = new LNDistrito(Config.getCadConec);
        LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDistritos();
            cargarSecciones();
            if(Session["_modificarEstudiante"] != null)
            {
                int estuId = int.Parse(Session["_modificarEstudiante"].ToString());

                EEstudiante estu = lnE.listarDetallesPorEstudiante(estuId);
                txtCarnet.Text = estu.Carnet;
                txtNumIdent.Text = estu.NumIdentificacion.ToString();
                txtSeccion.Text = estu.Seccion;
                txtNombre.Text = estu.Nombre;
                txtApe1.Text= estu.Apellido1;
                txtApe2.Text = estu.Apellido2;
                ddlGenero.Text = estu.Genero.ToString();
                txtEmail.Text = estu.Email;
                cldFechaIngreso.SelectedDate = estu.FechaIngreso;
                cldFechaNacimiento.SelectedDate = estu.FechaNacimiento;
                txtIdDistrito.Text = estu.Distrito.ToString();
                txtDistrito.Text = lnD.nombre(estu.Distrito).Distrito;
                txtDirExact.Text = estu.DirExact;
                ckbActivo.Checked = estu.Activo;
                ckbBorrado.Checked = estu.Borrado;
            }
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
        /// <summary>
        /// Para modificsr o insertar un nuevo registro se
        /// valida la existencia de la variable de sesión
        /// se valida en modificar que hayan cambios
        /// y los tres factores: cédula, carnet y email se validan en existencia
        /// y cambio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            if(Session["_modificarEstudiante"] == null)
            {
                if (lnE.existeCedula(long.Parse(txtNumIdent.Text)))
                {
                    if (lnE.existeCarnet(txtCarnet.Text))
                    {
                        if (lnE.existeEmail(txtEmail.Text))
                        {
                            EEstudiante estu = new EEstudiante();
                            estu.Carnet = txtCarnet.Text;
                            estu.NumIdentificacion = long.Parse(txtNumIdent.Text);
                            estu.Seccion = txtSeccion.Text;
                            estu.Nombre = txtNombre.Text;
                            estu.Apellido1 = txtApe1.Text;
                            estu.Apellido2 = txtApe2.Text;
                            estu.Genero = char.Parse(ddlGenero.Text);
                            estu.Email = txtEmail.Text;
                            estu.FechaIngreso = cldFechaIngreso.SelectedDate;
                            estu.FechaNacimiento = cldFechaNacimiento.SelectedDate;
                            estu.Distrito = int.Parse(txtIdDistrito.Text);
                            estu.DirExact = txtDirExact.Text;
                            estu.Activo = ckbActivo.Checked;
                            estu.Borrado = ckbBorrado.Checked;
                            if (lnE.agregar(estu))
                            {
                                Session["_exito"] = "Estudiante agregado con éxito";
                                Response.Redirect("wfrListarEstudiantes.aspx", false);
                            }
                            else
                                Session["_err"] = "No se pudo insertar estudiante";
                        }
                        else
                            Session["_err"] = "Este correo electrónico ya existe";
                    }
                    else
                        Session["_err"] = "Este carnet de estudiante ya existe";
                }
                else
                    Session["_err"] = "Este número de identificación ya existe"; 
            }
            else
            {
                int estuId = int.Parse(Session["_modificarEstudiante"].ToString());
                try
                {
                    EEstudiante estu = lnE.listarDetallesPorEstudiante(estuId);
                    if (hayCambios(estu))
                    {
                        if (estu.NumIdentificacion != long.Parse(txtNumIdent.Text)){
                            if (lnE.existeCedula(long.Parse(txtNumIdent.Text)))
                            {
                                if (estu.Carnet != txtCarnet.Text)
                                {
                                    if (lnE.existeCarnet(txtCarnet.Text))
                                    {
                                        if (estu.Email != txtEmail.Text)
                                        {
                                            if (lnE.existeEmail(txtEmail.Text))
                                            {
                                                EEstudiante estud = new EEstudiante();
                                                estud.Carnet = txtCarnet.Text;
                                                estud.NumIdentificacion = long.Parse(txtNumIdent.Text);
                                                estud.Seccion = txtSeccion.Text;
                                                estud.Nombre = txtNombre.Text;
                                                estud.Apellido1 = txtApe1.Text;
                                                estud.Apellido2 = txtApe2.Text;
                                                estud.Genero = char.Parse(ddlGenero.Text);
                                                estud.Email = txtEmail.Text;
                                                estud.FechaIngreso = cldFechaIngreso.SelectedDate;
                                                estud.FechaNacimiento = cldFechaNacimiento.SelectedDate;
                                                estud.Distrito = int.Parse(txtIdDistrito.Text);
                                                estud.DirExact = txtDirExact.Text;
                                                estud.Activo = ckbActivo.Checked;
                                                estud.Borrado = ckbBorrado.Checked;
                                                if (lnE.actualizar(estud))
                                                {
                                                    Session["_exito"] = "Estudiante actualizado con éxito";
                                                    Response.Redirect("wfrListarEstudiantes.aspx", false);
                                                }
                                                else
                                                    Session["_err"] = "No se pudo modificar estudiante";
                                            }
                                            else
                                                Session["_err"] = "Este correo electrónico ya existe";
                                        }
                                        else
                                        {
                                            EEstudiante estud = new EEstudiante();
                                            estud.Carnet = txtCarnet.Text;
                                            estud.NumIdentificacion = long.Parse(txtNumIdent.Text);
                                            estud.Seccion = txtSeccion.Text;
                                            estud.Nombre = txtNombre.Text;
                                            estud.Apellido1 = txtApe1.Text;
                                            estud.Apellido2 = txtApe2.Text;
                                            estud.Genero = char.Parse(ddlGenero.Text);
                                            estud.Email = txtEmail.Text;
                                            estud.FechaIngreso = cldFechaIngreso.SelectedDate;
                                            estud.FechaNacimiento = cldFechaNacimiento.SelectedDate;
                                            estud.Distrito = int.Parse(txtIdDistrito.Text);
                                            estud.DirExact = txtDirExact.Text;
                                            estud.Activo = ckbActivo.Checked;
                                            estud.Borrado = ckbBorrado.Checked;
                                            if (lnE.actualizar(estud))
                                            {
                                                Session["_exito"] = "Estudiante actualizado con éxito";
                                                Response.Redirect("wfrListarEstudiantes.aspx", false);
                                            }
                                            else
                                                Session["_err"] = "No se pudo modificar estudiante";

                                        }
                                    }
                                    else
                                        Session["_err"] = "Este carnet de estudiante ya existe";

                                }
                                else
                                {
                                    if (estu.Email != txtEmail.Text)
                                    { 
                                        if (lnE.existeEmail(txtEmail.Text))
                                        {
                                            EEstudiante estud = new EEstudiante();
                                            estud.Carnet = txtCarnet.Text;
                                            estud.NumIdentificacion = long.Parse(txtNumIdent.Text);
                                            estud.Seccion = txtSeccion.Text;
                                            estud.Nombre = txtNombre.Text;
                                            estud.Apellido1 = txtApe1.Text;
                                            estud.Apellido2 = txtApe2.Text;
                                            estud.Genero = char.Parse(ddlGenero.Text);
                                            estud.Email = txtEmail.Text;
                                            estud.FechaIngreso = cldFechaIngreso.SelectedDate;
                                            estud.FechaNacimiento = cldFechaNacimiento.SelectedDate;
                                            estud.Distrito = int.Parse(txtIdDistrito.Text);
                                            estud.DirExact = txtDirExact.Text;
                                            estud.Activo = ckbActivo.Checked;
                                            estud.Borrado = ckbBorrado.Checked;
                                            if (lnE.actualizar(estud))
                                            {
                                                Session["_exito"] = "Estudiante actualizado con éxito";
                                                Response.Redirect("wfrListarEstudiantes.aspx", false);
                                            }
                                            else
                                                Session["_err"] = "No se pudo modificar estudiante";
                                        }
                                        else
                                            Session["_err"] = "Este correo electrónico ya existe";
                                    }

                                    
                                    else
                                    {
                                        EEstudiante estud = new EEstudiante();
                                                estud.Carnet = txtCarnet.Text;
                                                estud.NumIdentificacion = long.Parse(txtNumIdent.Text);
                                                estud.Seccion = txtSeccion.Text;
                                                estud.Nombre = txtNombre.Text;
                                                estud.Apellido1 = txtApe1.Text;
                                                estud.Apellido2 = txtApe2.Text;
                                                estud.Genero = char.Parse(ddlGenero.Text);
                                                estud.Email = txtEmail.Text;
                                                estud.FechaIngreso = cldFechaIngreso.SelectedDate;
                                                estud.FechaNacimiento = cldFechaNacimiento.SelectedDate;
                                                estud.Distrito = int.Parse(txtIdDistrito.Text);
                                                estud.DirExact = txtDirExact.Text;
                                                estud.Activo = ckbActivo.Checked;
                                                estud.Borrado = ckbBorrado.Checked;
                                                if (lnE.actualizar(estud))
                                                {
                                                    Session["_exito"] = "Estudiante actualizado con éxito";
                                                    Response.Redirect("wfrListarEstudiantes.aspx", false);
                                                }
                                                else
                                                    Session["_err"] = "No se pudo modificar estudiante";
                                            
                                    }
                            
                                        Session["_err"] = "Este número de identificación ya existe";
                                }
                            }
                        }
                        else
                        {
                            if (estu.Carnet != txtCarnet.Text)
                            {
                                if (lnE.existeCarnet(txtCarnet.Text))
                                {
                                    if (estu.Email != txtEmail.Text)
                                    {
                                        if (lnE.existeEmail(txtEmail.Text))
                                        {
                                            EEstudiante estud = new EEstudiante();
                                            estud.Carnet = txtCarnet.Text;
                                            estud.NumIdentificacion = long.Parse(txtNumIdent.Text);
                                            estud.Seccion = txtSeccion.Text;
                                            estud.Nombre = txtNombre.Text;
                                            estud.Apellido1 = txtApe1.Text;
                                            estud.Apellido2 = txtApe2.Text;
                                            estud.Genero = char.Parse(ddlGenero.Text);
                                            estud.Email = txtEmail.Text;
                                            estud.FechaIngreso = cldFechaIngreso.SelectedDate;
                                            estud.FechaNacimiento = cldFechaNacimiento.SelectedDate;
                                            estud.Distrito = int.Parse(txtIdDistrito.Text);
                                            estud.DirExact = txtDirExact.Text;
                                            estud.Activo = ckbActivo.Checked;
                                            estud.Borrado = ckbBorrado.Checked;
                                            if (lnE.actualizar(estud))
                                            {
                                                Session["_exito"] = "Estudiante actualizado con éxito";
                                                Response.Redirect("wfrListarEstudiantes.aspx", false);
                                            }
                                            else
                                                Session["_err"] = "No se pudo modificar estudiante";
                                        }
                                        else
                                            Session["_err"] = "Este correo electrónico ya existe";
                                    }
                                    else
                                    {
                                        EEstudiante estud = new EEstudiante();
                                        estud.Carnet = txtCarnet.Text;
                                        estud.NumIdentificacion = long.Parse(txtNumIdent.Text);
                                        estud.Seccion = txtSeccion.Text;
                                        estud.Nombre = txtNombre.Text;
                                        estud.Apellido1 = txtApe1.Text;
                                        estud.Apellido2 = txtApe2.Text;
                                        estud.Genero = char.Parse(ddlGenero.Text);
                                        estud.Email = txtEmail.Text;
                                        estud.FechaIngreso = cldFechaIngreso.SelectedDate;
                                        estud.FechaNacimiento = cldFechaNacimiento.SelectedDate;
                                        estud.Distrito = int.Parse(txtIdDistrito.Text);
                                        estud.DirExact = txtDirExact.Text;
                                        estud.Activo = ckbActivo.Checked;
                                        estud.Borrado = ckbBorrado.Checked;
                                        if (lnE.actualizar(estud))
                                        {
                                            Session["_exito"] = "Estudiante actualizado con éxito";
                                            Response.Redirect("wfrListarEstudiantes.aspx", false);
                                        }
                                        else
                                            Session["_err"] = "No se pudo modificar estudiante";

                                    }
                                }
                                else
                                    Session["_err"] = "Este carnet de estudiante ya existe";

                            }
                            else
                            {
                                if (estu.Email != txtEmail.Text)
                                {
                                    if (lnE.existeEmail(txtEmail.Text))
                                    {
                                        EEstudiante estud = new EEstudiante();
                                        estud.Carnet = txtCarnet.Text;
                                        estud.NumIdentificacion = long.Parse(txtNumIdent.Text);
                                        estud.Seccion = txtSeccion.Text;
                                        estud.Nombre = txtNombre.Text;
                                        estud.Apellido1 = txtApe1.Text;
                                        estud.Apellido2 = txtApe2.Text;
                                        estud.Genero = char.Parse(ddlGenero.Text);
                                        estud.Email = txtEmail.Text;
                                        estud.FechaIngreso = cldFechaIngreso.SelectedDate;
                                        estud.FechaNacimiento = cldFechaNacimiento.SelectedDate;
                                        estud.Distrito = int.Parse(txtIdDistrito.Text);
                                        estud.DirExact = txtDirExact.Text;
                                        estud.Activo = ckbActivo.Checked;
                                        estud.Borrado = ckbBorrado.Checked;
                                        if (lnE.actualizar(estud))
                                        {
                                            Session["_exito"] = "Estudiante actualizado con éxito";
                                            Response.Redirect("wfrListarEstudiantes.aspx", false);
                                        }
                                        else
                                            Session["_err"] = "No se pudo modificar estudiante";
                                    }
                                    else
                                        Session["_err"] = "Este correo electrónico ya existe";
                                }


                                else
                                {
                                    EEstudiante estud = new EEstudiante();
                                    estud.Carnet = txtCarnet.Text;
                                    estud.NumIdentificacion = long.Parse(txtNumIdent.Text);
                                    estud.Seccion = txtSeccion.Text;
                                    estud.Nombre = txtNombre.Text;
                                    estud.Apellido1 = txtApe1.Text;
                                    estud.Apellido2 = txtApe2.Text;
                                    estud.Genero = char.Parse(ddlGenero.Text);
                                    estud.Email = txtEmail.Text;
                                    estud.FechaIngreso = cldFechaIngreso.SelectedDate;
                                    estud.FechaNacimiento = cldFechaNacimiento.SelectedDate;
                                    estud.Distrito = int.Parse(txtIdDistrito.Text);
                                    estud.DirExact = txtDirExact.Text;
                                    estud.Activo = ckbActivo.Checked;
                                    estud.Borrado = ckbBorrado.Checked;
                                    if (lnE.actualizar(estud))
                                    {
                                        Session["_exito"] = "Estudiante actualizado con éxito";
                                        Response.Redirect("wfrListarEstudiantes.aspx", false);
                                    }
                                    else
                                        Session["_err"] = "No se pudo modificar estudiante";

                                }

                                Session["_err"] = "Este número de identificación ya existe";
                            }
                        }
                    }
                    
                    else
                        Session["_err"] = "No hay cambios que actualizar";
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        /// <summary>
        /// Compara los datos de la página con el objeto del registro de estudiante
        /// guardardo en la base de datos
        /// </summary>
        /// <param name="estu"></param>
        /// <returns></returns>
        protected bool hayCambios(EEstudiante estu)
        {
            bool result = false;
            if (estu.Carnet != txtCarnet.Text)
                result = true;
            if (estu.NumIdentificacion != long.Parse(txtNumIdent.Text))
                result = true;
            if (estu.Seccion != txtSeccion.Text)
                result = true;
            if(estu.Nombre != txtNombre.Text)
                result = true;
            if(estu.Apellido1 != txtApe1.Text)
                result = true;
            if(estu.Apellido2 != txtApe2.Text)
                result = true;
            if(estu.Genero != char.Parse(ddlGenero.Text))
                result = true;
            if(estu.Email != txtEmail.Text)
                result = true;
            if(estu.FechaIngreso != cldFechaIngreso.SelectedDate)
                result = true;
            if(estu.FechaNacimiento != cldFechaNacimiento.SelectedDate)
                result = true;
            if(estu.Distrito != int.Parse(txtIdDistrito.Text))
                result = true;
            if(estu.DirExact != txtDirExact.Text)
                result = true;
            if(estu.Activo != ckbActivo.Checked)
                result = true;
            if(estu.Borrado != ckbBorrado.Checked)
                result = true;
            return result;
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