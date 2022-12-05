<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeSecurity.aspx.cs" Inherits="KL_Vendas.HomeSecurity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>Proteção antivírus e software de segurança de Internet | Kaspersky Lab BR</title>
    <meta name="description" content="Kaspersky Lab BR">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css\style.css">
    <link rel="stylesheet" type="text/css" href="css\style-aux.css">

    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
</head>

<body>
    <form id="form2" runat="server">
        <div class="container-fluid notification-bar--gray">
            <div class="container ">
                <ul class="menu-item_security">
                    <li class="title">
                        <p>Soluções para: </p>
                    </li>
                    <li><a href="#">Produtos domésticos</a></li>
                    <li><a href="#">Empresas até 50 funcionários</a></li>
                    <li><a href="#">Empresas até 1000 funcionários</a></li>
                    <li><a href="#">Empresas +1000 funcionários</a></li>
                </ul>
            </div>
        </div>
        <header>
            <div class="container-fluid">
                <nav class="nav_mobile navbar navbar-default">
                    <div class="container">
                        <a class="navbar-brand" href="#">
                            <img src="src/Kaspersky_RGB_POS_PNG.png" style="max-width: 130px;"
                                alt=""></a>
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar-collapse-1">
                                <span class="fas fa-3x fa-bars"></span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="navbar-collapse-1">
                            <ul class="nav navbar-nav navbar-right">
                                <li class="btn-menu-principal_mobile">
                                    <a class="btn" data-toggle="collapse" href="#nav-ProdutosDom" aria-expanded="false"
                                        aria-controls="nav-ProdutosDom">Produtos domésticos</a>
                                </li>
                                <ul class="collapse nav navbar-nav nav-collapse" id="nav-ProdutosDom">
                                    <li><a href="#">Security Cloud</a></li>
                                    <li><a href="#">Internet Security</a></li>
                                    <li><a href="#">Anti-Virus</a></li>
                                    <li><a href="#">Total Security</a></li>
                                    <li><a href="#">Internet Security para Android</a></li>
                                    <li><a href="#">Safe Kids</a></li>
                                </ul>
                                <li class="btn-menu-principal_mobile">
                                    <a class="btn btn-default btn-outline btn-circle" data-toggle="collapse" href="#nav-PequenasEmp"
                                        aria-expanded="false" aria-controls="nav-PequenasEmp">Pequenas empresas (1 A 50
                                    FUNCIONÁRIOS)</a>
                                </li>
                                <ul class="collapse nav navbar-nav nav-collapse" id="nav-PequenasEmp">
                                    <li><a href="#">Small Office Security</a></li>
                                    <li><a href="#">Endpoint Security Cloud</a></li>
                                    <li><a href="#">Endpoint Security para Business Select</a></li>
                                    <li><a href="#">Endpoint Security para Business Advanced</a></li>
                                </ul>
                                <li class="btn-menu-principal_mobile">
                                    <a class="btn btn-default btn-outline btn-circle" data-toggle="collapse" href="#nav-MediaEmp"
                                        aria-expanded="false" aria-controls="nav-MediaEmp">Medianas empresas (51 A 999
                                    FUNCIONÁRIOS)</a>
                                </li>
                                <ul class="collapse nav navbar-nav nav-collapse" id="nav-MediaEmp">
                                    <li><a href="#">Endpoint Security Cloud</a></li>
                                    <li><a href="#">Security para Microsoft</a></li>
                                    <li><a href="#">Endpoint Security para Business Select</a></li>
                                    <li><a href="#">Endpoint Security para Business Advanced</a></li>
                                    <li><a href="#">Security para Business Total</a></li>
                                    <li><a href="#">Segurança para cargas de trabalhos físicas, virtuais e na nuvem Hybrid
                                        Cloud</a></li>
                                </ul>
                                <li class="btn-menu-principal_mobile">
                                    <a class="btn btn-default btn-outline btn-circle" data-toggle="collapse" href="#nav-GrandesEmp"
                                        aria-expanded="false" aria-controls="nav-GrandesEmp">Enterprise (+1000
                                    FUNCIONÁRIOS)</a>
                                </li>
                                <ul class="collapse nav navbar-nav nav-collapse" id="nav-GrandesEmp">
                                    <li><a href="#">Endpoint Security</a></li>
                                    <li><a href="#">Endpoint Detection and Response</a></li>
                                    <li><a href="#">Hybrid Cloud Security</a></li>
                                    <li><a href="#">Anti Targeted Attack Platform</a></li>
                                    <li><a href="#">Private Security Network</a></li>
                                    <li><a href="#">Embedded Systems Security</a></li>
                                </ul>
                                <li class="btn-menu-principal_mobile"><a class="nav-link" href="https://www.kaspersky.com.br/partners"
                                    target="_blank">Parceiros</a></li>
                                <li class="btn-menu-principal_mobile"><a class="nav-link" href="https://support.kaspersky.com.br"
                                    target="_blank">Suporte</a></li>
                            </ul>
                        </div>
                        <!-- /.navbar-collapse -->
                    </div>
                    <!-- /.container -->
                </nav>
                <!-- /.navbar -->
                <div class="container-fluid home-security row">
                    <div class="container">
                        <div class="container-banner_first col">
                            <img src="src/Kaspersky_RGB_POS_PNG.png" alt="Logo Kaspersky">
                        </div>
                        <div class="container-my-kaspersky_second col">
                            <div class="dropdown">
                                <div class="nav-item dropdown">
                                    <a class="nav-link dropdown-toggle my-kaspersky_second" data-toggle="dropdown" href="#"
                                        role="button" aria-haspopup="true" aria-expanded="false">My Kaspersky </a>
                                    <div class="dropdown-menu">
                                        <a type="button" class="btn btn-dropdown" href="https://my.kaspersky.com/?returnUrl=http%3a%2f%2fmy.kaspersky.com%2fMyAccountRedirector%2fOrderHistory"
                                            target="_blank"><span class="dropdown-ico far fa-credit-card"></span>
                                            Meus pedidos</a>
                                        <a type="button" class="btn btn-dropdown" href="https://my.kaspersky.com/MyLicenses"
                                            target="_blank"><span class="dropdown-ico fas fa-download"></span>
                                            Meus produtos</a>
                                        <a type="button" class="btn btn-dropdown" href="https://my.kaspersky.com/MyDevices"
                                            target="_blank"><span class="dropdown-ico fas fa-tv"></span>
                                            Meus dispositivos</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>
        <section class="container-principal">
            <div class="container">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#">Página inicial</a></li>
                        <li class="breadcrumb-item active" aria-current="page">Produtos domésticos</li>
                    </ol>
                </nav>
                <h1 style="font-weight: bolder;">A SEGURANÇA MAIS TESTADA E MAIS PREMIADA À SUA DISPOSIÇÃO</h1>
                <div class="border-green">
                    <h2 class="text-desc">Seja para proteger sua vida digital, sua mobilidade ou todo o mundo digital de
                    sua família, nós temos
                    as soluções de segurança que você precisa.</h2>
                    <span>Aceitamos: </span>
                    <i class="fas fa-2x fa-credit-card"></i>
                    <i class="fab fa-2x fa-cc-mastercard"></i>
                    <i class="fab fa-2x fa-cc-paypal"></i>
                    <i class="fab fa-2x fa-cc-visa"></i>
                    <i class="fas fa-2x fa-barcode"></i>
                </div>
            </div>
        </section>
        <div class="container row product" runat="server" id="divProducts" visible="true">
            <!-- LISTA DE PRODUTOS ADD DINAMICAMENTE -->
        </div>
        <!-- COMPARE -->
        <div class="product-compare container row ">
            <h1 style="text-align: center;">COMPARE PRODUTOS</h1>
            <table class="table table-borderless ">
                <tbody>
                    <tr>
                        <th scope="row"></th>
                        <td>
                            <div class="product-item-box_desc">
                                <div class="product-img">
                                    <img src="src/product-box-KAV.png" alt="Proteção especial para PCS">
                                </div>
                                <div class="product-desc">
                                    <span>Proteção essencial para PCS</span>
                                    <h4 class="text-green">Kaspersky</h4>
                                    <h2>Anti-Virus</h2>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="product-item-box_desc">
                                <div class="product-img">
                                    <img src="src/product-box-KISMD.png" alt="Proteção premium">
                                </div>
                                <div class="product-desc">
                                    <span>Proteção premium</span>
                                    <h4 class="text-green">Kaspersky</h4>
                                    <h2>Internet Security</h2>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="product-item-box_desc">
                                <div class="product-img">
                                    <img src="src/product-box-KTS.png" alt="Nossa melhor segurança">
                                </div>
                                <div class="product-desc">
                                    <span>Nossa melhor segurança</span>
                                    <h4 class="text-green">Kaspersky</h4>
                                    <h2>Total Security</h2>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="ico-table col">
                                <span class="fas fa-shield-alt"></span>
                            </div>
                            <div class="table-text col">
                                <p class="table-tittle">Segurança</p>
                                <p class="table-desc">Defende contra vírus, ransomware e muito mais</p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-red"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="ico-table col">
                                <span class="fas fa-desktop"></span>
                            </div>
                            <div class="table-text col">
                                <p class="table-tittle">Desempenho</p>
                                <p class="table-desc">Protege sem reduzir seu desempenho</p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-red"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="ico-table col">
                                <span class="far fa-grin"></span>
                            </div>
                            <div class="table-text col">
                                <p class="table-tittle">Simplicidade</p>
                                <p class="table-desc">Simplifica a segurança para economizar tempo e evitar preocupações</p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-red"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="ico-table col">
                                <span class="fas fa-mobile-alt"></span>
                            </div>
                            <div class="table-text col">
                                <p class="table-tittle">PCs, Macs e dispositivos móveis</p>
                                <p class="table-desc">Protege qualquer combinação de dispositivos</p>
                            </div>
                        </th>
                        <td></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-red"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="ico-table col">
                                <span class="fas fa-user-secret"></span>
                            </div>
                            <div class="table-text col">
                                <p class="table-tittle">Privacidade</p>
                                <p class="table-desc">Ajuda a manter sua privacidade</p>
                            </div>
                        </th>
                        <td></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-red"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="ico-table col">
                                <span class="far fa-credit-card"></span>
                            </div>
                            <div class="table-text col">
                                <p class="table-tittle">Recursos Financeiros</p>
                                <p class="table-desc">Oferece proteção ao fazer compras e usar bancos online no PC e no Mac</p>
                            </div>
                        </th>
                        <td></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-red"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="ico-table col">
                                <span class="fas fa-user-times"></span>
                            </div>
                            <div class="table-text col">
                                <p class="table-tittle">Safe Kids</p>
                                <p class="table-desc">
                                    Controles para pais e recursos extras para proteger as crianças em
                                PCs,
                                Macs e dispositivos móveis
                                </p>
                            </div>
                        </th>
                        <td></td>
                        <td></td>
                        <td><span class="fas fa-check ico-red"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="ico-table col">
                                <span class="fas fa-key"></span>
                            </div>
                            <div class="table-text col">
                                <p class="table-tittle">Senhas</p>
                                <p class="table-desc">
                                    Gerencia e armazena suas senhas e as mantém sincronizadas para
                                permitir o acesso em PCs, Macs e dispositivos móveis
                                </p>
                            </div>
                        </th>
                        <td></td>
                        <td></td>
                        <td><span class="fas fa-check ico-red"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="ico-table col">
                                <span class="far fa-file"></span>
                            </div>
                            <div class="table-text col">
                                <p class="table-tittle">Proteção de arquivos</p>
                                <p class="table-desc">Ajuda a proteger suas fotos, músicas e seus arquivos valiosos em PCs</p>
                            </div>
                        </th>
                        <td></td>
                        <td></td>
                        <td><span class="fas fa-check ico-red"></span></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="container-about">
        </div>
        <footer>
            <div class="row container">
                <div class="col-md-auto">
                    <ul class="col-links">
                        <li>
                            <a href="#" class="first">Produtos domésticos</a>
                        </li>
                        <li>
                            <a href="#" class="second">Kaspersky Anti-Virus</a>
                        </li>
                        <li>
                            <a href="#" class="second">Kaspersky Internet Security</a>
                        </li>
                        <li>
                            <a href="#" class="second">Kaspersky Total Security</a>
                        </li>
                        <li>
                            <a href="#" class="second">Kaspersky Free</a>
                        </li>
                        <li>
                            <a href="#" class="second">Todos os produtos</a>
                        </li>
                    </ul>
                </div>
                <div class="col-md-auto">
                    <ul class="col-links">
                        <li>
                            <a href="#" class="first">Empresas</a>
                            <br />
                            <span class="footer-desc">(1-50 FUNCIONÁRIOS)</span>
                        </li>
                        <li>
                            <a href="#" class="second">Kaspersky Small Office Security</a>
                        </li>
                        <li>
                            <a href="#" class="second">Kaspersky EndPoint Security Cloud</a>
                        </li>
                        <li>
                            <a href="#" class="second">Todos os produtos</a>
                        </li>
                    </ul>
                </div>
                <div class="col-md-auto">
                    <ul class="col-links">
                        <li>
                            <a href="#" class="first">Empresas</a>
                            <br />
                            <span class="footer-desc">(51-999 FUNCIONÁRIOS)</span>
                        </li>
                        <li>
                            <a href="#" class="second">Kaspersky Endpoint Security Cloud</a>
                        </li>
                        <li>
                            <a href="#" class="second">Kaspersky Endpoint Security for Business Select</a>
                        </li>
                        <li>
                            <a href="#" class="second">Kaspersky Endpoint Security for Business Advanced</a>
                        </li>
                        <li>
                            <a href="#" class="second">Todos os produtos</a>
                        </li>
                    </ul>
                </div>
                <div class="col-md-auto">
                    <ul class="col-links">
                        <li>
                            <a href="#" class="first">Empresas</a>
                            <br />
                            <span class="footer-desc">(ACIMA DE 1000 FUNCIONÁRIOS)</span>
                        </li>
                        <li>
                            <a href="#" class="second">Cybersecurity Services</a>
                        </li>
                        <li>
                            <a href="#" class="second">Threat Management and Defense</a>
                        </li>
                        <li>
                            <a href="#" class="second">Endpoint Security</a>
                        </li>
                        <li>
                            <a href="#" class="second">Cloud Security</a>
                        </li>
                        <li>
                            <a href="#" class="second">Todos os produtos</a>
                        </li>
                    </ul>
                </div>
            </div>
            <p class="container">© 2018 AO Kaspersky Lab. Todos os direitos reservados.</p>
        </footer>
        <script src="https://code.jquery.com/jquery-2.1.3.min.js"></script>
        <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
        <script defer src="https://use.fontawesome.com/releases/v5.5.0/js/all.js" integrity="sha384-GqVMZRt5Gn7tB9D9q7ONtcp4gtHIUEW/yG7h98J7IpE3kpi+srfFyyB/04OV6pG0"
            crossorigin="anonymous"></script>
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

                $("input:radio").prop("checked", true);

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
                    var name_span = 'span' + radio.attr("id-product");
                    var name_button = 'button' + radio.attr("id-product");
                    var valor_desconto = parseFloat(radio.attr("vl-desconto").replace(',','.')).toFixed(2);

                    var price_old = parseFloat(radio.val().replace(',','.')).toFixed(2);
                    var price = (price_old - (price_old * valor_desconto));

                    var element_price = document.getElementById(name_price);
                    var element_price_old = document.getElementById(name_price_old);
                    var element_span = document.getElementById(name_span);

                    if (element_price != undefined && element_price != null
                        && element_price_old != undefined && element_price_old != null) {

                        element_price.innerHTML = "R$ " + price.toString().replace('.', ',');
                        element_price_old.innerHTML = "R$ " + price_old.toString().replace('.', ',');

                        if (valor_desconto == 0) {
                            element_span.style.visibility = 'hidden';
                            element_price_old.style.visibility = 'hidden';
                        }
                    }

                    localStorage[name_button] = radio.attr("sub-produto");
                };
            });
        </script>
    </form>
<script type="text/javascript">

	var _st_account = 5423;

	(function () {

		var ss = document.createElement('script');

		ss.type = 'text/javascript';

		ss.async = true;

		ss.src = '//app.shoptarget.com.br/js/tracking.js';

		var sc = document.getElementsByTagName('script')[0];

		sc.parentNode.insertBefore(ss, sc);

	})();

</script>
</body>
</html>
