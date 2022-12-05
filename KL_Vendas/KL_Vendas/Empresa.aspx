<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Empresa.aspx.cs" Inherits="KL_Vendas.Business" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html lang="pt-br">
<head runat="server">
    <!-- Google Tag Manager -->
<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','dataLayer','GTM-WHJKWBF');</script>
<!-- End Google Tag Manager -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <title>Segurança Digital para Pequenas Empresas | Software de Segurança de TI | Antivirus Linktel & Kaspersky Lab</title>
    <link href="vendor/bootstrap/css/bootstrap.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/scrolling-nav.css" rel="stylesheet">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
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
      </div>
    </nav>

    <section class="p-0">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-12 text-center">
            <img src="images/cover/KSOS.jpg" class="img-fluid" />
          </div>
        </div>
      </div>
    </section>        

    <section class="p-0">
      <div class="container">
        <div class="row">
          <div class="col-lg-2 p-5 text-center">
              <img src="images/products/KSOS.png" width="140">
          </div>
          <div class="col-lg-5 p-5">
            <div class="introduction">
              <h5 class="color-blue bold mtp-4">Segurança sempre ativa para empresas sempre ativas</h5>
                <p class="mtp-4">Criado especificamente para empresas com 5 a 50 computadores, o Kaspersky Small Office Security é:</p>
                <ul class="mtp-4">
                    <li>
                        Fácil de instalar e gerenciar;
                    </li>
                    <li>
                        Oferece a segurança mais testada e premiada do mundo para computadores, servidores de arquivos e dispositivos móveis;
                    </li>
                    <li>
                        Protege sua empresa de ataques on-line, fraude financeira, ransomware e perda de dados.
                    </li>
                </ul>
            </div>
            <div class="btn-more">
                <a href="pdf/KSOS6_Datasheet_BR.pdf" target="_blank" class="btn btn-product medium more">SAIBA MAIS </a>
            </div>  

            <div class="confirmation">
              <p class="bold space">Não tem certeza?</p>
              <p><a href="pdf/Material_Informativo_KSOS_Saiba_Mais.pdf" target="_blank">Veja mais benefícios da proteção exclusiva Linktel e Kaspersky para seu negócio</a></p>
            </div>  
             <p class="mtp-4">
                 <a href="pdf/REQUISITOS_DO_SISTEMA_KSOS.pdf" target="_blank" class="confirmation">Verifique os requisitos de sistema mínimos</a>
             </p>
          </div>
          <div class="col-lg-5 p-4">
              <div class="container">
                <div class="card-deck mb-3 text-center">
                  <div class="card mb-4 no-border">
                    <div class="card-body">
                        <div class="product">
                            <div class="desc-head">
                              <div class="container-fluid">
                                <div class="row">
                                  <div class="col-md-4 p-2 color-white bg-orange bold" runat="server" id="DivDesconto" visible="false">
                                    <p class="desc-p"><span class="highlight"><asp:Label runat="server" ID="lbl_desconto"></asp:Label> %</span> de desconto</p>
                                  </div>
                                  <div class="col-md-8">
                                    <div class="row">
                                      <div class="col-lg-12 bg-dark color-white bold">
                                        <h3>R$ <asp:Label runat="server" ID="lbl_valor_produto"></asp:Label>/mês</h3>
                                      </div>
                                    </div>
                                    <div class="row" runat="server" id="DivEconomize">
                                      <div class="col-lg-12 p-0">
                                        <p class="bold">
                                          <span><s>R$ <asp:Label runat="server" ID="lbl_valor_desconto"></asp:Label>/mês</s></span><br>
                                          <span>Economize!</span>
                                        </p>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                            <div class="separator mg-reset-10"></div>
                            <div class="select-plan fsizemedium text-left">
                                <p>Escolha a quantidade de desktops de 5 a 50</p>
                                <div class="selection">
                                    <div class="select-group" style="min-height: 100px;">
                                        <asp:DropDownList runat="server" ID="ddl_produtos" DataTextField="nm_produto_sub" DataValueField="id_produto_sub" CssClass="form-control border-blue m-1" OnSelectedIndexChanged="ddlProdutos_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="license">
                                  <p class="bold">Sua licença cobre:</p>
                                  <p>
                                    <asp:Literal runat="server" ID="lbl_dtl_produto"></asp:Literal>
                                    
                                  </p>
                                </div>
                            </div>
                            <div class="form-buttons text-center p-3">
                                <asp:Button runat="server" ID="btn_comprar" CssClass="btn btn-product large" Text="COMPRAR" OnClick="btn_comprar_Click" />
                                <p style="font-size:9pt;">Cancele a qualquer momento, sem compromisso</p>
                            </div>
                        </div>
                    </div>
                  </div>
                </div>
              </div>  
          </div>
        </div>
      </div>
    </section>

    <section class="p-4">
      <div class="container">
        <div class="separator "></div>
        <div class="row">
          <div class="col-lg-4 text-center">
              <img src="images/site/promocao.png">
          </div>
          <div class="col-lg-8 text-left p-3">
              <p>
                Assine <strong>Kaspersky Small Office Security</strong> para sua
                empresa mensalmente no cartão de crédito e proteja
                seus negócios por um valor menor que um almoço
                com clientes por mês!
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
    
    <asp:Label runat="server" ID="cpd" Visible="false"></asp:Label>
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>
