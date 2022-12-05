<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="KL_Vendas.ambiente_seguro.conteudo_improprio.Home" %>

<!DOCTYPE html>

<html lang="pt-br">
<head runat="server">
    <meta charset="utf-8" />
    <title>Proteção digital exclusiva | Antivirus Linktel & Kaspersky Lab</title>
     <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">

  <link rel="stylesheet" type="text/css" href="assets/CSS/bootstrap.css">
  <link rel="stylesheet" type="text/css" href="assets/CSS/style.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
    <div class="row">
      <div class="col-md-1"></div>
      <div class="col-md-5">
        <section id="container-main-alert">
          <div class="box">
            <header class="text-center header-aviso">
                <h3 class="fweight">CONTEÚDO IMPRÓPRIO</h3>
            </header>
            <div class="stop text-center">
              <img src="assets/IMG/aviso-02.png" width="100"/>
            </div>
          </div>

          <div class="text-main-alert text-center">
            <h4>Página bloqueada por tentativa de acesso a um site perigoso ou por desrespeitar os termos e condições de uso da Linktel</h4>
          </div>

          <div class="buttons text-center">
            <a href="javascript:history.back()" class="btn alert"><img src="assets/IMG/voltar.png" width="35"> Voltar</a>
            <a href="https://www.antiviruslinktel.com.br/Termos.aspx?utm_source=navegacao&utm_medium=termos_telablock&utm_campaign=conteudo&nr_token=<%= this.Token %>" class="btn alert"><img src="assets/IMG/termos.png" width="35"> Termos</a>
          </div>
        </section>
      </div>
      <div class="col-md-4">
          <a href="https://protecao.antiviruslinktel.com.br/combo-kaspersky-wifi?utm_source=pagina_bloqueio&utm_medium=fase_combo&utm_campaign=first_campaign" style="display:block;">
        <section id="container-main">
          <img src="assets/IMG/campanha_1610_350_405.jpg" width="100%" />
        </section>
              </a>
      </div>
      <div class="col-md-1"></div>
    </div>
  </div>
    </form>
</body>
</html>
