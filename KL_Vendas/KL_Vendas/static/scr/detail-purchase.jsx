import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Grid } from '@material-ui/core';
import produto from '../../assets/images/Produto.svg';
import check from '../../assets/images/check.svg';
import porco from '../../assets/images/Porquinho.svg';
import padlock from '../../assets/images/padlock-grey.svg';
import card1 from '../../assets/images/cartoes/America.svg';
import card2 from '../../assets/images/cartoes/Diners.svg';
import card3 from '../../assets/images/cartoes/elo.svg';
import card4 from '../../assets/images/cartoes/Hiper.svg';
import card5 from '../../assets/images/cartoes/master.svg';
import card6 from '../../assets/images/cartoes/Visa.svg';
import classes from './detail-purchase.module.scss';

export class DetailPurchase extends Component {
  static propTypes = {};

  render() {
    const { sent } = this.props;
    if (!sent)
      return (
        <React.Fragment>
          <Grid container className={classes.wrapper}>
            <Grid item xs={12} style={{ paddingLeft: 5 }}>
              <h1 className={classes.title}>Meu Antivírus</h1>
            </Grid>
            <Grid item xs={12} container justify="space-around">
              <Grid item xs="auto">
                <img
                  src={produto}
                  alt="Kaspersky Antivirus"
                  className={classes.productImage}
                />
              </Grid>
              <Grid item xs={8}>
                <h2 className={classes.title_green}>
                  Avalie GRÁTIS por 30 dias
                </h2>
                <div className={classes.subTitle_green}>
                  *Depois R$ 6,70/mês
                </div>
                <div className={classes.productName}>
                  Kaspersky Internet Security
                </div>
                <div className={classes.plan}>Plano Mensal</div>
                <div className={classes.plan}>1 dispositivo</div>
              </Grid>
            </Grid>
          </Grid>
          <Grid
            container
            justify="center"
            alignItems="center"
            className={classes.savings}
          >
            <img src={porco} alt="Cofre de porquinho" style={{ height: 23 }} />
            Economia de R$100,00
          </Grid>
          <Grid
            container
            justify="center"
            alignItems="center"
            className={classes.flags}
          >
            <Grid item>
              <img src="" alt="" />
            </Grid>
          </Grid>
          <Grid
            container
            justify="space-between"
            alignItems="center"
            className={classes.flags}
          >
            <Grid item xs="auto">
              <img src={card1} alt="" style={{ height: 16 }} />
            </Grid>
            <Grid item xs="auto">
              <img src={card2} alt="" style={{ height: 16 }} />
            </Grid>
            <Grid item xs="auto">
              <img src={card3} alt="" style={{ height: 16 }} />
            </Grid>
            <Grid item xs="auto">
              <img src={card4} alt="" style={{ height: 16 }} />
            </Grid>
            <Grid item xs="auto">
              <img src={card5} alt="" style={{ height: 16 }} />
            </Grid>
            <Grid item xs="auto">
              <img src={card6} alt="" style={{ height: 16 }} />
            </Grid>
          </Grid>
          <Grid
            container
            justify="center"
            alignItems="center"
            className={classes.safeBuy}
          >
            <Grid item xs={12}>
              Compra Segura
            </Grid>
            <Grid item xs={12}>
              <img
                src={padlock}
                alt="Cofre de porquinho"
                style={{ height: 16 }}
              />
            </Grid>
          </Grid>
        </React.Fragment>
      );
    return (
      <React.Fragment>
        <Grid container className={classes.wrapper}>
          <Grid item xs={12}>
            <h1 className={classes.title}>Pedido Recebido</h1>
          </Grid>
          <Grid item xs={12} container justify="space-around">
            <Grid item xs="auto">
              <img
                src={check}
                alt="Kaspersky Antivirus"
                className={classes.productImage}
              />
            </Grid>
            <Grid item xs={8} container alignItems="center">
              <Grid item xs={12}>
                <div className={classes.thanksTitle}>Obrigado!</div>
                <div className={classes.thanksSubTitle}>
                  Agradecemos o seu pedido
                </div>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </React.Fragment>
    );
  }
}

export default DetailPurchase;
