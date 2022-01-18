<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrListarEncargados.aspx.cs" Inherits="PresentacionWeb.wfrListarEncargados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card-header bg-info text-center" >
        <h1>Encargados</h1>
    </div>
    <div class="container">
        <div class="card-header text-center">   
            <h2>Busque por nombre:</h2>
            <br />
            <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"  ReadOnly="False" ValidationGroup="1" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar un nombre" ControlToValidate="txtNombre" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
            <br />
            <asp:Button class="btn btn-outline-info" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" ValidationGroup="1" />
            <br />
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" ForeColor="Red" />
        </div>
    </div>
    <br />
    
    <div class="container">
        <asp:GridView ID="gdvEncargados" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("encargadoId").ToString() %>' ForeColor="Red">Eliminar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkModificar" runat="server" CommandArgument='<%# Eval("encargadoId").ToString() %>' ForeColor="Green">Modificar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkAsignarEstudiantees" runat="server" CommandArgument='<%# Eval("encargadoId").ToString() %>' ForeColor="Blue">Asignar Estudiantes</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="encargadoId" HeaderText="Encargado Id" Visible="False" />
                <asp:BoundField DataField="numeroIdentificacion" HeaderText="Cédula" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre del Estudiante" />
                <asp:BoundField DataField="email" HeaderText="Correo Electrónico" />
                <asp:BoundField DataField="genero" HeaderText="Género" />
                <asp:BoundField DataField="fechaIngreso" HeaderText="Fecha de Ingreso" />
                <asp:BoundField DataField="fechaNacimiento" HeaderText="Fecha de Nacimiento" />
                <asp:BoundField DataField="dirExact" HeaderText="Dirección Exacta" />
                <asp:CheckBoxField DataField="activo" HeaderText="Activo" ReadOnly="True" />
                <asp:CheckBoxField DataField="borrado" HeaderText="Borrado" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
    </div>

    <br />

     
</asp:Content>
