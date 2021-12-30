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
        <%--alerts--%>
        <% if (Session["_exito"] != null) { %>
            <div class="alert alert-success" role="alert">
                <%= Session["_exito"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_exito"] = null;
          }%>

        <% if (Session["_wrn"] != null) { %>
            <div class="alert alert-warning" role="alert">
                <%= Session["_wrn"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_wrn"] = null;
          }%>

        <% if (Session["_err"] != null) { %>
            <div class="alert alert-danger" role="alert">
                <%= Session["_err"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_err"] = null;
          }%>
        <div class="container" style="width: fit-content">
            <asp:Button CssClass="btn btn-outline-primary container" ID="btnGenerar" runat="server" Text="Generar Horarios Nuevos" OnClick="btnGenerar_Click" />
        </div>
        <br />
        <div class="container" style="width: fit-content">
            <asp:Label ID="lblSeccion" runat="server" Text="Sección"></asp:Label>

            <div class="input-group mb-3">
                <asp:TextBox CssClass="form-control" ID="txtSeccion" runat="server"  ReadOnly="True" aria-describedby="btnModalSeccion" ValidationGroup="5"></asp:TextBox>
                
                <button class="btn btn-outline-primary" type="button" id="btnModalSeccion"
                data-bs-toggle="modal"
                data-bs-target="#seccionModal" style="width: 62px">Buscar</button>
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Sección incompleta!! Por favor, complétela." ValidationGroup="5" ControlToValidate="txtSeccion" ForeColor="Red">*</asp:RequiredFieldValidator>
       </div>

         <%--modal sección--%>
        <div class="modal" tabindex="-1" id="seccionModal">
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
                    <div class="col-auto">
                        <asp:TextBox ID="txtNumSeccion" runat="server" CccClass="form-control" ValidationGroup="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ControlToValidate="txtNumSeccion"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" />
                    </div>
                    <div class="col-auto">
                        <asp:Button ID="btnBuscarSeccion" runat="server" Text="Filtrar" CssClass="btn btn-secondary" />
                    </div>
                </div>
                    <br />
                  <asp:GridView ID="gdvSecciones" runat="server"></asp:GridView>
                </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
              </div>
            </div>
          </div>
        </div>
</asp:Content>
