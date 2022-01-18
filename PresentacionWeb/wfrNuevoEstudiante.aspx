<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrNuevoEstudiante.aspx.cs" Inherits="PresentacionWeb.wfrNuevoEstudiante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card-header">
        <h1 class="text-center">Mantenimiento de Estudiantes</h1>
    </div>
    <br />
    <div class="container bg-info" style="width: fit-content">
        <div class="container" style="width: fit-content">
        <asp:Label CssClass="form" ID="lblCarnet" runat="server" Text="Carnet: "></asp:Label>
        <asp:TextBox CssClass="form" ID="txtCarnet" runat="server" ReadOnly="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Se requiere Carnet" ValidationGroup="1" ControlToValidate="txtCarnet" ForeColor="Red">*</asp:RequiredFieldValidator>

        <asp:Label CssClass="form" ID="lblNumIdent" runat="server" Text="Número de Identificación: "></asp:Label>
        <asp:TextBox CssClass="form" ID="txtNumIdent" runat="server" ReadOnly="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Se requiere Número de Identificación" ValidationGroup="1" ControlToValidate="txtNumIdent" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <br />
        <div class="container" style="width: fit-content">
        <div class="input-group mb-3">
            <asp:Label CssClass="form" ID="lblSección" runat="server" Text="Seleccione la sección: "></asp:Label>

            <asp:TextBox CssClass="form-control" ID="txtSeccion" runat="server"  ReadOnly="True" aria-describedby="btnModalSec" Text="7-1"></asp:TextBox>
                
            <button class="btn btn-outline-primary" type="button" id="btnModalSec"
            data-bs-toggle="modal"
            data-bs-target="#secModal" style="width: 62px">Buscar</button>
        </div>
        </div>
        </div>
        <br />
        <div class="container" style="width: fit-content">
        <asp:Label CssClass="form" ID="lblNombre" runat="server" Text="Nombre: "></asp:Label>
        <asp:TextBox CssClass="form" ID="txtNombre" runat="server" ReadOnly="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Se requiere Nombre" ValidationGroup="1" ControlToValidate="txtNombre" ForeColor="Red">*</asp:RequiredFieldValidator>

        <asp:Label CssClass="form" ID="lblApe1" runat="server" Text="Primer Apellido: "></asp:Label>
        <asp:TextBox CssClass="form" ID="txtApe1" runat="server" ReadOnly="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Se requiere Primer Apellido" ValidationGroup="1" ControlToValidate="txtApe1" ForeColor="Red">*</asp:RequiredFieldValidator>

        <asp:Label CssClass="form" ID="lblApe2" runat="server" Text="Segundo Apellido: "></asp:Label>
        <asp:TextBox CssClass="form" ID="txtApe2" runat="server" ReadOnly="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Se requiere Segundo Apellido" ValidationGroup="1" ControlToValidate="txtApe2" ForeColor="Red">*</asp:RequiredFieldValidator>

        <br />
        <br />
        <div class="container" style="width: fit-content">

        <asp:Label CssClass="form" ID="lblEmail" runat="server" Text="Email: "></asp:Label>
        <asp:TextBox CssClass="form" ID="txtEmail" runat="server" ReadOnly="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Se requiere Email" ValidationGroup="1" ControlToValidate="txtEmail" ForeColor="Red">*</asp:RequiredFieldValidator>

        <asp:Label CssClass="form" ID="lblGenero" runat="server" Text="Género: "></asp:Label>
        <asp:DropDownList ID="ddlGenero" runat="server">
            <asp:ListItem Selected="True">F</asp:ListItem>
            <asp:ListItem>M</asp:ListItem>
            <asp:ListItem>P</asp:ListItem>
        </asp:DropDownList>
        </div>
        </div>
        <br />
        <div class="container" style="width: fit-content">
       <asp:Label CssClass="form" ID="lblFechaIng" runat="server" Text="Fecha de Ingreso: "></asp:Label>
        <asp:Calendar ID="cldFechaIngreso" runat="server"></asp:Calendar>
        <br />
        <asp:Label CssClass="form" ID="lblFechaNacimiento" runat="server" Text="Fecha de Nacimiento: "></asp:Label>
        <asp:Calendar ID="cldFechaNacimiento" runat="server"></asp:Calendar>
        <br />
        </div>
        <div class="container" style="width: fit-content">
        <div class="input-group mb-3">
            <asp:Label CssClass="form" ID="lblDistrito" runat="server" Text="Seleccione el distrito: "></asp:Label>
            <asp:TextBox CssClass="form-control" ID="txtIdDistrito" runat="server"  Visible="false"></asp:TextBox>

            <asp:TextBox CssClass="form-control" ID="txtDistrito" runat="server"  ReadOnly="True" aria-describedby="btnModalDist" ValidationGroup="5"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Se requiere distrito" ValidationGroup="1" ControlToValidate="txtIdDistrito">*</asp:RequiredFieldValidator>
            <button class="btn btn-outline-primary" type="button" id="btnModalDist"
            data-bs-toggle="modal"
            data-bs-target="#disModal" style="width: 62px">Buscar</button>
        </div>
            <div class="container" style="width: fit-content">
                <asp:Label CssClass="form" ID="lblDirExact" runat="server" Text="Dirección Exacta: "></asp:Label>
                <asp:TextBox CssClass="form" ID="txtDirExact" runat="server" ReadOnly="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Se requiere Email" ValidationGroup="1" ControlToValidate="txtDirExact" ForeColor="Red">*</asp:RequiredFieldValidator>

            </div>
            <div class="container" style="width: fit-content">

            <asp:Label ID="lblActivo" runat="server" Text="Activo"></asp:Label>
            <asp:CheckBox ID="ckbActivo" runat="server" />

            <asp:Label ID="lblBorrado" runat="server" Text="Borrado"></asp:Label>
            <asp:CheckBox ID="ckbBorrado" runat="server" />
            </div>
        </div>
    </div>
    <br />

    <div class="container bg-info">
        <br />    
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" ForeColor="Red" />

    </div>
    <br />
    <br />
    <div class="container" style="width: fit-content">
        <asp:Button class="form btn btn-success" ID="btnAsignar" runat="server" Text="Asignar" OnClick="btnAsignar_Click" ValidationGroup="1" />
        <asp:Button class="form btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />

    </div>


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

     <%--modal distritos--%>
    <div class="modal" tabindex="-1" id="disModal">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Buscar Distrito</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="row mt-3">
                <div class="col-auto">
                    <asp:Label ID="Label1" runat="server" Text="Distrito"></asp:Label>
                </div>
            
            </div>
                <br />
                <asp:GridView ID="gdvDistritos" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="100%" PageSize="20">
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSeleccionarDistrito" runat="server" CommandArgument='<%# Eval("distritoId").ToString() %>' OnCommand="lnkSeleccionarDistrito_Command">Seleccionar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="distritoId" HeaderText="Id" Visible="False" />
                    <asp:BoundField DataField="distrito" HeaderText="Distrito" Visible="True" />
                    <asp:BoundField DataField="canton" HeaderText="Cantón" Visible="True" />

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
