<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrPermiso.aspx.cs" Inherits="PresentacionWeb.wfrPermiso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container bg-info" style="width: fit-content">
        <h1 class="card-header text-center">Solicitud de Permiso de Cambio de Calificación a Dirección</h1>
        <h2 class="card-header text-center">Puesto que el Trimestre ya Terminó</h2>
        <br />
        <asp:Label ID="lblEstudiante" runat="server" Text="Nombre del Estudiante: "></asp:Label>
        <br />
        <asp:Label ID="lblProfesor" runat="server" Text="Nombre del Profesor: "></asp:Label>
        <br />
        <asp:Label ID="lblMateria" runat="server" Text="Nombre de la Materia: "></asp:Label>
        <br />
        <asp:Label ID="lblNotaA" runat="server" Text="Nota Actual: "></asp:Label>
        <br />
        <asp:Label ID="lblNotaR" runat="server" Text="Ingrese Calificación de Reemplazo: "></asp:Label>
        <asp:TextBox ID="txtNotaR" runat="server" ReadOnly="True"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar una calificación de reemplazo" ControlToValidate="txtNotaR" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Valores Permitidos solo: 0-100" ControlToValidate="txtNotaR" ValidationGroup="1" Type="Double" MaximumValue="100" MinimumValue="0" ForeColor="Red">*</asp:RangeValidator>
         <br />
        <asp:Label ID="lblEstadoA" runat="server" Text="Estado Actual de la Calificación: "></asp:Label>
         <br />
        <asp:Label ID="lblEstadoR" runat="server" Text="Estado de Reemplazo de la Calificación: "></asp:Label>
        <asp:TextBox ID="txtEstadoR" runat="server" ReadOnly="True"></asp:TextBox>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Ingrese el motivo de Cambio: "></asp:Label>
        <asp:TextBox ID="txtMotivo" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar un motivo" ControlToValidate="txtMotivo" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" ForeColor="Red" />
        <br />
        <br />
        <asp:Button CssClass="btn btn-outline-primary" ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" ValidationGroup="1" />
        <asp:Button CssClass="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
    </div>
</asp:Content>
