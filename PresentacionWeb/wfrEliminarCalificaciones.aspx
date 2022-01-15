<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrEliminarCalificaciones.aspx.cs" Inherits="PresentacionWeb.wfrEliminarCalificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width: fit-content">
        <h1 class="text-center bg-info">Confirmación de eliminación</h1>
        <br />
        <div class="card-body">
            <div class="card-header">
                
                <asp:Label ID="lblEstudiante" runat="server" Text="Nombre del Estudiante: "></asp:Label>
                <br />
                <asp:Label ID="lblFecha" runat="server" Text="Fecha de la calificación: "></asp:Label>
                <br />
                <asp:Label ID="lblCalificacion" runat="server" Text="Resultado de la calificación: "></asp:Label>
                <br />
                <asp:Label ID="lblEstado" runat="server" Text="Estado de la calificación: "></asp:Label>
                <br />
                <asp:Label ID="lblTrimestre" runat="server" Text="Número de Trimestre: "></asp:Label>
                <br />
                <asp:Button Cssclass="btn btn-warning" ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />
                <asp:Button Cssclass="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
            </div>
        </div>

    </div>
</asp:Content>
