<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrBuzon.aspx.cs" Inherits="PresentacionWeb.wfrBuzon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container bg-info card-header text-center">
        <h1>Bienvenid@ a su Buzón de Permisos</h1>
    </div>

    <br />
    <br />
    <% if (Session["_profesor"] != null) { %>
            <div class="container bg-info card-header text-center">
                <h1>Permisos Pendientes</h1>
            </div>
            <br />
            <div class="container">
                <asp:GridView ID="gdvPendientesP" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="permisoId" HeaderText="permisoId" Visible="False" />
                        <asp:BoundField DataField="profesor" HeaderText="Profesor" />
                        <asp:BoundField DataField="materia" HeaderText="Materia" />
                        <asp:BoundField DataField="estudiante" HeaderText="Estudiante" />
                        <asp:BoundField DataField="notaA" HeaderText="Nota Actual" />
                        <asp:BoundField DataField="notaR" HeaderText="Nota de Reemplazo" />
                        <asp:BoundField DataField="EstadoA" HeaderText="Estado Actual" />
                        <asp:BoundField DataField="EstadoR" HeaderText="Estado de Reemplazo" />
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="motivo" HeaderText="Motivo del Cambio" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </div>
        <% 
            }%>
    <% if (Session["_director"] != null) { %>
            <div class="container bg-info card-header text-center">
                <h1>Permisos Pendientes</h1>
            </div>
            <br />
            <div class="container">
                <asp:GridView ID="gdvpendientesD" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAceptar" runat="server" CommandArgument='<%# Eval("permisoId").ToString() %>' ForeColor="Green" OnCommand="lnkAceptar_Command">Aceptar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRechazar" runat="server" CommandArgument='<%# Eval("permisoId").ToString() %>' ForeColor="Red" OnCommand="lnkRechazar_Command">Rechazar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="permisoId" HeaderText="permisoId" Visible="False" />
                        <asp:BoundField DataField="profesor" HeaderText="Profesor" />
                        <asp:BoundField DataField="materia" HeaderText="Materia" />
                        <asp:BoundField DataField="estudiante" HeaderText="Estudiante" />
                        <asp:BoundField DataField="seccion" HeaderText="Sección" />
                        <asp:BoundField DataField="notaA" HeaderText="Nota Actual" />
                        <asp:BoundField DataField="notaR" HeaderText="Nota de Reemplazo" />
                        <asp:BoundField DataField="EstadoA" HeaderText="Estado Actual" />
                        <asp:BoundField DataField="EstadoR" HeaderText="Estado de Reemplazo" />
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="motivo" HeaderText="Motivo del Cambio" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </div>
        <% 
            }%>

    <br />
    <br />
    <div class="container bg-info card-header text-center">
        <h1>Permisos Aceptados</h1>
    </div>
    <br />
    <div class="container">
        <asp:GridView ID="gdvAceptados" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="permisoId" HeaderText="permisoId" Visible="False" />
                <asp:BoundField DataField="profesor" HeaderText="Profesor" />
                <asp:BoundField DataField="materia" HeaderText="Materia" />
                <asp:BoundField DataField="estudiante" HeaderText="Estudiante" />
                <asp:BoundField DataField="seccion" HeaderText="Sección" />
                <asp:BoundField DataField="notaA" HeaderText="Nota Actual" />
                <asp:BoundField DataField="notaR" HeaderText="Nota de Reemplazo" />
                <asp:BoundField DataField="EstadoA" HeaderText="Estado Actual" />
                <asp:BoundField DataField="EstadoR" HeaderText="Estado de Reemplazo" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="motivo" HeaderText="Motivo del Cambio" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
    </div>
    <br />
    <br />
    <div class="container bg-info card-header text-center">
        <h1>Permisos Rechazdos</h1>
    </div>
    <br />
    <div class="container">
        <asp:GridView ID="gdvRechazados" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="permisoId" HeaderText="permisoId" Visible="False" />
                <asp:BoundField DataField="profesor" HeaderText="Profesor" />
                <asp:BoundField DataField="materia" HeaderText="Materia" />
                <asp:BoundField DataField="estudiante" HeaderText="Estudiante" />
                <asp:BoundField DataField="seccion" HeaderText="Sección" />
                <asp:BoundField DataField="notaA" HeaderText="Nota Actual" />
                <asp:BoundField DataField="notaR" HeaderText="Nota de Reemplazo" />
                <asp:BoundField DataField="EstadoA" HeaderText="Estado Actual" />
                <asp:BoundField DataField="EstadoR" HeaderText="Estado de Reemplazo" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="motivo" HeaderText="Motivo del Cambio" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
    </div>
    <br />
    <br />
</asp:Content>


