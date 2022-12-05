<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="KL_Vendas.Index" %>

<!DOCTYPE html>

<html lang="pt-br">
<head>
    <!-- Google Tag Manager -->
<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','dataLayer','GTM-WHJKWBF');</script>
<!-- End Google Tag Manager -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="robots" content="noindex">
    <title>Proteção digital exclusiva | Antivirus Linktel & Kaspersky Lab</title>

    <link href="vendor/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/scrolling-nav.css" rel="stylesheet" />
    <link rel="icon" type="image/png" href="img/FavIcon_Linktel.ico" />
</head>
<body>
    <!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-WHJKWBF"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->
    <form id="form1" runat="server">
      <nav class="navbar navbar-expand-lg navbar-dark bg-dark color-white">
      <div class="container">
        <a class="navbar-brand js-scroll-trigger" href="index.aspx?nr_token=<%= this.Token %>">
          <img src="images/nav/logo.png" />
        </a>
        <p class="txt-nav-brand">
          Escolha a melhor proteção <strong>KASPERSKY LAB</strong> para seu acesso:
        </p>
      </div>
    </nav>

    <header class="text-white p-header">
      <div class="container-fluid p-0 text-center">
        <div class="row no-gutters">
          <div class="col-md-6">
            <div class="image-content bg-left">
                <a href="/empresas?nr_token=<%= this.Token %>" class="btn btn-header">Empresas</a>
            </div>
          </div>
          <div class="col-md-6">
            <div class="image-content bg-right">
                <a href="/seguranca-digital?nr_token=<%= this.Token %>" class="btn btn-header">Para você</a>
            </div>
          </div>
        </div>
      </div>
    </header>

    <section class="text-center p-0">
      <div class="container-fluid">
        <div class="row no-gutters">
          <div class="col-lg-4 text-center p-70">
              <img src="images/site/logo.png" class="m-top-10" />
              <h4 class="p-5 color-gray">
                Proteção exclusiva
              </h4>
          </div>
          <div class="col-lg-4 bg-content text-center p-50 p-r-l">
              <p class="txt-content">
                <strong>LINKTEL</strong>
                agora oferece navegação segura <strong>GRATUITA</strong> na internet em parceria com a <strong>Kasperky Lab</strong>,
                e ofertas exclusivas aos <strong>CLIENTES LINKTEL</strong>.
              </p>
          </div>
          <div class="col-lg-4 text-center p-70">
              <img src="images/site/wifi.png" />
              <h4 class="p-4 color-gray">
                Protegido por <br>
                Kasperky Lab.*
              </h4>
              <p class="color-gray small">
                * Se caso uma das proteções Kaspersky Lab, oferecidas <br />
                com desconto especial, for ativada pelo usuário.
              </p>
          </div>
        </div>
      </div>
    </section>

    <footer class="p-4 bg-footer">
      <div class="container">
        <p class="m-0 text-center text-white">
          A <strong>Kasperky Lab</strong> é lider mundial em Segurança Digital. Protege seu acesso wifi, sua vida online e muito mais.
        </p>
      </div>
    </footer>

    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    </form>

</body>
</html>
