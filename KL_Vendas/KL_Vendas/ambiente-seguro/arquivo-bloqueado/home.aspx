<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="KL_Vendas.ambiente_seguro.arquivo_bloqueado" %>

<!DOCTYPE html>

<html lang="pt">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
                    <h3 class="fweight">ARQUIVO MALICIOSO BLOQUEADO</h3>
                </header>
                <div class="stop text-center">
                  <img src="assets/IMG/aviso.png" width="100"/>
                </div>
              </div>

              <div class="text-main-alert text-center">
                <h4>Página bloqueada por desrespeitar os termos e condições de uso da Linktel.</h4>
              </div>

              <div class="buttons text-center">
                <a href="javascript:history.back()" class="btn alert"><img src="assets/IMG/voltar.png" width="35"> Voltar</a>
                <a href="https://www.antiviruslinktel.com.br/Termos.aspx?utm_source=navegacao&utm_medium=termos_telablock&utm_campaign=arquivo&nr_token=<%= this.Token %>" class="btn alert"><img src="assets/IMG/termos.png" width="35"> Termos</a>
              </div>
            </section>
          </div>
          <div class="col-md-4">
              <a href="https://www.antiviruslinktel.com.br/protecao-total?utm_source=navegacao&utm_medium=banner_telablock&utm_campaign=arquivo&nr_token=<%= this.Token %>" style="display:block;">
                  <section id="container-main">
                      <header class="header">
                          <div class="container-fluid p-0">
                              <div class="row no-gutters">
                                  <div class="col-md-12">
                                      <img src="assets/IMG/logo.png" width="90" />
                                  </div>
                              </div>
                          </div>
                      </header>

                      <div class="text-main">
                          <div class="container-fluid p-0">
                              <div class="row no-gutters">
                                  <div class="col-md-12 text-center color-white">
                                      <h4>PROTEÇÃO PARA VOCÊ <BR />E SEUS DISPOSITIVOS</h4>
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class="text-subheader">
                          <div class="container-fluid p-0">
                              <div class="row no-gutters">
                                  <div class="col-md-12 text-center color-lightblue">
                                      <h4>Condição Exclusiva <br />para clientes LINKTEL</h4>
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class="kts">
                          <div class="container-fluid p-0">
                              <div class="row no-gutters">
                                  <div class="col-md-12 text-center">
                                      <img src="assets/IMG/KTS.png" width="200" />
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class="text-subheader">
                          <div class="container-fluid p-0">
                              <div class="row no-gutters">
                                  <div class="col-md-12 text-center color-white">
                                      <h4 class="price">por apenas <span class="highlight">R$ 6,70/mês*</span></h4>
                                      <p class="psmall">* valor referente à licença para 1 dispositivo.</p>
                                  </div>
                              </div>
                          </div>
                      </div>
                  </section>
              </a>
          </div>
        
          <div class="col-md-1"></div>
        </div>
      </div>
    </form>
</body>
</html>
