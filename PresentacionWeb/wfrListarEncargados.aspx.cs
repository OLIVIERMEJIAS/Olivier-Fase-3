﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace PresentacionWeb
{
    public partial class wfrListarEncargados : System.Web.UI.Page
    {
        LNEncargado lnE = new LNEncargado(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["_estudianteId"] != null)
                {
                    int estuId = int.Parse(Session["_estudianteId"].ToString());
                    cargarEncargadosPorEstudiante(estuId);
                }
                else
                    cargarEncargados();
                   
            }
        }
        protected void cargarEncargadosPorEstudiante(int estuId)
        {
            try
            {
                gdvEncargados.DataSource = lnE.listarPorEstudiante(estuId);
                gdvEncargados.DataBind();
            }
            catch (Exception ex)
            {

                Session["err"] = ex.Message;
            }
        }
        protected void cargarEncargados(string condicion = "")
        {
            try
            {
                gdvEncargados.DataSource = lnE.listar(condicion);
                gdvEncargados.DataBind();
            }
            catch (Exception ex)
            {

                Session["err"] = ex.Message;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                cargarEncargados(txtNombre.Text);
            }
            catch (Exception ex)
            {

                Session["err"] = ex.Message;
            }
        }
    }
}