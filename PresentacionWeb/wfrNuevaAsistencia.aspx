<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrNuevaAsistencia.aspx.cs" Inherits="PresentacionWeb.wfrNuevaAsistencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card-header">
        <h1 class="text-center">Mantenimiento de Asistencias</h1>
    </div>
    <br />
    <div class="container bg-info" style=""width: fit-content">
        <asp:Label CssClass="form" ID="lblEstudiante" runat="server" Text="Nombre del Estudiante"></asp:Label>
        <asp:TextBox CssClass="form" ID="txtEstudiante" runat="server" ReadOnly="True"></asp:TextBox>
        <asp:TextBox CssClass="form" ID="txtIdMateria" runat="server" Visible="False" ReadOnly="True"></asp:TextBox>
        <asp:Label CssClass="form" ID="lblMateria" runat="server" Text="Nombre de la Materia"></asp:Label>
        <asp:TextBox CssClass="form" ID="txtMateria" runat="server"></asp:TextBox>
        <asp:DropDownList ID="ddlEstados" runat="server">
            <asp:ListItem Selected="True">PR</asp:ListItem>
            <asp:ListItem>TA</asp:ListItem>
            <asp:ListItem>AI</asp:ListItem>
            <asp:ListItem>AJ</asp:ListItem>
        </asp:DropDownList>
    </div>
        <br />
        <br />
        <div class="container" style="width: fit-content">
            <asp:Button class="form btn btn-success" ID="btnAsignar" runat="server" Text="Asignar" OnClick="btnAsignar_Click" />
            <asp:Button class="form btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />

        </div>
     
</asp:Content>
