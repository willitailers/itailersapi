import React, { Component } from 'react';
import MaskedInput from 'react-text-mask';
import classes from './signup-form.module.scss';
import {
  Grid,
  FormControl,
  InputLabel,
  Select,
  Input,
  MenuItem,
  Button,
  FormHelperText,
  Box,
  InputAdornment
} from '@material-ui/core';
import cardBack from '../../assets/images/card.svg';
import Check from '@material-ui/icons/Check';
import SafePurchase from '../safe-purchase/safe-purchase';
export class SignupForm extends Component {
  phoneMask = () => {
    let input = this.state.values.phone;
    let mask = [
      '(',
      /\d/,
      /\d/,
      ')',
      ' ',
      /\d/,
      /\d/,
      /\d/,
      /\d/,
      /\d/,
      '-',
      /\d/,
      /\d/,
      /\d/,
      /\d/
    ];
    if (input.replace(/[^\d]/g, '').length <= 10)
      mask = [
        '(',
        /\d/,
        /\d/,
        ')',
        ' ',
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        '-',
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/
      ];
    return mask;
  };
  isRequied = value => (!value || value === '' ? 'É obrigatório.' : '');
  minLength = (value, length) =>
    value.length < length ? 'O tam. mínimo é ' + length : '';
  maxLength = (value, length) =>
    value && value.length < length ? 'O tam. máximo é ' + length : '';
  isEmail = input =>
    /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(
      String(input).toLowerCase()
    )
      ? ''
      : 'Não é um e-mail válido';
  isCPF(strCPF) {
    strCPF = String(strCPF).replace(/\D/g, '');
    console.log('strCPF', strCPF);
    let Soma;
    let Resto;
    Soma = 0;
    if (strCPF === '00000000000') return 'CPF Inválido';
    if (strCPF === '11111111111') return 'CPF Inválido';

    for (let i = 1; i <= 9; i++)
      Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if (Resto === 10 || Resto === 11) Resto = 0;
    if (Resto !== parseInt(strCPF.substring(9, 10))) return 'CPF Inválido';

    Soma = 0;
    for (let i = 1; i <= 10; i++)
      Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if (Resto === 10 || Resto === 11) Resto = 0;
    if (Resto !== parseInt(strCPF.substring(10, 11))) return 'CPF Inválido';
    return '';
  }
  state = {
    mask: {
      cardYear: [/\d/, /\d/, /\d/, /\d/],
      cardCVV: [/\d/, /\d/, /\d/, /\d/],
      cardNumber: [
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/,
        /\d/
      ],
      CPF: [
        /\d/,
        /\d/,
        /\d/,
        '.',
        /\d/,
        /\d/,
        /\d/,
        '.',
        /\d/,
        /\d/,
        /\d/,
        '-',
        /\d/,
        /\d/
      ],
      phone: this.phoneMask
    },
    year: [2020],

    values: {
      name: '',
      email: '',
      phone: '',
      CPF: '',
      cardNumber: '',
      cardOwner: '',
      cardMonth: '',
      cardYear: '',
      cardCVV: ''
    },
    errors: {
      name: '',
      email: '',
      phone: '',
      CPF: '',
      cardNumber: '',
      cardOwner: '',
      cardMonth: '',
      cardYear: '',
      cardCVV: ''
    },
    validators: {
      name: [this.isRequied, input => this.minLength(input, 5)],
      email: [this.isRequied, this.isEmail],
      phone: [this.isRequied, input => this.minLength(input, 10)],
      CPF: [this.isRequied, this.isCPF],
      cardNumber: [this.isRequied],
      cardOwner: [this.isRequied, input => this.minLength(input, 5)],
      cardMonth: [this.isRequied],
      cardYear: [this.isRequied, input => this.minLength(input, 4)],
      cardCVV: [this.isRequied, input => this.minLength(input, 3)]
    },
    valid: {
      name: false,
      email: false,
      phone: false,
      CPF: false,
      cardNumber: false,
      cardOwner: false,
      cardMonth: false,
      cardYear: false,
      cardCVV: false
    }
  };
  static propTypes = {};

  InputMask = props => {
    const { inputRef, ...other } = props;
    let mask = this.state.mask[props.id];

    return (
      <MaskedInput
        {...other}
        ref={ref => {
          inputRef(ref ? ref.inputElement : null);
        }}
        mask={mask}
        placeholderChar={'\u2000'}
      />
    );
  };
  handleChange = input => event => {
    this.setState({
      errors: {
        ...this.state.errors,
        [input]: ''
      },
      values: {
        ...this.state.values,
        [input]: event.target.value
      }
    });
  };

