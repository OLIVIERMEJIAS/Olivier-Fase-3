<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrCalificaciones.aspx.cs" Inherits="PresentacionWeb.wfrCalificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container card-body">
        <div class="card-header">
            <h1 class="bg-info text-center">Calificaciones por estudiante</h1>
        </div>
        <br />
        <br />
        <div class="container">
            <asp:GridView ID="gdvCalificaciones" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("calificacionId").ToString() %>' ForeColor="Red" OnCommand="lnkEliminar_Command">Eliminar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkModificar" runat="server" CausesValidation="False" CommandArgument='<%# Eval("calificacionId").ToString() %>' ForeColor="Green" OnCommand="lnkModificar_Command">Modificar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="calificacionId" HeaderText="aistenciaId" Visible="False" />
                    <asp:BoundField DataField="materia" HeaderText="Materia" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre del Estudiante" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="calificacion" HeaderText="Calificación" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:BoundField DataField="trimestre" HeaderText="Trimestre" />
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
        <asp:Button Cssclass="btn btn-success" ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />
    </div>
</asp:Content>
