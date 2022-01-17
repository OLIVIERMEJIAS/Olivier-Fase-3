<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrListarEstudiantes.aspx.cs" Inherits="PresentacionWeb.wfrListarEstudiantes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card-header bg-info text-center" >
        <h1>Estudiantes</h1>
    </div>
    <div class="container">
        <div class="card-header text-center">   
            <h2>Seleccione una sección, para ver los estudiantes:</h2>
        </div>
    </div>
    <br />
    <div class="container" style="width: fit-content">
        <asp:Label ID="lblSec" runat="server" Text="Sección:"></asp:Label>

        <div class="input-group mb-3">
            <asp:TextBox CssClass="form-control" ID="txtSeccion" runat="server"  ReadOnly="True" aria-describedby="btnModalSec" ValidationGroup="5" Text="7-1"></asp:TextBox>
                
            <button class="btn btn-outline-primary" type="button" id="btnModalSec"
            data-bs-toggle="modal"
            data-bs-target="#secModal" style="width: 62px">Buscar</button>
        </div>
    </div>
    <br />
    <br />
    <div class="container">
        <asp:GridView ID="gdvEstudiantes" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" CellSpacing="2">
            <Columns>
                
                <asp:BoundField DataField="estudianteId" HeaderText="Estudiante Id" Visible="False" />
                <asp:BoundField DataField="carnet" HeaderText="Cárnet" />
                <asp:BoundField DataField="cedula" HeaderText="Cédula" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre del Estudiante" />
                <asp:BoundField DataField="email" HeaderText="Correo Electrónico" />
                <asp:BoundField DataField="genero" HeaderText="Género" />
                <asp:BoundField DataField="fechaIngreso" HeaderText="Fecha de Ingreso" />
                <asp:BoundField DataField="fechaNacimiento" HeaderText="Fecha de Nacimiento" />
                <asp:BoundField DataField="dirExact" HeaderText="Dirección Exacta" />
                <asp:CheckBoxField DataField="activo" HeaderText="Activo" ReadOnly="True" />
                <asp:CheckBoxField DataField="borrado" HeaderText="Borrado" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
    </div>

    <br />

     <%--modal secciones--%>
    <div class="modal" tabindex="-1" id="secModal">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Buscar Sección</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="row mt-3">
                <div class="col-auto">
                    <asp:Label ID="Label5" runat="server" Text="Sección"></asp:Label>
                </div>
            
            </div>
                <br />
                <asp:GridView ID="gdvSecciones" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="100%" PageSize="20">
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSeleccionarSeccion" runat="server" CommandArgument='<%# Eval("seccion").ToString() %>' OnCommand="lnkSeleccionarSeccion_Command">Seleccionar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="seccion" HeaderText="Sección" Visible="True" />
                </Columns>
                <FooterStyle BackColor="Tan" />
                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
            </asp:GridView>
            </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
          </div>
        </div>
      </div>
    </div>
</asp:Content>
