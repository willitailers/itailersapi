<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessSecurity.aspx.cs" Inherits="KL_Vendas.BusinessSecurity" %>

<!doctype html>
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
    <form id="form1" runat="server">
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
                    </div>
                </nav>

                <div class="container-fluid home-security row">
                    <div class="container">
                        <div class="container-banner_first col">
                            <img src="src/Kaspersky_RGB_POS_PNG.png" alt="Logo Kaspersky">
                        </div>
                        <div class="container-my-kaspersky_second col">
                            <a class="nav-link" href="https://companyaccount.kaspersky.com/" target="_blank">CompanyAccount</a>
                        </div>
                    </div>
                </div>
        </header>
        <section class="container-principal">
            <div class="container">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#">Página inicial</a></li>
                        <li class="breadcrumb-item active" aria-current="page">Segurança para médias empresas</li>
                    </ol>
                </nav>
                <h1 style="font-weight: bolder;">CIBERSEGURANÇA DA PRÓXIMA GERAÇÃO PARA EMPRESAS</h1>
                <div class="border-green">
                    <h2 class="text-desc">Fácil de usar, simples de dimensionar: a Verdadeira Segurança Virtual para
                    proteger contra todos os tipos de ameaças que sua empresa enfrenta. No local ou na nuvem.</h2>
                </div>
        </section>
        </div>
    <div class="container row product">
        <div class="col-3 product-item business-col">
            <div class="product-item-box">
                <div class="product-item-box_desc business-image">
                    <img src="src/cloud.png" alt="Cloud" style="max-width: 120px;">
                    <h2 style="text-align: center;">CLOUD</h2>
                    <p>Endpoint Security</p>
                    <div class="product-item-desc">
                        <p>Força na proteção, facilidade no gerenciamento.</p>
                    </div>
                </div>
                <a type="button" class="btn button-comprar-red">Saiba mais</a>
            </div>
        </div>
        <div class="col-3 product-item business-col">
            <div class="product-item-box">
                <div class="product-item-box_desc business-image">
                    <img src="src/office-365.png" alt="Office 365" style="max-width: 120px;">
                    <h2 style="text-align: center;">Office 365</h2>
                    <p>Security for Microsoft</p>
                    <div class="product-item-desc">
                        <p>Proteção da próxima geração para e-mails do Office 365</p>
                    </div>
                </div>
                <a type="button" class="btn button-comprar-red">Saiba mais</a>
            </div>
        </div>
        <div class="col-3 product-item business-col">
            <div class="product-item-box">
                <div class="product-item-box_desc business-image">
                    <img src="src/product-icon-select.png" alt="Cloud" style="max-width: 120px;">
                    <h2 style="text-align: center;">SELECT</h2>
                    <p>Endpoint Security for Business</p>
                    <div class="product-item-desc">
                        <p>Proteção e controle da próxima geração para todos os endpoints</p>
                    </div>
                </div>
                <a type="button" class="btn button-comprar-red">Saiba mais</a>
            </div>
        </div>
        <div class="col-3 product-item business-col">
            <div class="product-item-box">
                <div class="product-item-box_desc business-image">
                    <img src="src/product-icon-advanced.png" alt="Cloud" style="max-width: 120px;">
                    <h2 style="text-align: center;">Advanced</h2>
                    <p>Endpoint Security for Business</p>
                    <div class="product-item-desc">
                        <p>Mais segurança com proteção de dados e gerenciamento estendido</p>
                    </div>
                </div>
                <a type="button" class="btn button-comprar-red">Saiba mais</a>
            </div>
        </div>

    </div>
        </div>

    <!-- COMPARE -->
        <div class="product-compare container row ">
            <h1 style="text-align: center;">Qual a solução ideal para sua empresa?</h1>
            <p style="text-align: center;">Compare soluções de segurança de TI da Kaspersky para empresas</p>
            <table class="table table-borderless ">
                <tbody>
                    <tr>
                        <th scope="row"></th>
                        <td>
                            <div class="product-item-box_desc">
                                <div class="product-img margin-auto">
                                    <img src="src/cloud.png" alt="Cloud">
                                    <h2>Cloud</h2>
                                </div>

                            </div>
                        </td>
                        <td>
                            <div class="product-item-box_desc">
                                <div class="product-img margin-auto">
                                    <img src="src/product-icon-select.png" alt="Select">
                                    <h2>Select</h2>
                                </div>

                            </div>
                        </td>
                        <td>
                            <div class="product-item-box_desc">
                                <div class="product-img margin-auto">
                                    <img src="src/product-icon-advanced.png" alt="Advanced">
                                    <h2>Advanced</h2>
                                </div>

                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">Console local com configuração granular</p>
                            </div>
                        </th>
                        <td></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">Console com base na nuvem simples e intuitivo</p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">
                                    Perfis de segurança pré-configurados
                                </p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">
                                    Proteção avançada para estações de trabalho e servidores de arquivos
                                </p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">
                                    Controles de dispositivos e da Web
                                </p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">
                                    Controle de aplicativos
                                </p>
                            </div>
                        </th>
                        <td></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">
                                    Segurança para Dispositivos Móveis
                                </p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">
                                    Gerenciamento de dispositivos móveis
                                </p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">
                                    Gerenciamento de aplicativos móveis
                                </p>
                            </div>
                        </th>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">
                                    Gerenciamento de sistemas
                                </p>
                            </div>
                        </th>
                        <td></td>
                        <td></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                    </tr>
                    <tr>
                        <th scope="row">
                            <div class="table-text col">
                                <p class="table-desc">
                                    Criptografia
                                </p>
                            </div>
                        </th>
                        <td></td>
                        <td></td>
                        <td><span class="fas fa-check ico-green"></span></td>
                    </tr>
                    <tr>
                        <th scope="row"></th>
                    </tr>
                    <th scope="row"></th>
                    <td>
                        <h2>Cloud</h2>
                        <a type="button" class="btn button-comprar-red">Saiba mais</a>
                    </td>
                    <td>
                        <h2>Select</h2>
                        <a type="button" class="btn button-comprar-red">Saiba mais</a>
                    </td>
                    <td>
                        <h2>Advanced</h2>
                        <a type="button" class="btn button-comprar-red">Saiba mais</a>
                    </td>
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
                $(".dropdown-toggle").dropdown();
            });

        </script>
    </form>
</body>
</html>
