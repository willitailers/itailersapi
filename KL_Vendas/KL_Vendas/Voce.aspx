<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Voce.aspx.cs" Inherits="KL_Vendas.Voce" %>

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
    <meta name="robots" content="noindex">
    <title>Segurança Digital para Você | Antivirus Linktel & Kaspersky Lab</title>
    <link href="vendor/bootstrap/css/bootstrap.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/scrolling-nav.css" rel="stylesheet">
    <script src="https://code.jquery.com/jquery-2.1.3.min.js"></script>
    <link rel="icon" type="image/png" href="img/FavIcon_Linktel.ico" />
</head>
<body>
    <!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-WHJKWBF"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->

    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-dark bg-white color-white">
      <div class="container">
        <a class="navbar-brand js-scroll-trigger" href="index.aspx?nr_token=<%= this.Token %>">
          <img src="images/nav/logo_b.png" />
        </a>
      </div>
    </nav>

    <section class="bg-dark text-center p-0">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-12 text-center p-5 color-white">
              <h2>
                A SEGURANÇA MAIS TESTADA E MAIS PREMIADA À SUA DISPOSIÇÃO
              </h2>
              <p>
                Seja para proteger sua vida digital, sua mobilidade ou todo o mundo digital
                de sua família, nós temos as soluções <br /> de segurança que você precisa.
              </p>
          </div>
        </div>
      </div>
      <div class="container">

          <div class="card-deck mb-3 text-center" runat="server" id="divProducts" visible="true">
            <!-- LISTA DE PRODUTOS ADD DINAMICAMENTE -->
        </div>
        <div class="card-deck mb-3 text-center">

        </div>
      </div>
    </section>

    <section class="p-0">
      <div class="container p-0">
        <div class="row">
          <div class="col-lg-12">
            <div class="container p-4">
              <div class="row">
                <div class="col-lg-12 bg-white">
                  <div class="container p-0">
                    <div class="row">
                      <div class="col-lg-3 p-4"><img src="images/icons/como-funciona2.png" /></div>
                      <div class="col-lg-9">
                        <div class="container p-0">
                          <div class="row">
                            <div class="col-md-4 text-center ask">
                              <img src="images/icons/card.png" />
                                <p><b>Mês a mês</b></p>
                              <p>
                                Você paga uma mensalidade e garante a proteção digital do seu PC, tablet e smartphone, evitando vírus, bloqueando páginas perigosas, realizando transações bancárias com segurança e protegendo suas informações.
                              </p>
                            </div>
                            <div class="col-md-4 text-center ask">
                              <img src="images/icons/network.png" />
                                <p><b>Exclusividade para clientes Linktel</b></p>
                              <p>
                                Além do benefício especial de preço, clientes Linktel e Kaspersky contam com suporte disponível para solucionar qualquer dúvida sobre o produto.
                              </p>
                            </div>
                            <div class="col-md-4 text-center ask">
                              <img src="images/icons/user.png" />
                                <p><b>Proteção recorrente</b></p>
                              <p>
                                Não precisa se preocupar, sua proteção Kaspersky Lab permanece ativa de forma recorrente, sem qualquer problema ou dor de cabeça. Assine e aproveite a navegação 100% segura.
                              </p>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col-md-12 text-center">
                          <br />
                        <p>
                          Cancele a qualquer momento, sem compromisso.
                        </p>
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

    <section class="p-0">
      <div class="container p-0">
        <div class="row">
          <div class="col-lg-12">
            <div class="container p-4">
              <div class="row">
                <div class="col-lg-12 bg-white">
                  <div class="container p-0">
                      <div class="row ">
                          <div class="col-lg-12 bg-white text-center">
                          
                              </div>
                      </div> 
                    <div class="row">
                      <div class="col-lg-12 bg-white">
                        <div id="carouselContent" class="carousel slide" data-ride="carousel">
                            <div class="carousel-inner" role="listbox">
                                <asp:Repeater ID="rptAvaliacao" runat="server">
                                    <ItemTemplate>
                                        <div class="carousel-item <%# Eval("classe") %> p-4">
                                            <div class="row">
                                                <div class="col-lg-3">
                                                <img src="images/icons/star.png" /> 
                                                <img src="images/icons/star.png" /> 
                                                <img src="images/icons/star.png" /> 
                                                <img src="images/icons/star.png" /> 
                                                <img src="images/icons/star.png" /> 
                                                </div>
                                                <div class="col-lg-9"><h3><%# Eval("nm_titulo") %></h3></div>
                                            </div>                                    
                                     
                                                <p>
                                                <%# Eval("nm_avaliacao") %>
                                                </p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                            </div>
                            <a class="carousel-control-prev" href="#carouselContent" role="button" data-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="sr-only">Voltar</span>
                            </a>
                            <a class="carousel-control-next" href="#carouselContent" role="button" data-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="sr-only">Próximo</span>
                            </a>
                        </div>
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
        <footer class="p-4 bg-footer">
      <div class="container">
        <p class="text-center text-white">
          <img src="images/icons/cards.png">
        </p>
      </div>
    </footer>

    <footer class="p-4 bg-footer">
      <div class="container">
        <p class="m-0 text-center text-white">
          A <strong>Kasperky Lab</strong> é lider mundial em Segurança Digital. Protege seu acesso wifi, sua vida online e muito mais.
        </p>
      </div>
    </footer>

    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script>
      $(document).ready(function(){
        $('#myCarousel').carousel();
      });
    </script>
        <script>
            $(document).ready(function () {

                $.extend({
                    getUrlVars: function () {
                        var vars = [], hash;
                        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                        for (var i = 0; i < hashes.length; i++) {
                            hash = hashes[i].split('=');
                            vars.push(hash[0]);
                            vars[hash[0]] = hash[1];
                        }
                        return vars;
                    },
                    getUrlVar: function (name) {
                        return $.getUrlVars()[name];
                    }
                });

                $(".dropdown-toggle").dropdown();

                //$("input:radio").prop("checked", true);

                $('input:radio').each(function () {

                    var $this = $(this),
                        id = $this.attr('id'),
                        url = $this.attr('datasrc');

                    if ($(this).prop('checked')) {
                        ChangeRadio($(this));
                    }

                });

                $('input:radio').change(function () {
                    if ($(this).prop('checked')) {
                        ChangeRadio($(this));
                    }
                });

                $('#action a').click(function () {
                    var name_button = 'button' + $(this).attr("id-product");
                    var v_subProduto = localStorage[name_button];
                    var token = $.getUrlVar('nr_token');

                    window.location.replace("meu-carrinho?nr_token=" + token + "&cdp=" + v_subProduto);
                });

                function ChangeRadio(radio) {
                    var name_price = 'price' + radio.attr("id-product");
                    var name_price_old = 'price_old' + radio.attr("id-product");
                    var name_span = 'DivPrecoAntigo_' + radio.attr("id-product");
                    var name_button = 'button' + radio.attr("id-product");
                    var name_desc = radio.attr("dc-desc");
                    var name_desconto = 'DivDesconto' + radio.attr("id-product");
                    var name_valor_desconto = 'VlDesconto' + radio.attr("id-product");
                    

                    var price_old = radio.attr("dc-desc");
                    var price = radio.attr("vl-produto");
                    var valor_desc = radio.attr("dc-desc");
                    var valor_desconto = radio.attr("vl-desconto");

                    var valor_desc_str = parseFloat(radio.attr("dc-desc-str")).toFixed(2);
                    var valor_desconto_str = parseFloat(radio.attr("vl-desconto-str")).toFixed(2);

                    var element_price = document.getElementById(name_price);
                    var element_price_old = document.getElementById(name_price_old);
                    var element_span = document.getElementById(name_span);
                    var element_desconto = document.getElementById(name_desconto); 
                    var element_valor_desconto = document.getElementById(name_valor_desconto); 

                    if (element_price != undefined && element_price != null
                        && element_price_old != undefined && element_price_old != null) {

                        element_price.innerHTML = "R$ " + price.toString().replace('.', ',')  + '/mês';
                        element_price_old.innerHTML = "R$ " + price_old.toString().replace('.', ',')  + '/mês';
                        element_valor_desconto.innerHTML = valor_desconto + '%';

                        if (valor_desconto_str <= 0) {
                            element_span.style.visibility = 'hidden';
                            element_price_old.style.visibility = 'hidden';
                        }
                        else {
                            element_span.style.visibility = 'visible';
                            element_price_old.style.visibility = 'visible';
                            element_price_old.style.textDecoration = 'line-through';
                        }

                        if (valor_desc_str <= 0) {
                            element_desconto.style.visibility = 'hidden';
                        }
                        else {
                            element_desconto.style.visibility = 'visible';
                        }
                    }

                    localStorage[name_button] = radio.attr("sub-produto");
                }
            });
        </script>
   
    </form>
</body>
</html>
