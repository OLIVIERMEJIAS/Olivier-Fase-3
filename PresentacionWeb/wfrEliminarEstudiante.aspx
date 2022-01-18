<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrEliminarEstudiante.aspx.cs" Inherits="PresentacionWeb.wfrEliminarEstudiante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width: fit-content">
        <h1 class="text-center bg-info">Confirmación de eliminación</h1>
        <br />
        <div class="card-body">
            <div class="card-header">
                
                <asp:Label ID="lblCarnet" runat="server" Text="Carnet: "></asp:Label>
                <br />
                <asp:Label ID="lblNumIdent" runat="server" Text="Número de Identificación: "></asp:Label>
                <br />
                <asp:Label ID="lblSeccion" runat="server" Text="Sección: "></asp:Label>
                <br />
                <asp:Label ID="lblNombre" runat="server" Text="Nombre: "></asp:Label>
                <br />
                <asp:Label ID="lblApellido1" runat="server" Text="Primer Apellido: "></asp:Label>
                <br />
                <asp:Label ID="lblApellido2" runat="server" Text="Segundo Apellido: "></asp:Label>
                <br />
                <asp:Label ID="lblEmail" runat="server" Text="Dirección de correo electrónico: "></asp:Label>
                <br />
                <asp:Label ID="lblGenero" runat="server" Text="Género: "></asp:Label>
                <br />
                <asp:Label ID="lblFechaIng" runat="server" Text="Fecha de Ingreso "></asp:Label>
                <br />
                <asp:Label ID="lblFechaNac" runat="server" Text="Fecha de Nacimiento: "></asp:Label>
                <br />
                <asp:Label ID="lblDistrito" runat="server" Text="Distrito: "></asp:Label>
                <br />
                <asp:Label ID="lblDirec" runat="server" Text="Dirección Exacta "></asp:Label>
                <br />
                <asp:Label ID="lblActivo" runat="server" Text="Activo: "></asp:Label>
                <asp:CheckBox ID="ckbActivo" runat="server" />
                <br />
                <asp:Label ID="lblBorrado" runat="server" Text="Borrado: "></asp:Label>
                <asp:CheckBox ID="ckbBorrado" runat="server" />
                <asp:Button Cssclass="btn btn-warning" ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />
                <asp:Button Cssclass="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
            </div>
        </div>

    </div>
</asp:Content>
