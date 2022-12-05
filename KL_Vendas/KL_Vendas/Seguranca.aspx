<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seguranca.aspx.cs" Inherits="KL_Vendas.Seguranca" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>antiviruslinktel.com.br</title>
    <link href="css/bootstrap-3.3.7.min.css" rel="stylesheet" />
    <link href="css/conteudo.css" rel="stylesheet" />
    <!-- Google Fonts -->
    <link href='http://fonts.googleapis.com/css?family=Raleway' rel='stylesheet' type='text/css'>
    <link rel="icon" type="image/png" href="img/FavIcon_Linktel.ico" />
</head>
<body>
    <form id="form1" runat="server">
       
    <!-- Content Wrapper -->
    <div id="wrapper">
        <div class="container">
            <div class="col-xs-12 col-sm-7 col-lg-7">
                <!-- Info -->
                <div class="info">
                    <h1>Não</h1>
                    <h2>Conteúdo Impróprio</h2>
                    <p>A página que está tentando acessar foi bloqueada por desrespeitar os termos e condições de uso LinkTel.</p>
                    <a href="http://www.antiviruslinktel.com.br/" runat="server" id="link" class="btn">Proteja-se</a>
                </div>
                <!-- end Info -->
            </div>

            <div class="col-xs-12 col-sm-5 col-lg-5 text-center">
                <!-- Monkey -->
                <div class="monkey">
                    <img src="css/monkey.gif" />
                </div>
                <!-- end Monkey -->
            </div>

        </div>
        <!-- end container -->
    </div>
    <!-- end Content Wrapper -->

        <script src="js/bootstrap.min.js"></script>
        <script src="js/jquery-1.3.2.min.js"></script>

   
    </form>
</body>
</html>
