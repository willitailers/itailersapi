import React, { Component } from 'react';
import { Grid } from '@material-ui/core';
import padlock from '../../assets/images/padlock.svg';
import classes from './safe-purchase.module.scss';
export class SafePurchase extends Component {
  static propTypes = {};

  render() {
    return (
      <Grid
        container
        justify="center"
        alignItems="center"
        className={classes.bar}
      >
        <Grid item>
          <img src={padlock} alt="cadeado" />
          COMPRA SEGURA
        </Grid>
      </Grid>
    );
  }
}

export default SafePurchase;
