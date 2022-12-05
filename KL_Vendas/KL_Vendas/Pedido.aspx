<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pedido.aspx.cs" Inherits="KL_Vendas.Pedido" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Google Tag Manager -->
<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','dataLayer','GTM-WHJKWBF');</script>
<!-- End Google Tag Manager -->

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width">
        <title>Pedido Concluído - Antivírus: Linktel Wifi e Kaspersky Lab</title>
     <link rel="stylesheet" href="css/bootstrap-3.3.7.min.css"/>
      <link rel="stylesheet" href="css/FontMuseoSans.css"/>
      <!--!/esi:include -->
      <link rel="stylesheet" href="css/styleC.css" type="text/css" media="all" />
    <link rel="icon" type="image/png" href="img/FavIcon_Linktel.ico" />
</head>
<body>
    <!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-WHJKWBF"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->
    <form id="form1" runat="server">
        <div class="container">
            <div class="dr_logo col-md-2 col-sm-2 col-xs-5">
            <img src="img/LINKTEL_LOGOS.png" />
            </div>
        </div>
        <div class="container breadcrumbBox">
             <div id="dr_CheckoutBreadcrumb" class="col-xs-12 col-sm-12 col-md-12">
                <ul id="dr_breadcrumb">
                   <li id="dr_bcCheckoutCart" class="dr_titleOff"><span>Carrinho</span></li>
                   <li id="dr_bcCheckoutBilling" class="dr_titleOff"><span>1 : Cadastro</span></li>
                   <li id="dr_bcConfirmOrder" class="dr_titleOff"><span>2 : Cobrança</span></li>
                   <li id="dr_bcThankYou" class="dr_titleOn current"><span>3 : Confirmação&#160;<i class="font-icons icon-angle-down"></i></span></li>
                </ul>
                <div class="xsBreadcrumb">
                   <span class="dr_titleOn">3 : Confirmação&#160;<i class="font-icons icon-angle-down"></i></span>
                </div>
             </div>
        </div>
        <div class="container-fluid breadcrumbContainer">&#160;</div>
        <div class="container">
         <!--!/esi:include -->
         <!-- Splash Transformer Optimized -->
         <!-- Layout: ThankYouPage, Generated: Sun Dec 16 23:27:49 CST 2018 -->
         <div xmlns="http://www.w3.org/1999/xhtml">
            <div class="dr_Content" id="dr_ThankYou">
               <div class="TYContent">
                  <h1>Obrigado</h1>
                  <h2 class="dr_TYPheader2">Agradecemos seu pedido</h2>
                  <div id="dr_thankYouElementContainer">
                     <div id="dr_wireTransferInstructions" class="dr_thankYouElement">
                        <h3>Instruções para pagamento</h3>
                        <div class="dr_thankYouElementPadding">
                           <h4><asp:Literal runat="server" ID="msgCompra"></asp:Literal></h4>
                           <h4><p>Enviaremos um e-mail de notificação assim que recebermos a confirmação do pagamento. Caso o pagamento não seja recebido dentro de 20 dias úteis, seu pedido será cancelado.</p>
                           <p>Imprima esta página com seus registros.</p></h4>
                        </div>
                     </div>
                     <div id="dr_productInformation">
                        <h3>Informações do Produto</h3>
                        <div id="dr_productInfo">
                           <div class="dr_lineItemProdInfo">
                              <asp:Literal runat="server" ID="lblprodutos"></asp:Literal>
                              <div class="TYP_additionalInfo">
                                 <h3>Como ativar a sua licença Premium<span class="tyQuesOpen">
                                 </h3>
                                    <p class="dr_additionalInfo">
                                        <h4>1. A sua licença é ativada no dia da compra.
                                        <br /><br />2. Copie o código de ativação que voce receberá em seu e-mail.
                                        <br /><br />3. Abra a sua janela do <%= this.nm_produto %>.
                                        <br /><br />4. Clique nas informações da sua licença no canto inferior direito da janela principal.
                                        <br /><br />5. Cole o seu código no campo que aparece e, a seguir, clique no botão 'Ativar'.
                                        <br /><br />6. Se você não tiver o <%= this.nm_produto %>, <b><a href="<%= this.link_produto %>" target="_blank" >baixe-o aqui</a></b>.
                                        <br /><br />Perguntas sobre a ativação? Visite nossa <b><a href="https://support.kaspersky.com.br/" target="_blank">Base de Conhecimento</a></b> ou contate o <b><a href="https://support.kaspersky.com.br/b2c?cid=purchase_email&utm_source=after_purchase_email&utm_medium=email&utm_campaign=after_purchase_2018&utm_content=br_gl" target="_blank" > Suporte Técnico</a></b>.<br></p>
                                    <p>Esta licença não pode ser vendida ou ativada fora da América Latina, Estados Unidos da América, Ilhas do Caribe e Ilhas Menores Distantes dos Estados Unidos. </p></h4>
                              </div>
                           </div>
                        </div>
                     </div>
                     <h3 id="orderInfoHeader">Informações sobre pedidos</h3>
                     <div class="col-xs-12 col-sm-12 col-md-12" id="dr_orderInformation">
                        <div>
                           <span id="dr_orderNumber">
                           <strong>Número do pedido:</strong> <asp:Label runat="server" ID="lblPedidoID"></asp:Label></span>
                           <br/>
                           <span id="dr_orderDate">
                           <strong>Data do pedido:</strong> <asp:Label runat="server" ID="lblPedidoData"></asp:Label></span>
                           <p>Quando terminarmos de processar seu pedido, você receberá um e-mail de confirmação no endereço fornecido.</p>
                        </div>
                        <hr/>
                        <div class="billingShipping">
                           <div class="col-xs-12 col-sm-6 col-md-6" id="billingDetails">
                              <address>
                                 <h3>
                                    <span>Endereço de cobrança:
                                    </span>
                                 </h3>
                                 <asp:Label runat="server" ID="lblNomeCliente"></asp:Label><br/>
                                 <div id="dr_billingEmailAddress"><asp:Label runat="server" ID="lblEmail"></asp:Label></div>
                              </address>
                           </div>
                        </div>
                     </div>
                     <div id="dr_TCFooter">
                        <p class="dr_legalResellerStatement">Itailers Gestão Empresarial e Marketing Ltda / CNPJ: 10.876.124/0001-99 / Inscrição Estadual: 148.084.552.119 / Rua Doutor Gabriel Piza, 577 - São Paulo, SP - 02036-011 / falecom@antiviruslinktel.com.br</p>
                        
                     </div>
                  </div>
               </div>
            </div>
    <asp:Literal runat="server" ID="lblTagConversao"></asp:Literal>
    </form>

</body>
</html>
