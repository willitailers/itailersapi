<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KL_Arquivo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Itailers - Arquivo KL</title>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-xl-8 text-center">
                    Arquivo KL
                    <asp:FileUpload runat="server" ID="FileKL" class="form-control" />
                </div>
                <div class="col-xl-8 text-center">
                    Arquivo UOL
                    <asp:FileUpload runat="server" ID="FileUOL" class="form-control" />
                    <asp:Button runat="server" ID="btnEnviar" Text="Enviar" CssClass="btn btn-success" OnClick="btnEnviar_Click" />
                </div>
            </div>
            <div class="row alert alert-danger" runat="server" id="divErro" visible="false">
                <div class="col-sm-12 col-lg-12 text-center">
                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row success alert-success" runat="server" id="divSucesso" visible="false">
                <div class="col-sm-12 col-lg-12 text-center" style="padding: 10px">
                    <asp:Label ID="lblSucesso" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
