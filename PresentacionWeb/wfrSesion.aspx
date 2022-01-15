<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrSesion.aspx.cs" Inherits="PresentacionWeb.wfrSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>

</head>
<body>
    <form id="form1" runat="server">
        <br />
         <%--alerts--%>
        <% if (Session["_exito"] != null) { %>
            <div class="alert alert-success container" role="alert">
                <%= Session["_exito"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_exito"] = null;
          }%>

        <% if (Session["_wrn"] != null) { %>
            <div class="alert alert-warning container" role="alert">
                <%= Session["_wrn"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_wrn"] = null;
          }%>

        <% if (Session["_err"] != null) { %>
            <div class="alert alert-danger container" role="alert">
                <%= Session["_err"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_err"] = null;
          }%>
        <br />
        <div class="container card bg-info">
            <h1 class="text-center">Inicio de Sesión</h1>
        </div>
        <br />
        <div class="container text-center" style="width: fit-content">
            <asp:Label ID="lblUsuario" runat="server" Text="Nombre de Usuario"></asp:Label>
            <br />
            <asp:TextBox CssClass="form" ID="txtUsuario" runat="server" Text="NU001"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Se requiere un Nombre de Usuario" ControlToValidate="txtUsuario" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label  ID="lblContrasena" runat="server" Text="Contraseña"></asp:Label>
            <br />
            <asp:TextBox CssClass="form" ID="txtContrasena" runat="server" Text="CON001"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Se requiere una Contraseña" ControlToValidate="txtContrasena" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" ForeColor="Red" />
            <br />
            <div class="container bg-dark" style="width: fit-content">
                <h2 class="text-white">Accede Como:</h2>
                <br />
                <asp:Button ID="btnAccesoDirector" runat="server" Text="Director" CssClass="btn btn-outline-info" ValidationGroup="1" OnClick="btnAccesoDirector_Click" />
                <br />
                <br />
                <asp:Button ID="btnAccesoAsistente" runat="server" Text="Asistente" CssClass="btn btn-outline-info" ValidationGroup="1" OnClick="btnAccesoAsistente_Click" />
                <br />
                <br />
                <asp:Button ID="btnAccesoProfesor" runat="server" Text="Profesor" CssClass="btn btn-outline-info" ValidationGroup="1" OnClick="btnAccesoProfesor_Click" />
                <br />
                <br />
            </div>

        </div>
    </form>
</body>
</html>
