<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrHorarios.aspx.cs" Inherits="PresentacionWeb.wfrHorarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card-header text-center">   
            <h1>Generación y Consulta de Horarios</h1>
        </div>
    </div>
        <br />
        <%--alerts--%>
        <% if (Session["_exito"] != null) { %>
            <div class="alert alert-success" role="alert">
                <%= Session["_exito"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_exito"] = null;
          }%>

        <% if (Session["_wrn"] != null) { %>
            <div class="alert alert-warning" role="alert">
                <%= Session["_wrn"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_wrn"] = null;
          }%>

        <% if (Session["_err"] != null) { %>
            <div class="alert alert-danger" role="alert">
                <%= Session["_err"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_err"] = null;
          }%>
        <div class="container" style="width: fit-content">
            <asp:Button CssClass="btn btn-outline-primary container" ID="btnGenerar" runat="server" Text="Generar Horarios Nuevos" OnClick="btnGenerar_Click" />
        </div>
        <br />
        <div class="container" style="width: fit-content">
            <asp:DropDownList ID="ddlSecciones" runat="server">
                <asp:ListItem Selected="True">7-1</asp:ListItem>
                <asp:ListItem>7-2</asp:ListItem>
                <asp:ListItem>7-3</asp:ListItem>
                <asp:ListItem>7-4</asp:ListItem>
                <asp:ListItem>8-1</asp:ListItem>
                <asp:ListItem>8-2</asp:ListItem>
                <asp:ListItem>8-3</asp:ListItem>
                <asp:ListItem>9-1</asp:ListItem>
                <asp:ListItem>9-2</asp:ListItem>
                <asp:ListItem>10-1</asp:ListItem>
                <asp:ListItem>10-2</asp:ListItem>
                <asp:ListItem>11-1</asp:ListItem>
                <asp:ListItem>12-1</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Sección incompleta!! Por favor, complétela." ValidationGroup="5" ControlToValidate="txtSeccion" ForeColor="Red">*</asp:RequiredFieldValidator>
       </div>
    <br />
    <br />
    <asp:GridView ID="gdvLunes" runat="server"></asp:GridView>
        
</asp:Content>
