import { createMuiTheme } from '@material-ui/core/styles';

const LinkTelTheme = createMuiTheme({
  palette: {
    primary: {
      main: '#FFF'
    },
    secondary: {
      main: '#23C6A3',
      contrastText: '#fff'
    }
  },
  status: {
    danger: 'orange'
  }
});

export default LinkTelTheme;
