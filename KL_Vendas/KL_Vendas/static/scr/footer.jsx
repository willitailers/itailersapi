import React, { Component } from 'react';
import PropTypes from 'prop-types';
import classes from './footer.module.scss';
import mail from '../../assets/images/Email.svg';
import ssl from '../../assets/images/ssl.png';
import google from '../../assets/images/google.png';
import cert from '../../assets/images/cert.png';
import { Grid, Container } from '@material-ui/core';
export class Footer extends Component {
  static propTypes = {};

  render() {
    return (
      <div className={classes.wrapper}>
        <Container>
          <Grid container className={classes.container}>
            <Grid item xs={12} md={6} container alignItems="center">
              <Grid
                item
                xs={12}
                container
                alignItems="center"
                className={classes.mail}
              >
                <img src={mail} alt="Icone de carta" />
                falecom@antiviruslinktel.com.br
              </Grid>
              <Grid item xs={12}>
                <p className={classes.address}>
                  Itailers Gestão Empresarial e Marketing Ltda
                  <br />
                  CNPJ: 10.876.124/0001-99
                  <br />
                  Inscrição Estadual: 148.084.552.119
                  <br /> Rua Doutor Gabriel Piza, 577 | São Paulo – SP 02036-011
                </p>
              </Grid>
            </Grid>
            <Grid item xs={12} md={6} container alignItems="center">
              <Grid
                item
                xs={12}
                container
                alignItems="center"
                className={classes.mail}
                style={{ opacity: 0 }}
              >
                falecom@antiviruslinktel.com.br
              </Grid>
              <Grid item xs={12}>
                <span className={classes.buy}>Compra Segura</span>
                <div className={classes.certs}>
                  <img src={ssl} alt="Certificado SSL" />
                  <img src={google} alt="Certificado Google" />
                  <img src={cert} alt="Certificado Certisign" />
                </div>
              </Grid>
            </Grid>
          </Grid>
        </Container>
      </div>
    );
  }
}

export default Footer;
