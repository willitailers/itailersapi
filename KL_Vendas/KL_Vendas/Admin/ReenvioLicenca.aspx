<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReenvioLicenca.aspx.cs" Inherits="KL_Vendas.Admin.ReenvioLicenca" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="txtIdCompra" placeholder="Id Compra"></asp:TextBox>
            <br />
            <asp:Button runat="server" ID="btnEnviar" Text="Enviar" OnClick="btnEnviar_Click" />
            <br />
            <asp:Label runat="server" ID="lblMsg"></asp:Label>
        </div>
    </form>
</body>
</html>
