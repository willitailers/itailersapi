import React, { Component } from 'react';
import { AppBar, Grid } from '@material-ui/core';
import './header.scss';
import logoLinktel from '../../assets/images/logo-linktel.svg';
import logoKaspersky from '../../assets/images/logo-kaspersky.svg';
export class Header extends Component {
  static propTypes = {};

  render() {
    return (
      <AppBar color="primary">
        <Grid
          container
          justify="space-between"
          alignItems="center"
          className="header"
        >
          <Grid item>
            <img className="logo" src={logoLinktel} alt="Logo Linktel" />
          </Grid>
          <Grid item>
            <img className="logo" src={logoKaspersky} alt="Logo kaspersky" />
          </Grid>
        </Grid>
      </AppBar>
    );
  }
}

export default Header;
