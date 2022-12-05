import React, { Component } from 'react';
import PropTypes from 'prop-types';
import classes from './testimonials.module.scss';
import { Grid } from '@material-ui/core';
import star from '../../assets/images/star.svg';
import ico0 from '../../assets/images/ico00.svg';
import ico1 from '../../assets/images/ico01.svg';
import ico2 from '../../assets/images/ico02.svg';
import ico3 from '../../assets/images/ico03.svg';
export class Testimonials extends Component {
  static propTypes = {};

  render() {
    const { sent } = this.props;

    return (
      <React.Fragment>
        {sent && (
          <React.Fragment>
            <Grid container className={classes.wrapperSent} spacing={2}>
              <Grid item xs={12}>
                <h1>Como ativar sua licença premium</h1>
              </Grid>
              <Grid
                item
                xs={12}
                container
                justify="space-between"
                alignItems="center"
                className={classes.icon}
              >
                <Grid
                  item
                  xs={12}
                  md={3}
                  container
                  alignItems="center"
                  justify="flex-start"
                >
                  <Grid item xs={12}>
                    <img src={ico0} alt="" />
                  </Grid>
                  <Grid item xs={12}>
                    <p>A sua licença é ativada no dia da compra.</p>
                  </Grid>
                </Grid>
                <Grid
                  item
                  xs={12}
                  md={3}
                  container
                  alignItems="center"
                  justify="center"
                >
                  <Grid item xs={12}>
                    <img src={ico1} alt="" />
                  </Grid>
                  <Grid item xs={12}>
                    <p>
                      Copie o código de ativação que você receberá em seu
                      e-mail.
                    </p>
                  </Grid>
                </Grid>
                <Grid item xs={12} md={3} container alignItems="center">
                  <Grid item xs={12}>
                    <img src={ico2} alt="" />
                  </Grid>
                  <Grid item xs={12}>
                    <p>Abra a sua janela do Kaspersky Antivírus.</p>
                  </Grid>
                </Grid>
                <Grid item xs={12} md={3} container alignItems="center">
                  <Grid item xs={12}>
                    <img src={ico3} alt="" />
                  </Grid>
                  <Grid item xs={12}>
                    <p>
                      Cole o seu código no campo que aparece e, a seguir, clique
                      no botão ‘Ativar’.
                    </p>
                  </Grid>
                </Grid>
              </Grid>
            </Grid>
            <Grid container className={classes.wrapperDetails} spacing={2}>
              <Grid item xs={12} md={6} className={classes.side}>
                <h1 className={classes.orderDetail}>
                  Informações sobre pedidos
                </h1>
                <p>
                  <b>Número do pedido:</b> 70
                </p>
                <p>
                  <b>Data do pedido:</b> 04/01/2019 10:54
                </p>
                <p>
                  Quando terminarmos de processar seu pedido, você receberá um
                  e-mail de confirmação no endereço fornecido.
                </p>
                <p>
                  <b>Endereço de Cobrança:</b>
                </p>
                <p>
                  Neemias Santos
                  <br />
                  neemias.ferreira@itailers.com.br
                </p>
              </Grid>
              <Grid item xs={12} md={6}>
                <h1 className={classes.orderDetail}>Dúvidas</h1>
                <p>
                  Perguntas sobre a ativação? Visite nossa Base de Conhecimento
                  ou contate o Suporte Técnico. Esta licença não pode ser
                  vendida ou ativada fora da América Latina, Estados Unidos da
                  América, Ilhas do Caribe e Ilhas Menores Distantes dos Estados
                  Unidos.
                </p>
              </Grid>
            </Grid>
          </React.Fragment>
        )}
        <Grid container className={classes.wrapper} spacing={2}>
          <Grid item xs={12} md={6}>
            <h1>CANAL TECH sobre Kaspersky antivírus</h1>
            <p>
              (…) top 3 das principais listas de melhores antivírus do mundo há
              tempos. Ele oferece recursos avançados de varredura e limpeza,
              além de ser capaz de desfazer ações realizadas por malwares e por
              isso está no topo desta lista.
            </p>
          </Grid>
          <Grid item xs={12} md={6}>
            <h1>
              <img src={star} alt="Estrela" />
              <img src={star} alt="Estrela" />
              <img src={star} alt="Estrela" />
              <img src={star} alt="Estrela" />
              <img src={star} alt="Estrela" />
              MELHOR COMPRA REALIZADA
            </h1>
            <p>
              Software muito bom. Após alguns dias comecei a conhecê-lo melhor e
              indico. Estou muito satisfeito com o desempenho do antivírus e com
              a navegação segura em meu pc. Não pretendo deixar de usá-lo.
              Recomendo para qualquer pessoa que esteja buscando proteção para o
              seu computador ou notebook. Parabéns!
            </p>
          </Grid>
        </Grid>
      </React.Fragment>
    );
  }
}

export default Testimonials;
