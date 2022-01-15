<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrNuevaCalificaciones.aspx.cs" Inherits="PresentacionWeb.wfrNuevaCalificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card-header">
        <h1 class="text-center">Mantenimiento de Calificaciones</h1>
    </div>
    <br />
    <div class="container bg-success" style=""width: fit-content">
        <asp:Label CssClass="form" ID="lblEstudiante" runat="server" Text="Nombre del Estudiante"></asp:Label>
        <asp:TextBox CssClass="form" ID="txtEstudiante" runat="server" ReadOnly="True"></asp:TextBox>
        <asp:TextBox CssClass="form" ID="txtIdMateria" runat="server" Visible="False" ReadOnly="True"></asp:TextBox>
        <asp:Label CssClass="form" ID="lblMateria" runat="server" Text="Nombre de la Materia"></asp:Label>
        <asp:TextBox CssClass="form" ID="txtMateria" runat="server" ReadOnly="True"></asp:TextBox>
        <asp:TextBox CssClass="form" ID="txtFecha" ReadOnly="True" runat="server"></asp:TextBox>
        <asp:Label CssClass="form" ID="lblCalificacion" runat="server" Text="Calificación"></asp:Label>
        <asp:TextBox CssClass="form" ID="txtCalificacion" runat="server" ReadOnly="True"></asp:TextBox>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="El rango ingresado de calificación no es valido. Válidos: 0-100" Type="Double" MaximumValue="100.00" MinimumValue="0.00" ValidationGroup="1">*</asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar Califcación" ControlToValidate="txtCalificacion" ValidationGroup="1">*</asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlEstados" runat="server">
            <asp:ListItem Selected="True">APR</asp:ListItem>
            <asp:ListItem>APL</asp:ListItem>
            <asp:ListItem>REP</asp:ListItem>
        </asp:DropDownList>
        <asp:Label CssClass="form" ID="lblTrimestre" runat="server" Text="Trimestre"></asp:Label>
        <asp:DropDownList ID="ddlTrimestre" runat="server">
            <asp:ListItem Selected="True">1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" />
        <br />
        <br />
        <div class="container" style="width: fit-content">
            <asp:Button class="form btn btn-success" ID="btnAsignar" runat="server" Text="Asignar" OnClick="btnAsignar_Click" ValidationGroup="1" />
            <asp:Button class="form btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />

        </div>
</asp:Content>
