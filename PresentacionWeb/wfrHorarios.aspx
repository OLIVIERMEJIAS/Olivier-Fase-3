<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrHorarios.aspx.cs" Inherits="PresentacionWeb.wfrHorarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card-header text-center">   
            <h1>Generación y Consulta de Horarios</h1>
        </div>
    </div>
        <br />
       
        <div class="container" style="width: fit-content">
            <asp:Button CssClass="btn btn-outline-primary container" ID="btnGenerar" runat="server" Text="Generar Horarios Nuevos" OnClick="btnGenerar_Click" />
        </div>
        <div class="container text-center">
            <asp:Label ID="lblAdvertencia" runat="server" Text="*Al usar este botón se borrarán los registros existentes" ForeColor="Red" Font-Size="Medium" Font-Italic="True"></asp:Label>
        </div>
        <br />
        <div class="container">
            <div class="card-header text-center">   
                <h2>Para Ver Horarios: Seleccione una Sección, para ver Horarios</h2>
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
        <asp:GridView ID="gdvLunes" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="8" GridLines="Vertical" CellSpacing="5">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="Detalle" HeaderText="Lunes" />
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
        <br />
        <asp:GridView ID="gdvMartes" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="8" GridLines="Vertical" CellSpacing="5">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="Detalle" HeaderText="Martes" />
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
        <br />
        <asp:GridView ID="gdvMiercoles" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="8" GridLines="Vertical" CellSpacing="5">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="Detalle" HeaderText="Miércoles" />
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
        <br />
        <asp:GridView ID="gdvJueves" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="8" GridLines="Vertical" CellSpacing="5">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" HeaderStyle-Width="100px" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="Detalle" HeaderText="Jueves" />
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
        <br />
        <asp:GridView ID="gdvViernes" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="8" GridLines="Vertical" CellSpacing="5">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="Detalle" HeaderText="Viernes" />
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
        <br />
        <br />
    </div>    

     <%--modal autor--%>
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
