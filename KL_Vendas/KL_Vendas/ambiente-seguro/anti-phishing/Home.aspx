<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="KL_Vendas.ambiente_seguro.anti_phishing.Home" %>

<!DOCTYPE html>

<html lang="pt">
<head runat="server">
    <meta charset="utf-8" />
    <title>Proteção digital exclusiva | Antivirus Linktel & Kaspersky Lab</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" type="text/css" href="assets/CSS/bootstrap.css">
  <link rel="stylesheet" type="text/css" href="assets/CSS/style.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
    <div class="row">
      <div class="col-md-1"></div>
      <div class="col-lg-5">
        <section id="container-main-alert">
          <header class="header-alert">
            <div class="container-fluid p-0">
                <div class="row no-gutters">
                  <div class="col-md-12 text-left">
                    <img src="assets/IMG/cuidado.png" class="img-fluid" />
                  </div>
                </div>
            </div>
          </header>

          <div class="text-main-alert">
            <div class="container">
                <div class="row no-gutters">
                  <div class="col-md-1 text-left color-white"></div>
                  <div class="col-md-10 text-left color-white">
                    <p class="auto fsize-22">
                      A página que você está tentando acessar foi bloqueada. <br />
                      Detectamos uma <span class="highlight-alert">TENTATIVA DE PHISHING</span>.
                    </p>
                    <p class="auto fsize-22">
                      Criminosos podem tentar roubar seus dados <br />através desse site.<br />
                      <span class="highlight-alert">Saia dessa página.</span>
                    </p>
                  </div>
                  <div class="col-md-1 text-left color-white"></div>
                </div>
                <div class="row no-gutters">
                  <div class="col-md-1 text-left color-white"></div>
                  <div class="col-md-10 text-left color-white">
                      <div class="buttons">
                        <a href="javascript:history.back()" class="btn btn-primary">VOLTAR</a>
                        <a href="https://www.antiviruslinktel.com.br/Termos.aspx?utm_source=navegacao&utm_medium=termos_telablock&utm_campaign=phishing&nr_token=<%= this.Token %>" class="btn btn-primary">TERMOS</a>
                      </div>
                  </div>
                  <div class="col-md-1 text-left color-white"></div>
                </div>
            </div>
          </div>
        </section>
      </div>
      <div class="col-lg-4">
          <a href="https://www.antiviruslinktel.com.br/internet-security?utm_source=navegacao&utm_medium=banner_telablock&utm_campaign=phishing&nr_token=<%= this.Token %>" style="display:block;">
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
                    <h4>PROTEÇÃO CONTRA <br /> PHISHING</h4>
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
                    <img src="assets/IMG/KIS.png" width="200" />
                  </div>
                </div>
            </div>
          </div>

          <div class="text-subheader">
            <div class="container-fluid p-0">
                <div class="row no-gutters">
                  <div class="col-md-12 text-center color-white">
                    <h4 class="price">por apenas <span class="highlight">R$ 5,90/mês*</span></h4>
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
