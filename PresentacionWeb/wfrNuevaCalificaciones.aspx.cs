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
    public partial class wfrNuevaCalificaciones : System.Web.UI.Page
    {
        LNCalificacion lnC = new LNCalificacion(Config.getCadConec);
        LNTrimestre lnT = new LNTrimestre(Config.getCadConec);
        LNPermiso lnP = new LNPermiso(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LNEstudiante lnE = new LNEstudiante(Config.getCadConec);
                //el proceso de si se modifica una calificación o se crea 
                //se determina con la existencia de una variable de sesión
                if (Session["_nuevaCalificacion"] != null)
                {//insertar nueva
                    try
                    {//se cargan los datos necesarios para mostrar en
                     //los controles, se obtinen el nombre y con variables static 
                     //globales se cargan otros detalles
                        int estudianteId = int.Parse(Session["_nuevaCalificacion"].ToString());
                        string nombreEst = lnE.existe(estudianteId);
                        if (nombreEst != "")
                            txtEstudiante.Text = nombreEst;
                        txtIdMateria.Text = Config.MateriaId.ToString();
                        txtMateria.Text = Config.MateriaNombre;
                        txtFecha.Text = DateTime.Today.ToString();


                    }
                    catch (Exception ex)
                    {

                        Session["_err"] = ex.Message;
                    }
                }
                else
                {//modificar
                    int calificacionId = int.Parse(Session["_modificarCalificacion"].ToString());
                    try
                    {//con el Id de la  calificación en la variable de
                     //sesión que viene del formulario
                        //wfrCalificaciones se carga un o bjeto ECalificacion 
                        //con los datos actuales de la misma
                        //variables globales de la clase Config también son usadas
                        ECalificacion cali;
                        cali = lnC.listar(calificacionId);
                        int estudianteId = cali.EstudianteID;
                        string nombreEst = lnE.existe(estudianteId);
                        if (nombreEst != "")
                            txtEstudiante.Text = nombreEst;
                        txtIdMateria.Text = Config.MateriaId.ToString();
                        txtMateria.Text = Config.MateriaNombre;
                        txtFecha.Text = cali.FechaIngreso;
                        txtCalificacion.Text = cali.Calificacion.ToString();
                        ddlEstados.Text = cali.Estado;
                        ddlTrimestre.Text = cali.TrimestreID.ToString();
                        ddlTrimestre.Enabled = false;
                        //el trimestre no se permite cambiar
                    }
                    catch (Exception ex)
                    {

                        Session["_err"] = ex.Message;
                    }
                }
            }
        }
        //al cancelar un mantenimento de calificaciones
        //se procede a anular las variables de sesiones
        //posibles en este formulario
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["_nuevaCalificacion"] = null;
            Session["_modificarCalificacion"] = null;
            Response.Redirect("wfrListarCalificaciones.aspx", false);
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {//al clicker al botón asignar se procede a validar si es
         //modificación a inserción desde cero
            if (Session["_nuevaCalificacion"] != null)
            {   //creación de un nuevo registro

                int estudianteId = int.Parse(Session["_nuevaCalificacion"].ToString());
                //obtención del Id del estudiante,
                //se procede a crear un objeto ECalificacion para insertar
                ECalificacion cali = new ECalificacion(0, estudianteId, Config.MateriaId, decimal.Parse(txtCalificacion.Text), ddlEstados.Text, byte.Parse(ddlTrimestre.Text));
                try
                {   //si se agrega con éxito se notifica y redirecciona
                    //a la página de la lista de registros de calificaciones
                    //la variable de sesión de anula
                    if (lnC.agregar(cali))
                    {
                        Session["_exito"] = "Calificación Agregada Exitosamente!!";
                        Response.Redirect("wfrCalificaciones.aspx", false);
                        Session["_nuevaCalificacion"] = null;
                    }
                    else
                    {
                        Session["_err"] = "No se logró insertar la calificación";
                        Session["_nuevaCalificacion"] = null;
                    }


                }
                catch (Exception ex)
                {
                    Session["_nuevaCalificacion"] = null;
                    Session["_err"] = ex.Message;
                }
            }
            else
            {// modificar la calificación
                //obtención del Id de la calificación
                int calificacionId = int.Parse(Session["_modificarCalificacion"].ToString());
                try
                {
                    //se procede a la obtención del número de trimestre
                    //de la calificación para saber si es preciso enviar
                    //al diector un permiso de cambio
                    byte trimId = byte.Parse(ddlTrimestre.Text);
                    //obtenión de la fecha de fin del trimestre
                    DateTime fechaFin = lnT.listar(trimId).FechaFin;
                    //comparación, si el resultado es menor a cero, 
                    //la fecha de fin es mayor a la actual, estará vencido el
                    //trimestre
                    if (DateTime.Compare(fechaFin, DateTime.Today) > 0)
                    {//no vencido
                       //se busca la calificación para datos de comparación
                       
                        ECalificacion cali;
                        cali = lnC.listar(calificacionId);
                        //si no hay cambios en los datos , se notifica
                        //de haber siguen las instrucciones
                        if (cali.Estado != ddlEstados.Text ||
                            cali.Calificacion != decimal.Parse(txtCalificacion.Text))
                        {
                            //si hubo un cambio en el estado de la calificación
                            //se reasigna al atributo del objeto
                            if (cali.Estado != ddlEstados.Text)
                                cali.Estado = ddlEstados.Text;
                            //si hubo cambio en la calificación como tal, se reasigna
                            if (cali.Calificacion != decimal.Parse(txtCalificacion.Text))
                                cali.Calificacion = decimal.Parse(txtCalificacion.Text);
                            //el objeto se envia a actualizar
                            if (lnC.actualizar(cali))
                            {
                                Session["_exito"] = "Se ha modificado la calificacion con éxito!!";
                                Session["_modificarCalificacion"] = null;
                                //se anula la variable de sesión de modificación
                                Response.Redirect("wfrCalificaciones.aspx", false);
                                //se redirije a la páginas de los registros de calificaciones
                            }
                            else
                            {   //aunque no se hubiera podido actualizar, la variable se anula
                                //y se redirije a la página de calificaciones
                                Session["_err"] = "No se ha podido modificar la calificación!!";
                                Session["_modificarCalificacion"] = null;
                                Response.Redirect("wfrCalificaciones.aspx", false);
                            }
                        }
                        else
                        {
                            Session["_wrn"] = "No existen cambios que modificar!";
                        }

                    }
                    else
                    {//en caso de haber terminado el trimestre
                        //se notifica
                        
                        Session["_err"] = "No puede modificar esta calificación debido" +
                            "a que ya terminó este trimestre!! Desea enviar una" +
                            "solicitud de permiso al director??";
                        //se envia el Id de la calificación y a la vez se anula la variable
                        //de modificación con el Id de la misma
                        Session["_permiso"] = Session["_modificarCalificacion"].ToString();
                        Session["_modificarCalificacion"] = null;
                        //se crea una cookie con dos variables enviando
                        //la calificación y el estado que se estaba
                        //intentando agregar como los cambios
                        HttpCookie cookie = new HttpCookie("MyCookie");
                        cookie["_calificacion"] = txtCalificacion.Text;
                        cookie["_estadoR"] = ddlEstados.Text;
                        Response.Cookies.Add(cookie);
                        //se redirije a la página de envio de permiso de cambio
                        //fuera de trimestre
                        Response.Redirect("wfrPermiso.aspx", false);
                    }
                }
                catch (Exception ex)
                {

                    Session["_err"] = ex.Message;
                }
            }

        }
    }
}
        
