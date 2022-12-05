<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KTS.aspx.cs" Inherits="KL_Vendas.KTS" %>

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
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="robots" content="noindex">
    <title>Antivirus Online e Segurança Total | Antivirus Linktel & Kaspersky Lab</title>

    <link href="vendor/bootstrap/css/bootstrap.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/scrolling-nav.css" rel="stylesheet">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-2.1.3.min.js"></script>
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
            <img src="images/cover/KTS.jpg" class="img-fluid" />
          </div>
        </div>
      </div>
    </section>        

    <section class="p-0">
      <div class="container">
        <div class="row">
          <div class="col-lg-2 p-5 text-center">
              <img src="<%= this.CaminhoImagem %>">
          </div>
          <div class="col-lg-5 p-5 hidden-sm hidden-xs">
              <h5 class="color-blue bold mtp-4">Protege as senhas, os dados, os dispositivos de sua família e muito mais.</h5>
              <p class="mtp-4">
                Sua família é muito importante. Dê a eles a nossa melhor proteção.
              </p>
              <p class="mtp-4">
                O Kaspersky Total Security ajuda a proteger sua
                família enquanto vocês navegam pela Internet,
                fazem compras, usam as redes sociais ou fazem
                streaming. Além disso, a proteção extra de
                privacidade armazena com segurança suas
                senhas e documentos importantes, protege
                arquivos e lembranças preciosas e ajuda a
                proteger as crianças dos perigos digitais.
              </p>
              <p class="mtp-4">
                <ul class="p-3">
                  <li>Protege a privacidade, senhas, arquivos e fotos</li>
                  <li>Protege seu dinheiro em transações bancárias e compras on-line</li>
                  <li>Protege as crianças on-line e off-line</li>
                </ul>
              </p>
              <p class="mtp-4">
                  <a data-toggle="modal" data-target="#exampleModal" href="#exampleModal" >Verifique os requisitos de sistema mínimos. </a>
              </p>
          </div>
          <div class="col-lg-5 p-5">
              <div class="container">
                <div class="card-deck mb-3 text-center">
                  <div class="card mb-4 no-border">
                    <div class="card-body">
                        <div class="product">
                            <div class="desc-head">
                              <div class="container-fluid">
                                <div class="row">
                                  <div class="col-md-4 p-2 color-white bg-orange bold" id="DivDesconto<%= this.ProdutoId %>">
                                    <p class="desc-p"><span class="highlight" id="VlDesconto<%= this.ProdutoId %>">30%</span> de desconto</p>
                                  </div>
                                  <div class="col-md-8">
                                    <div class="row">
                                      <div class="col-lg-12 bg-dark color-white bold">
                                        <h3 id="price<%= this.ProdutoId %>">R$ 69,93/mês</h3>
                                      </div>
                                    </div>
                                    <div class="row" >
                                      <div class="col-lg-7 p-0" id="DivPrecoAntigo_<%= this.ProdutoId %>">
                                        <p class="bold">
                                          <span id="price_old<%= this.ProdutoId %>"><s>R$ 99,90/mês</s></span><br>
                                          <span>Economize!</span>
                                        </p>
                                      </div>
                                        <div class="col-lg-5 p-0" id="div_trial_<%= this.ProdutoId %>">
                                        <p class="bold">
                                          <span id="trial_<%= this.ProdutoId %>">Teste Gratis por X dias</span>
                                        </p>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                            <div class="separator mg-reset-10"></div>
                            <div class="select-plan">
                                <div class="radio-group" style="min-height: 140px;">
                                  <asp:Repeater ID="repeaterChecks" runat="server">
                                        <ItemTemplate>
                                            <div class="form-element radio <%# (ValidarDesconto(Eval("vl_desconto_str"))) %>">
                                                <input class="form-check-input" type="radio" name="radio-price<%# Eval("id_produto") %>" id='check<%# Eval("id_produto_sub") %>' vl-produto="<%# Eval("vl_produto_sub")  %>" value="<%# Eval("vl_produto_sub")  %>"
                                                    id-product="<%# Eval("id_produto") %>" sub-produto="<%# Eval("cd_produto_sub_publico") %>" vl-desconto="<%# Eval("vl_desconto")  %>" vl-desconto-str="<%# Eval("vl_desconto_str")  %>" nr-trial="<%# Eval("nr_trial")  %>"
                                                    dc-desc="<%# Eval("vl_produto_sub_desc")  %>" dc-desc-str="<%# Eval("vl_produto_sub_desc_str")  %>" <%# Eval("checado")  %> >
                                                <label>
                                                <span class="choose fsizemedium">
                                                    <span><%# Eval("nm_produto_sub") %></span>
                                                </span>
                                                <span class="price fsizemedium">
                                                    <span class="price-new">R$<%# Eval("vl_produto_sub") %>/mês</span>
                                                </span>
                                                </label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="form-buttons text-center p-3">
                                <input type="button" class="btn btn-product large" value="COMPRAR" id="button1" />
                                <img src="images/icons/cards.png" class="p-4">
                            </div>
                        </div>
                    </div>
                  </div>
                </div>
              </div>  
          </div>
            <div class="col-lg-5 p-5 hidden-lg hidden-md">
              <h5 class="color-blue bold mtp-4">Protege as senhas, os dados, os dispositivos de sua família e muito mais.</h5>
              <p class="mtp-4">
                Sua família é muito importante. Dê a eles a nossa melhor proteção.
              </p>
              <p class="mtp-4">
                O Kaspersky Total Security ajuda a proteger sua
                família enquanto vocês navegam pela Internet,
                fazem compras, usam as redes sociais ou fazem
                streaming. Além disso, a proteção extra de
                privacidade armazena com segurança suas
                senhas e documentos importantes, protege
                arquivos e lembranças preciosas e ajuda a
                proteger as crianças dos perigos digitais.
              </p>
              <p class="mtp-4">
                <ul class="p-3">
                  <li>Protege a privacidade, senhas, arquivos e fotos</li>
                  <li>Protege seu dinheiro em transações bancárias e compras on-line</li>
                  <li>Protege as crianças on-line e off-line</li>
                </ul>
              </p>
              <p class="mtp-4">
                  <a data-toggle="modal" data-target="#exampleModal" href="#exampleModal" >Verifique os requisitos de sistema mínimos. </a>
              </p>
          </div>
        </div>
      </div>
    </section>

    <section class="p-0">
      <div class="container">
        <div class="row">
          <div class="col-lg-12 text-center bg-dark">
            <div class="header color-white">
              <h4 class="p-1 m-2">ESSA É A MELHOR SOLUÇÃO PARA MIM?</h4>
            </div>
          </div>
        </div>
      </div>
      <div class="container">
        <div class="row p-3">
          <div class="col-md-4 text-center divisor p-4">
            <div class="row">
              <div class="icon col fsize icon-active">
                <i class="fas fa-laptop"></i>
                <span class="fsmall">PC's</span>
              </div>
              <div class="icon col fsize icon-active">
                <i class="fas fa-desktop"></i>
                <span class="fsmall">MAC's</span>
              </div>
              <div class="w-100"></div>
              <div class="icon col fsize m-3 icon-active">
                <i class="fas fa-tablet-alt"></i>
                <span class="fsmall">Tablets <br /> Android</span>
              </div>
              <div class="icon col fsize m-3 icon-active">
                <i class="fas fa-mobile-alt"></i>
                <span class="fsmall">SmartPhones Android</span>
              </div>
            </div>  
          </div>
          <div class="col-md-8 p-4">
            <div class="questions-selection">
              <asp:Repeater ID="rptMelhor" runat="server">
                    <ItemTemplate>
                        <div class="item">
                            <img src="img/Check_img.png" />
                            <label for="item-1"><b><%# Eval("nm_titulo_melhor_produto") %>:</b></label><label for="item-1"> <%# Eval("nm_melhor_produto") %></label>
                          </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
          </div>
        </div>
      </div>
    </section> 

    <section class="p-2">
      <div class="container">
        <div class="row">
          <div class="col-lg-12 text-center bg-dark">
            <div class="header color-white">
              <h4 class="p-1 m-2">AVALIAÇÕES</h4>
            </div>
          </div>
        </div>
      </div>
      <div class="container p-0">
        <div class="row">
          <div class="col-lg-12">
            <div class="container p-2">
              <div class="row">
                <div class="col-lg-12 bg-white">
                  <div class="container p-0">
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
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">REQUISITOS DO SISTEMA</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body" style="font-size:small">
                <h4>KASPERSKY TOTAL SECURITY:</h4>
                  <b>Para todos os dispositivos</b><br />
                    •	Conexão com a Internet – para a ativação e atualizações do produto, e para acessar alguns recursos<br />
                    •	A tecnologia Proteção para Webcam funciona somente em PCs e computadores Mac. O recurso está disponível para diversas webcams compatíveis. Para obter a lista completa dos dispositivos compatíveis, visite: <a href="https://support.kaspersky.com/14102#block1" target="_blank">https://support.kaspersky.com/14102#block1</a> (para PCs) e <a href="https://support.kaspersky.com/14248#block1"  target="_blank">https://support.kaspersky.com/14248#block1</a> (para computadores Mac)<br />
                    <b>Desktops e laptops Windows®</b><br />
                    •	1.500 MB de espaço disponível no disco rígido<br />
                    •	Microsoft® Internet Explorer® 10 ou superiores<br />
                    •	Microsoft .NET Framework 4 ou superiores<br />
                    •	Microsoft Windows 10(1) Home / Pro / Enterprise(2), (3)<br />
                    •	Microsoft Windows 8 e 8.1 / Pro / Enterprise(2) / atualização 8.1(3)<br />
                    •	Microsoft Windows 7 Starter/Home Basic e Premium/Professional/Ultimate — SP1 ou superiores(3)<br />
                    •	Processador: 1 GHz ou superiores<br />
                    •	Memória (RAM): 1 GB (32 bits) ou 2 GB (64 bits)<br />
                    <b>Tablets Windows (sistemas com processador Intel®)(2)</b><br />
                    •	Microsoft Windows 101 Home / Pro / Enterprise(2) (3)<br />
                    •	Microsoft Windows 8 e 8.1 Pro (64 bits(3))<br />
                    •	Resolução mínima de tela: 1024 x 600<br />
                    <b>Desktops e laptops Mac</b><br />
                    •	1.800 MB de espaço disponível no disco rígido<br />
                    •	Memória (RAM): 2 GB<br />
                    •	macOS 10.12 – 10.14<br />
                    <b>Smartphones e tablets Android(4)</b><br />
                    •	Android™ 4.2(5) – 9.0<br />
                    •	Resolução mínima de tela: 320 x 480<br />
                    <b>iPhone e iPad(6)</b><br />
                    •	iOS® 10.0(5) ou superiores<br />
                    
                    Não damos suporte para as versões Beta/Preview de sistemas operacionais novos. O produto oferece suporte apenas para sistemas operacionais finais e lançados oficialmente.<br />
                    <p style="font-size:x-small">1 Se você usa o Windows 10, talvez seja necessário baixar e instalar todas as correções disponíveis para o software de segurança da Kaspersky Lab. O produto não se destina à operação nas edições do Windows 10 Mobile/S.<br />
                    2 O serviço Kaspersky Safe Kids não está disponível para esta versão do sistema operacional ou este tipo de dispositivo.<br />
                    3 Alguns recursos do produto podem não funcionar em sistemas operacionais de 64 bits. Visitehttps://support.kaspersky.com/13812 para obter mais detalhes.<br />
                    4 No momento, as funcionalidades do Kaspersky Internet Security for Android podem ser limitadas em determinados dispositivos. Visite <a href="http://support.kaspersky.com/10216" target="_blank">http://support.kaspersky.com/10216</a> para obter mais detalhes. Se estiver usando o Kaspersky Safe Kids em dispositivos Xiaomi© ou ASUS©, visite <a href="https://support.kaspersky.com/12980"  target="_blank">https://support.kaspersky.com/12980</a>  para obter mais detalhes sobre como configurar e usar o serviço.<br />
                    5 O serviço Kaspersky Safe Kids funciona no Android™ 4.4 – 9.0, iOS® 10.0 ou superiores.<br />
                    6 Apenas o Kaspersky Password Manager, Kaspersky Safe Kids e Kaspersky Safe Browser estão disponíveis nessas plataformas.</p>

              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
              </div>
            </div>
          </div>
        </div>

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

                $('#button1').click(function () {
                    var name_button = 'button';
                    var v_subProduto = localStorage[name_button];
                    var token = $.getUrlVar('nr_token');

                    var url = 'meu-carrinho?nr_token=' + token + '&cdp=' + v_subProduto;

                    window.location.href = url;
                    
                });

                

                function ChangeRadio(radio) {
                    var name_price = 'price' + radio.attr("id-product");
                    var name_price_old = 'price_old' + radio.attr("id-product");
                    var name_span = 'DivPrecoAntigo_' + radio.attr("id-product");
                    var name_button = 'button';
                    var name_desc = radio.attr("dc-desc");
                    var name_desconto = 'DivDesconto' + radio.attr("id-product");
                    var name_valor_desconto = 'VlDesconto' + radio.attr("id-product");
                    var name_nr_trial = 'trial_' + radio.attr("id-product");
                    var name_span_nr_trial = 'div_trial_' + radio.attr("id-product");
                    

                    var price_old = radio.attr("dc-desc");
                    var price = radio.attr("vl-produto");
                    var valor_desc = radio.attr("dc-desc");
                    var valor_desconto = radio.attr("vl-desconto");
                    var valor_nr_trial = parseInt(radio.attr("nr-trial"));

                    var valor_desc_str = parseFloat(radio.attr("dc-desc-str")).toFixed(2);
                    var valor_desconto_str = parseFloat(radio.attr("vl-desconto-str")).toFixed(2);

                    var element_price = document.getElementById(name_price);
                    var element_price_old = document.getElementById(name_price_old);
                    var element_span = document.getElementById(name_span);
                    var element_desconto = document.getElementById(name_desconto); 
                    var element_valor_desconto = document.getElementById(name_valor_desconto); 
                    var element_nr_trial = document.getElementById(name_nr_trial); 
                    var element_span_nr_trial = document.getElementById(name_span_nr_trial); 

                    if (element_price != undefined && element_price != null
                        && element_price_old != undefined && element_price_old != null) {

                        element_price.innerHTML = "R$ " + price.toString().replace('.', ',') + '/mês';
                        element_price_old.innerHTML = "R$ " + price_old.toString().replace('.', ',') + '/mês';
                        element_valor_desconto.innerHTML = valor_desconto + '%';

                        if (valor_nr_trial > 0) {
                            element_span_nr_trial.style.visibility = 'visible';
                            element_nr_trial.innerHTML = "Teste Gratis por " + valor_nr_trial.toString() + " dias!";
                        }
                        else {
                            element_span_nr_trial.style.visibility = 'hidden';
                            element_nr_trial.innerHTML = "";
                        }

                        if (valor_desconto_str <= 0) {
                            element_span.style.visibility = 'hidden';
                            element_price_old.style.visibility = 'hidden';
                        }
                        else {
                            element_span.style.visibility = 'visible';
                            element_price_old.style.visibility = 'visible';
                            element_price_old.style.textDecoration = 'line-through'
                        }

                        if (valor_desc_str <= 0) {
                            element_desconto.style.visibility = 'hidden';
                        }
                        else {
                            element_desconto.style.visibility = 'visible';
                        }
                    }

                    localStorage[name_button] = radio.attr("sub-produto");
                };
            });
        </script>
    </form>
</body>
</html>
