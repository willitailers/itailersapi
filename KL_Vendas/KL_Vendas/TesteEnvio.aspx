<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TesteEnvio.aspx.cs" Inherits="KL_Vendas.TesteEnvio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Teste KL vendas</title>
    <script src="js/jquery-1.11.1.min.js"></script>
    <link href="cssB/bootstrap.min.css" rel="stylesheet" />
    <link href="cssB/bootstrap-grid.min.css" rel="stylesheet" />
    <script src="jsB/bootstrap.min.js"></script>
    
    <script>
        function ConsultaCPF() {
        //alert("https://ws.hubdodesenvolvedor.com.br/v2/cpf/?cpf=" + document.getElementById("txtCPF").value + "&data=" + document.getElementById("txtDtNascimento").value + "&token=25301485ytCESPRUuW45681064");
    $.ajax({
        type: "GET",
        url: "https://ws.hubdodesenvolvedor.com.br/v2/cpf/?cpf=" + document.getElementById("txtCPF").value + "&data=" + document.getElementById("txtDtNascimento").value + "&token=25301485ytCESPRUuW45681064", 
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        //data: '{cpf:"' + document.getElementById("txtCPF").value + '",data:"' + document.getElementById("txtDtNascimento").value + '", token="25301485ytCESPRUuW45681064"}',
        success: function (data) {
            alert(data.result);
        }
            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row" runat="server" id="Div1" visible="true">
                <div class="col-lg-4">
                    E-mail: <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-4">
                    Assunto: <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control">
                                <asp:ListItem Text="Pedido Recebido" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Pagamento Cancelado" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Assinatura Cancelada" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Pedido Em Analise" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Pedido Recusado" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Ative Licença" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Pagamento Confirmado" Value="7"></asp:ListItem>
                            </asp:DropDownList>
                </div>
                <div class="col-lg-4">
                    <asp:Button runat="server" ID="Button2" Text="Enviar Email" CssClass="btn btn-success" OnClick="Button2_Click1" />
                </div>
                <div class="col-lg-12">
                    <asp:Label runat="server" ID="lblEmail"></asp:Label>
                </div>
            </div>

            <div class="row" runat="server" id="d" visible="true">
                <div class="col-lg-2">
                    Numero da transacao: <asp:TextBox runat="server" ID="txtTransactionId" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-2">
                    Produto <asp:DropDownList runat="server" ID="ddlProdutos" CssClass="form-control">
                                <asp:ListItem>KIS</asp:ListItem>
                                <asp:ListItem>KISA</asp:ListItem>
                                <asp:ListItem>KISMD</asp:ListItem>
                                <asp:ListItem>KTSMD</asp:ListItem>
                            </asp:DropDownList>
                </div>
                <div class="col-lg-2">
                    Numero do subscribe: <asp:TextBox runat="server" ID="txtSubcribe" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-2">

                </div>
                <div class="col-lg-2">
                    <asp:Button runat="server" ID="btnEnviar" Text="Ativar" CssClass="btn btn-success" OnClick="btnEnviar_Click" />
                </div>
            </div>
            <div class="row" runat="server" id="d1" visible="false">
                <div class="col-lg-2">
                    id_carrinho: <asp:TextBox runat="server" ID="lblCartdID" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-2">
                    <asp:Button runat="server" ID="Button1" Text="Ativar" CssClass="btn btn-success" OnClick="Button2_Click" />
                </div>
            </div>
            <div class="row" runat="server" id="d2" visible="false">
                <div class="col-lg-4">
                    Data Nascimento: <input type="text" runat="server" id="txtDtNascimento" name="txtDtNascimento" class="form-control" />
                </div>
                <div class="col-lg-4">
                    CPF: <input type="text" runat="server" id="txtCPF" name="txtCPF" class="form-control" onblur="ConsultaCPF()" />
                </div>
                <div class="col-lg-4">

                </div>
            </div>
            <div class="row" >
                <asp:Button runat="server" ID="Button3" Text="Enviar linketel" CssClass="btn btn-success" OnClick="Button3_Click" />
            </div>

        </div>
    </form>
</body>
</html>