  componentWillMount = () => {
    this.setState({
      year: [
        new Date().getFullYear(),
        new Date().getFullYear() + 1,
        new Date().getFullYear() + 2,
        new Date().getFullYear() + 3,
        new Date().getFullYear() + 4,
        new Date().getFullYear() + 5,
        new Date().getFullYear() + 6
      ]
    });
  };
  onBlur = input => event => {
    console.log('onblur', input);
    const { values, errors, validators, valid } = this.state;
    errors[input] = '';
    validators[input].map(func => {
      console.log('f', func(values[input]));
      if (errors[input] === '') errors[input] = func(values[input]);
    });
    if (errors[input] === '') valid[input] = true;
    else valid[input] = false;

    this.setState({ errors, valid });
  };
  onSubmit = () => {
    const { setSent } = this.props;
    const { values, errors, validators } = this.state;
    let hasErrors = false;
    Object.keys(values).map(el => {
      validators[el].map(func => {
        if (errors[el] === '') {
          errors[el] = func(values[el]);
          if (errors[el] !== '') hasErrors = true;
        }
      });
    });
    if (!hasErrors) {
      setSent(true);
    }
    this.setState({ errors });
  };
  render() {
    const { values, year, errors, valid } = this.state;
    const { sent } = this.props;

    if (!sent)
      return (
        <Grid container>
          <Grid item xs={12}>
            <form className={classes.container} onSubmit={this.onSubmit}>
              <Grid container className={classes.fieldWrapper} spacing={1}>
                <Grid item xs={12}>
                  <h1 className={classes.mainTitle}>
                    Assine com o cartão de crédito
                  </h1>
                  <div className={classes.mainSubTitle}>
                    Cancele a qualquer momento, sem custo*
                  </div>
                </Grid>
                <Grid item xs={12}>
                  <FormControl fullWidth error={errors.name !== ''}>
                    <InputLabel htmlFor="name">Nome</InputLabel>
                    <Input
                      value={values.name}
                      onChange={this.handleChange('name')}
                      id="name"
                      onBlur={this.onBlur('name')}
                      endAdornment={
                        valid.name ? (
                          <InputAdornment position="end">
                            <Check style={{ fill: '#7EFF33' }} />
                          </InputAdornment>
                        ) : (
                          undefined
                        )
                      }
                    />
                    <FormHelperText>{errors.name}</FormHelperText>
                  </FormControl>
                </Grid>
                <Grid item xs={12}>
                  <FormControl fullWidth error={errors.email !== ''}>
                    <InputLabel htmlFor="email">Email</InputLabel>
                    <Input
                      value={values.email}
                      type="email"
                      onChange={this.handleChange('email')}
                      id="email"
                      onBlur={this.onBlur('email')}
                      endAdornment={
                        valid.email ? (
                          <InputAdornment position="end">
                            <Check style={{ fill: '#7EFF33' }} />
                          </InputAdornment>
                        ) : (
                          undefined
                        )
                      }
                    />
                    <FormHelperText error={errors.email !== ''}>
                      {errors.email}
                    </FormHelperText>
                  </FormControl>
                </Grid>
                <Grid item xs={12}>
                  <FormControl fullWidth error={errors.phone !== ''}>
                    <InputLabel htmlFor="phone">Telefone</InputLabel>
                    <Input
                      value={values.phone}
                      onChange={this.handleChange('phone')}
                      id="phone"
                      inputComponent={this.InputMask}
                      onBlur={this.onBlur('phone')}
                      endAdornment={
                        valid.phone ? (
                          <InputAdornment position="end">
                            <Check style={{ fill: '#7EFF33' }} />
                          </InputAdornment>
                        ) : (
                          undefined
                        )
                      }
                    />
                    <FormHelperText error={errors.phone !== ''}>
                      {errors.phone}
                    </FormHelperText>
                  </FormControl>
                </Grid>
                <Grid item xs={12}>
                  <FormControl fullWidth error={errors.CPF !== ''}>
                    <InputLabel htmlFor="CPF">CPF</InputLabel>
                    <Input
                      value={values.CPF}
                      onChange={this.handleChange('CPF')}
                      id="CPF"
                      onBlur={this.onBlur('CPF')}
                      inputComponent={this.InputMask}
                      endAdornment={
                        valid.CPF ? (
                          <InputAdornment position="end">
                            <Check style={{ fill: '#7EFF33' }} />
                          </InputAdornment>
                        ) : (
                          undefined
                        )
                      }
                    />
                    <FormHelperText error={errors.CPF !== ''}>
                      {errors.CPF}
                    </FormHelperText>
                  </FormControl>
                </Grid>
                <Grid item xs={12}>
                  <FormControl fullWidth error={errors.cardNumber !== ''}>
                    <InputLabel htmlFor="cardNumber">
                      Número do cartão
                    </InputLabel>
                    <Input
                      value={values.cardNumber}
                      onChange={this.handleChange('cardNumber')}
                      id="cardNumber"
                      inputComponent={this.InputMask}
                      onBlur={this.onBlur('cardNumber')}
                      endAdornment={
                        valid.cardNumber ? (
                          <InputAdornment position="end">
                            <Check style={{ fill: '#7EFF33' }} />
                          </InputAdornment>
                        ) : (
                          undefined
                        )
                      }
                    />
                    <FormHelperText error={errors.cardNumber !== ''}>
                      {errors.cardNumber}
                    </FormHelperText>
                  </FormControl>
                </Grid>
                <Grid item xs={12}>
                  <FormControl fullWidth error={errors.cardOwner !== ''}>
                    <InputLabel htmlFor="cardOwner">Nome do Titular</InputLabel>
                    <Input
                      value={values.cardOwner}
                      onChange={this.handleChange('cardOwner')}
                      id="cardOwner"
                      onBlur={this.onBlur('cardOwner')}
                      endAdornment={
                        valid.cardOwner ? (
                          <InputAdornment position="end">
                            <Check style={{ fill: '#7EFF33' }} />
                          </InputAdornment>
                        ) : (
                          undefined
                        )
                      }
                    />
                    <FormHelperText error={errors.cardOwner !== ''}>
                      {errors.cardOwner}
                    </FormHelperText>
                  </FormControl>
                </Grid>
                <Grid
                  item
                  xs={12}
                  container
                  alignItems="center"
                  justify="space-between"
                  spacing={1}
                >
                  <Grid item xs={12} sm={6}>
                    <span style={{ textAlign: 'center' }}>
                      Data de Validade
                    </span>
                  </Grid>
                  <Grid item xs={6} sm={3}>
                    <FormControl fullWidth error={errors.cardMonth !== ''}>
                      <InputLabel htmlFor="cardMonth">Mês</InputLabel>
                      <Select
                        value={values.cardMonth}
                        onChange={this.handleChange('cardMonth')}
                        input={<Input name="cardMonth" id="cardMonth" />}
                        onBlur={this.onBlur('cardMonth')}
                        startAdornment={
                          valid.cardMonth ? (
                            <InputAdornment position="start">
                              <Check style={{ fill: '#7EFF33' }} />
                            </InputAdornment>
                          ) : (
                            undefined
                          )
                        }
                      >
                        <MenuItem value="" selected disabled>
                          <em>Selecione</em>
                        </MenuItem>
                        <MenuItem value="1">Janeiro</MenuItem>
                        <MenuItem value="2">Fevereiro</MenuItem>
                        <MenuItem value="3">Março</MenuItem>
                        <MenuItem value="4">Abril</MenuItem>
                        <MenuItem value="5">Maio</MenuItem>
                        <MenuItem value="6">Junho</MenuItem>
                        <MenuItem value="7">Julho</MenuItem>
                        <MenuItem value="8">Agosto</MenuItem>
                        <MenuItem value="9">Setembro</MenuItem>
                        <MenuItem value="10">Outubro</MenuItem>
                        <MenuItem value="11">Novembro</MenuItem>
                        <MenuItem value="12">Dezembro</MenuItem>
                      </Select>
                      <FormHelperText error={errors.cardMonth !== ''}>
                        {errors.cardMonth}
                      </FormHelperText>
                    </FormControl>
                  </Grid>
                  <Grid item xs={6} sm={3}>
                    <FormControl fullWidth error={errors.cardYear !== ''}>
                      <InputLabel htmlFor="cardYear">Ano</InputLabel>
                      <Select
                        value={values.cardYear}
                        onChange={this.handleChange('cardYear')}
                        input={<Input name="cardYear" id="cardYear" />}
                        onBlur={this.onBlur('cardYear')}
                        startAdornment={
                          valid.cardYear ? (
                            <InputAdornment position="start">
                              <Check style={{ fill: '#7EFF33' }} />
                            </InputAdornment>
                          ) : (
                            undefined
                          )
                        }
                      >
                        <MenuItem value="" selected disabled>
                          <em>Selecione</em>
                        </MenuItem>
                        {year.map(el => {
                          return (
                            <MenuItem value={el} key={el}>
                              {el}
                            </MenuItem>
                          );
                        })}
                      </Select>
                      <FormHelperText error={errors.cardYear !== ''}>
                        {errors.cardYear}
                      </FormHelperText>
                    </FormControl>
                  </Grid>
                </Grid>
                <Grid
                  item
                  xs={12}
                  container
                  alignItems="center"
                  justify="flex-start"
                >
                  <Grid item xs="auto">
                    <FormControl
                      style={{ width: '80px' }}
                      error={errors.cardCVV !== ''}
                    >
                      <InputLabel htmlFor="cardCVV">CVV</InputLabel>
                      <Input
                        value={values.cardCVV}
                        onChange={this.handleChange('cardCVV')}
                        id="cardCVV"
                        inputComponent={this.InputMask}
                        onBlur={this.onBlur('cardCVV')}
                        endAdornment={
                          valid.cardCVV ? (
                            <InputAdornment position="start">
                              <Check style={{ fill: '#7EFF33' }} />
                            </InputAdornment>
                          ) : (
                            undefined
                          )
                        }
                      />
                      <FormHelperText>{errors.cardCVV}</FormHelperText>
                    </FormControl>
                  </Grid>
                  <Grid item xs="auto">
                    <img
                      src={cardBack}
                      alt="Verso do cartão"
                      style={{
                        height: 17,
                        width: 'auto',
                        paddingTop: 5,
                        marginRight: 5,
                        marginLeft: 5
                      }}
                    />
                    <span className={classes.helperText}>
                      São os 3 números de segurança
                    </span>
                  </Grid>
                </Grid>
              </Grid>
            </form>
          </Grid>
          <Grid item xs={12}>
            <div className={classes.titleWrapper}>
              <div className={classes.title}>Kaspersky Internet Security</div>
              <div className={classes.subTitle}>
                <b>30 dias</b> de Avaliação Gratuita
              </div>
              <div className={classes.price}>R$ 6,70/mês</div>
            </div>
            <Button
              variant="contained"
              color="secondary"
              fullWidth
              onClick={this.onSubmit}
            >
              Assinar
            </Button>
          </Grid>
          <Grid item xs={12}>
            <p className={classes.detailParagraph}>
              A cobrança será realizada apenas após o término da avaliação
              gratuita.
            </p>
            <p className={classes.detailParagraph}>
              Ao assinar confirmo que li e concordo com os
              <span> Termos de venda</span> e a
              <span> Política de Privacidade. </span>
            </p>
            <SafePurchase />
          </Grid>
        </Grid>
      );
    return (
      <Box>
        <h1 className={classes.thanksTitle}>Instruções da compra</h1>
        <h2 className={classes.thanksSubTitle}>Seu pedido foi realizado</h2>
        <p className={classes.thanksPara}>
          Assim que a operadora de cartão aprovar o seu pagamento, iremos lhe
          enviar o código de ativação do seu produto no e-mail cadastrado:
        </p>
        <h2 className={classes.thanksSubTitle}>{values.email}</h2>
        <p className={classes.thanksPara}>
          Caso este e-mail não seja o correto, favor entrar com contato:
          falecom@antiviruslinktel.com.br
        </p>
        <p className={classes.thanksPara}>
          Enviaremos um e-mail de notificação assim que recebermos a confirmação
          do pagamento. Caso o pagamento não seja recebido dentro de 20 dias
          úteis, seu pedido será cancelado.
        </p>
        <h1 className={classes.thanksTitle}>INFORMAÇÕES SOBRE O PRODUTO</h1>
        <h2 className={classes.thanksSubTitle}>Kaspersky Anti-Virus</h2>
        <span className={classes.transferLink}>
          Transferência de arquivos <a href="www.google.com.br">(Download)</a>{' '}
        </span>
        <br />
        <span className={classes.transferDesc}>Quantidade: 1 Desktop 1 PC</span>
        <Button
          variant="contained"
          color="secondary"
          fullWidth
          className={classes.downloadButton}
        >
          Download Antivírus Kaspersky
        </Button>
      </Box>
    );
  }
}

export default SignupForm;
