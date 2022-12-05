(window.webpackJsonp = window.webpackJsonp || []).push([
    [0],
    [function (e, a, t) {
        e.exports = t(108)
    }, function (e, a, t) {
        "use strict";
        t.r(a);
        var n = t(0),
            r = t.n(n),
            l = t(8),
            i = t.n(l),
            c = (t(100), t(15)),
            s = t(13),
            m = t(17),
            o = t(16),
            d = t(18),
            u = t(50),
            p = t.n(u),
            E = t(134),
            g = t(136),
            h = (t(101), t(57)),
            f = t.n(h),
            v = t(58),
            b = t.n(v),
            A = function (e) {
                function a() {
                    return Object(c.a)(this, a), Object(m.a)(this, Object(o.a)(a).apply(this, arguments))
                }
                return Object(d.a)(a, e), Object(s.a)(a, [{
                    key: "render",
                    value: function () {
                        return r.a.createElement(E.a, {
                            color: "primary"
                        }, r.a.createElement(g.a, {
                            container: !0,
                            justify: "space-between",
                            alignItems: "center",
                            className: "header"
                        }, r.a.createElement(g.a, {
                            item: !0
                        }, r.a.createElement("img", {
                            className: "logo",
                            src: f.a,
                            alt: "Logo Linktel"
                        })), r.a.createElement(g.a, {
                            item: !0
                        }, r.a.createElement("img", {
                            className: "logo",
                            src: b.a,
                            alt: "Logo kaspersky"
                        }))))
                    }
                }]), a
            }(n.Component),
            x = t(86),
            y = Object(x.a)({
                palette: {
                    primary: {
                        main: "#FFF"
                    },
                    secondary: {
                        main: "#23C6A3",
                        contrastText: "#fff"
                    }
                },
                status: {
                    danger: "orange"
                }
            }),
            C = t(141),
            w = t(59),
            N = t.n(w),
            k = t(60),
            F = t.n(k),
            T = function (e) {
                function a() {
                    return Object(c.a)(this, a), Object(m.a)(this, Object(o.a)(a).apply(this, arguments))
                }
                return Object(d.a)(a, e), Object(s.a)(a, [{
                    key: "render",
                    value: function () {
                        return r.a.createElement(g.a, {
                            container: !0,
                            justify: "center",
                            alignItems: "center",
                            className: F.a.bar
                        }, r.a.createElement(g.a, {
                            item: !0
                        }, r.a.createElement("img", {
                            src: N.a,
                            alt: "cadeado"
                        }), "COMPRA SEGURA"))
                    }
                }]), a
            }(n.Component),
            O = t(140),
            V = t(142),
            I = t(40),
            Y = t(51),
            M = t(61),
            B = t(62),
            P = t.n(B),
            U = t(10),
            D = t.n(U),
            W = t(137),
            L = t(146),
            j = t(145),
            z = t(147),
            S = t(138),
            H = t(144),
            R = t(148),
            G = t(139),
            X = t(63),
            J = t.n(X),
            Q = t(28),
            K = t.n(Q),
            q = function (e) {
                function a() {
                    var e, t;
                    Object(c.a)(this, a);
                    for (var n = arguments.length, l = new Array(n), i = 0; i < n; i++) l[i] = arguments[i];
                    //return (t = Object(m.a)(this, (e = Object(o.a)(a)).call.apply(e, [this].concat(l)))).phoneMask = function () {
                    //    var e = ["(", /\d/, /\d/, ")", " ", /\d/, /\d/, /\d/, /\d/, /\d/, "-", /\d/, /\d/, /\d/, /\d/];
                    //    return t.state.values.phone.replace(/[^\d]/g, "").length <= 10 && (e = ["(", /\d/, /\d/, ")", " ", /\d/, /\d/, /\d/, /\d/, "-", /\d/, /\d/, /\d/, /\d/, /\d/]), e
                    //},
                        t.isRequied = function (e) {
                        return e && "" !== e ? "" : "\xc9 obrigat\xf3rio."
                    }, t.minLength = function (e, a) {
                        return e.length < a ? "O tam. m\xednimo \xe9 " + a : ""
                    }, t.maxLength = function (e, a) {
                        return e && e.length < a ? "O tam. m\xe1ximo \xe9 " + a : ""
                    }, t.isEmail = function (e) {
                        return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(String(e).toLowerCase()) ? "" : "N\xe3o \xe9 um e-mail v\xe1lido"
                    }, t.state = {
                        mask: {
                            cardYear: [/\d/, /\d/, /\d/, /\d/],
                            cardCVV: [/\d/, /\d/, /\d/, /\d/],
                            cardNumber: [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
                            CPF: [/\d/, /\d/, /\d/, ".", /\d/, /\d/, /\d/, ".", /\d/, /\d/, /\d/, "-", /\d/, /\d/],
                            phone: t.phoneMask
                        },
                        year: [2020],
                        values: {
                            name: "",
                            email: "",
                            phone: "",
                            CPF: "",
                            cardNumber: "",
                            cardOwner: "",
                            cardMonth: "",
                            cardYear: "",
                            cardCVV: ""
                        },
                        errors: {
                            name: "",
                            email: "",
                            phone: "",
                            CPF: "",
                            cardNumber: "",
                            cardOwner: "",
                            cardMonth: "",
                            cardYear: "",
                            cardCVV: ""
                        },
                        validators: {
                            name: [t.isRequied, function (e) {
                                return t.minLength(e, 5)
                            }],
                            email: [t.isRequied, t.isEmail],
                            phone: [t.isRequied, function (e) {
                                return t.minLength(e, 10)
                            }],
                            CPF: [t.isRequied, t.isCPF],
                            cardNumber: [t.isRequied],
                            cardOwner: [t.isRequied, function (e) {
                                return t.minLength(e, 5)
                            }],
                            cardMonth: [t.isRequied],
                            cardYear: [t.isRequied, function (e) {
                                return t.minLength(e, 4)
                            }],
                            cardCVV: [t.isRequied, function (e) {
                                return t.minLength(e, 3)
                            }]
                        },
                        valid: {
                            name: !1,
                            email: !1,
                            phone: !1,
                            CPF: !1,
                            cardNumber: !1,
                            cardOwner: !1,
                            cardMonth: !1,
                            cardYear: !1,
                            cardCVV: !1
                        }
                    }, t.InputMask = function (e) {
                        var a = e.inputRef,
                            n = Object(M.a)(e, ["inputRef"]),
                            l = t.state.mask[e.id];
                        return r.a.createElement(P.a, Object.assign({}, n, {
                            ref: function (e) {
                                a(e ? e.inputElement : null)
                            },
                            mask: l,
                            placeholderChar: "\u2000"
                        }))
                    }, t.handleChange = function (e) {
                        return function (a) {
                            t.setState({
                                errors: Object(Y.a)({}, t.state.errors, Object(I.a)({}, e, "")),
                                values: Object(Y.a)({}, t.state.values, Object(I.a)({}, e, a.target.value))
                            })
                        }
                    }, t.componentWillMount = function () {
                        t.setState({
                            year: [(new Date).getFullYear(), (new Date).getFullYear() + 1, (new Date).getFullYear() + 2, (new Date).getFullYear() + 3, (new Date).getFullYear() + 4, (new Date).getFullYear() + 5, (new Date).getFullYear() + 6]
                        })
                    }, t.onBlur = function (e) {
                        return function (a) {
                            console.log("onblur", e);
                            var n = t.state,
                                r = n.values,
                                l = n.errors,
                                i = n.validators,
                                c = n.valid;
                            l[e] = "", i[e].map(function (a) {
                                console.log("f", a(r[e])), "" === l[e] && (l[e] = a(r[e]))
                            }), "" === l[e] ? c[e] = !0 : c[e] = !1, t.setState({
                                errors: l,
                                valid: c
                            })
                        }
                    }, t.onSubmit = function () {
                        var e = t.props.setSent,
                            a = t.state,
                            n = a.values,
                            r = a.errors,
                            l = a.validators,
                            i = !1;
                        Object.keys(n).map(function (e) {
                            l[e].map(function (a) {
                                "" === r[e] && (r[e] = a(n[e]), "" !== r[e] && (i = !0))
                            })
                        }), i || e(!0), t.setState({
                            errors: r
                        })
                    }, t
                }
                return Object(d.a)(a, e), Object(s.a)(a, [{
                    key: "isCPF",
                    value: function (e) {
                        var a, t;
                        if (e = String(e).replace(/\D/g, ""), console.log("strCPF", e), a = 0, "00000000000" === e) return "CPF Inv\xe1lido";
                        if ("11111111111" === e) return "CPF Inv\xe1lido";
                        for (var n = 1; n <= 9; n++) a += parseInt(e.substring(n - 1, n)) * (11 - n);
                        if (10 !== (t = 10 * a % 11) && 11 !== t || (t = 0), t !== parseInt(e.substring(9, 10))) return "CPF Inv\xe1lido";
                        a = 0;
                        for (var r = 1; r <= 10; r++) a += parseInt(e.substring(r - 1, r)) * (12 - r);
                        return 10 !== (t = 10 * a % 11) && 11 !== t || (t = 0), t !== parseInt(e.substring(10, 11)) ? "CPF Inv\xe1lido" : ""
                    }
                }, {
                    key: "render",
                    value: function () {
                        var e = this.state,
                            a = e.values,
                            t = e.year,
                            n = e.errors,
                            l = e.valid;
                        return this.props.sent ? r.a.createElement(V.a, null, r.a.createElement("h1", {
                            className: D.a.thanksTitle
                        }, "Instru\xe7\xf5es da compra"), r.a.createElement("h2", {
                            className: D.a.thanksSubTitle
                        }, "Seu pedido foi realizado"), r.a.createElement("p", {
                            className: D.a.thanksPara
                        }, "Assim que a operadora de cart\xe3o aprovar o seu pagamento, iremos lhe enviar o c\xf3digo de ativa\xe7\xe3o do seu produto no e-mail cadastrado:"), r.a.createElement("h2", {
                            className: D.a.thanksSubTitle
                        }, a.email), r.a.createElement("p", {
                            className: D.a.thanksPara
                        }, "Caso este e-mail n\xe3o seja o correto, favor entrar com contato: falecom@antiviruslinktel.com.br"), r.a.createElement("p", {
                            className: D.a.thanksPara
                        }, "Enviaremos um e-mail de notifica\xe7\xe3o assim que recebermos a confirma\xe7\xe3o do pagamento. Caso o pagamento n\xe3o seja recebido dentro de 20 dias \xfateis, seu pedido ser\xe1 cancelado."), r.a.createElement("h1", {
                            className: D.a.thanksTitle
                        }, "INFORMA\xc7\xd5ES SOBRE O PRODUTO"), r.a.createElement("h2", {
                            className: D.a.thanksSubTitle
                        }, "Kaspersky Anti-Virus"), r.a.createElement("span", {
                            className: D.a.transferLink
                        }, "Transfer\xeancia de arquivos ", r.a.createElement("a", {
                            href: "www.google.com.br"
                        }, "(Download)"), " "), r.a.createElement("br", null), r.a.createElement("span", {
                            className: D.a.transferDesc
                        }, "Quantidade: 1 Desktop 1 PC"), r.a.createElement(G.a, {
                            variant: "contained",
                            color: "secondary",
                            fullWidth: !0,
                            className: D.a.downloadButton
                        }, "Download Antiv\xedrus Kaspersky")) : r.a.createElement(g.a, {
                            container: !0
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("form", {
                            className: D.a.container,
                            onSubmit: this.onSubmit
                        }, r.a.createElement(g.a, {
                            container: !0,
                            className: D.a.fieldWrapper,
                            spacing: 1
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("h1", {
                            className: D.a.mainTitle
                        }, "Assine com o cart\xe3o de cr\xe9dito"), r.a.createElement("div", {
                            className: D.a.mainSubTitle
                        }, "Cancele a qualquer momento, sem custo*")), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement(W.a, {
                            fullWidth: !0,
                            error: "" !== n.name
                        }, r.a.createElement(L.a, {
                            htmlFor: "name"
                        }, "Nome"), r.a.createElement(j.a, {
                            value: a.name,
                            onChange: this.handleChange("name"),
                            id: "name",
                            onBlur: this.onBlur("name"),
                            endAdornment: l.name ? r.a.createElement(z.a, {
                                position: "end"
                            }, r.a.createElement(K.a, {
                                style: {
                                    fill: "#7EFF33"
                                }
                            })) : void 0
                        }), r.a.createElement(S.a, null, n.name))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement(W.a, {
                            fullWidth: !0,
                            error: "" !== n.email
                        }, r.a.createElement(L.a, {
                            htmlFor: "email"
                        }, "Email"), r.a.createElement(j.a, {
                            value: a.email,
                            type: "email",
                            onChange: this.handleChange("email"),
                            id: "email",
                            onBlur: this.onBlur("email"),
                            endAdornment: l.email ? r.a.createElement(z.a, {
                                position: "end"
                            }, r.a.createElement(K.a, {
                                style: {
                                    fill: "#7EFF33"
                                }
                            })) : void 0
                        }), r.a.createElement(S.a, {
                            error: "" !== n.email
                        }, n.email))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement(W.a, {
                            fullWidth: !0,
                            error: "" !== n.phone
                        }, r.a.createElement(L.a, {
                            htmlFor: "phone"
                        }, "Telefone"), r.a.createElement(j.a, {
                            value: a.phone,
                            onChange: this.handleChange("phone"),
                            id: "phone",
                            inputComponent: this.InputMask,
                            onBlur: this.onBlur("phone"),
                            endAdornment: l.phone ? r.a.createElement(z.a, {
                                position: "end"
                            }, r.a.createElement(K.a, {
                                style: {
                                    fill: "#7EFF33"
                                }
                            })) : void 0
                        }), r.a.createElement(S.a, {
                            error: "" !== n.phone
                        }, n.phone))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement(W.a, {
                            fullWidth: !0,
                            error: "" !== n.CPF
                        }, r.a.createElement(L.a, {
                            htmlFor: "CPF"
                        }, "CPF"), r.a.createElement(j.a, {
                            value: a.CPF,
                            onChange: this.handleChange("CPF"),
                            id: "CPF",
                            onBlur: this.onBlur("CPF"),
                            inputComponent: this.InputMask,
                            endAdornment: l.CPF ? r.a.createElement(z.a, {
                                position: "end"
                            }, r.a.createElement(K.a, {
                                style: {
                                    fill: "#7EFF33"
                                }
                            })) : void 0
                        }), r.a.createElement(S.a, {
                            error: "" !== n.CPF
                        }, n.CPF))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement(W.a, {
                            fullWidth: !0,
                            error: "" !== n.cardNumber
                        }, r.a.createElement(L.a, {
                            htmlFor: "cardNumber"
                        }, "N\xfamero do cart\xe3o"), r.a.createElement(j.a, {
                            value: a.cardNumber,
                            onChange: this.handleChange("cardNumber"),
                            id: "cardNumber",
                            inputComponent: this.InputMask,
                            onBlur: this.onBlur("cardNumber"),
                            endAdornment: l.cardNumber ? r.a.createElement(z.a, {
                                position: "end"
                            }, r.a.createElement(K.a, {
                                style: {
                                    fill: "#7EFF33"
                                }
                            })) : void 0
                        }), r.a.createElement(S.a, {
                            error: "" !== n.cardNumber
                        }, n.cardNumber))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement(W.a, {
                            fullWidth: !0,
                            error: "" !== n.cardOwner
                        }, r.a.createElement(L.a, {
                            htmlFor: "cardOwner"
                        }, "Nome do Titular"), r.a.createElement(j.a, {
                            value: a.cardOwner,
                            onChange: this.handleChange("cardOwner"),
                            id: "cardOwner",
                            onBlur: this.onBlur("cardOwner"),
                            endAdornment: l.cardOwner ? r.a.createElement(z.a, {
                                position: "end"
                            }, r.a.createElement(K.a, {
                                style: {
                                    fill: "#7EFF33"
                                }
                            })) : void 0
                        }), r.a.createElement(S.a, {
                            error: "" !== n.cardOwner
                        }, n.cardOwner))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            container: !0,
                            alignItems: "center",
                            justify: "space-between",
                            spacing: 1
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            sm: 6
                        }, r.a.createElement("span", {
                            style: {
                                textAlign: "center"
                            }
                        }, "Data de Validade")), r.a.createElement(g.a, {
                            item: !0,
                            xs: 6,
                            sm: 3
                        }, r.a.createElement(W.a, {
                            fullWidth: !0,
                            error: "" !== n.cardMonth
                        }, r.a.createElement(L.a, {
                            htmlFor: "cardMonth"
                        }, "M\xeas"), r.a.createElement(H.a, {
                            value: a.cardMonth,
                            onChange: this.handleChange("cardMonth"),
                            input: r.a.createElement(j.a, {
                                name: "cardMonth",
                                id: "cardMonth"
                            }),
                            onBlur: this.onBlur("cardMonth"),
                            startAdornment: l.cardMonth ? r.a.createElement(z.a, {
                                position: "start"
                            }, r.a.createElement(K.a, {
                                style: {
                                    fill: "#7EFF33"
                                }
                            })) : void 0
                        }, r.a.createElement(R.a, {
                            value: "",
                            selected: !0,
                            disabled: !0
                        }, r.a.createElement("em", null, "Selecione")), r.a.createElement(R.a, {
                            value: "1"
                        }, "1"), r.a.createElement(R.a, {
                            value: "2"
                        }, "2"), r.a.createElement(R.a, {
                            value: "3"
                        }, "3"), r.a.createElement(R.a, {
                            value: "4"
                        }, "4"), r.a.createElement(R.a, {
                            value: "5"
                        }, "5"), r.a.createElement(R.a, {
                            value: "6"
                        }, "6"), r.a.createElement(R.a, {
                            value: "7"
                        }, "7"), r.a.createElement(R.a, {
                            value: "8"
                        }, "8"), r.a.createElement(R.a, {
                            value: "9"
                        }, "9"), r.a.createElement(R.a, {
                            value: "10"
                        }, "10"), r.a.createElement(R.a, {
                            value: "11"
                        }, "11"), r.a.createElement(R.a, {
                            value: "12"
                        }, "12")), r.a.createElement(S.a, {
                            error: "" !== n.cardMonth
                        }, n.cardMonth))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 6,
                            sm: 3
                        }, r.a.createElement(W.a, {
                            fullWidth: !0,
                            error: "" !== n.cardYear
                        }, r.a.createElement(L.a, {
                            htmlFor: "cardYear"
                        }, "Ano"), r.a.createElement(H.a, {
                            value: a.cardYear,
                            onChange: this.handleChange("cardYear"),
                            input: r.a.createElement(j.a, {
                                name: "cardYear",
                                id: "cardYear"
                            }),
                            onBlur: this.onBlur("cardYear"),
                            startAdornment: l.cardYear ? r.a.createElement(z.a, {
                                position: "start"
                            }, r.a.createElement(K.a, {
                                style: {
                                    fill: "#7EFF33"
                                }
                            })) : void 0
                        }, r.a.createElement(R.a, {
                            value: "",
                            selected: !0,
                            disabled: !0
                        }, r.a.createElement("em", null, "Selecione")), t.map(function (e) {
                            return r.a.createElement(R.a, {
                                value: e,
                                key: e
                            }, e)
                        })), r.a.createElement(S.a, {
                            error: "" !== n.cardYear
                        }, n.cardYear)))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            container: !0,
                            alignItems: "center",
                            justify: "flex-start"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement(W.a, {
                            style: {
                                width: "80px"
                            },
                            error: "" !== n.cardCVV
                        }, r.a.createElement(L.a, {
                            htmlFor: "cardCVV"
                        }, "CVV"), r.a.createElement(j.a, {
                            value: a.cardCVV,
                            onChange: this.handleChange("cardCVV"),
                            id: "cardCVV",
                            inputComponent: this.InputMask,
                            onBlur: this.onBlur("cardCVV"),
                            endAdornment: l.cardCVV ? r.a.createElement(z.a, {
                                position: "start"
                            }, r.a.createElement(K.a, {
                                style: {
                                    fill: "#7EFF33"
                                }
                            })) : void 0
                        }), r.a.createElement(S.a, null, n.cardCVV))), r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement("img", {
                            src: J.a,
                            alt: "Verso do cart\xe3o",
                            style: {
                                height: 17,
                                width: "auto",
                                paddingTop: 5,
                                marginRight: 5,
                                marginLeft: 5
                            }
                        }), r.a.createElement("span", {
                            className: D.a.helperText
                        }, "S\xe3o os 3 n\xfameros de seguran\xe7a")))))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("div", {
                            className: D.a.titleWrapper
                        }, r.a.createElement("div", {
                            className: D.a.title
                        }, "Kaspersky Internet Security"), r.a.createElement("div", {
                            className: D.a.subTitle
                        }, r.a.createElement("b", null, "30 dias"), " de Avalia\xe7\xe3o Gratuita"), r.a.createElement("div", {
                            className: D.a.price
                        }, "R$ 6,70/m\xeas")), r.a.createElement(G.a, {
                            variant: "contained",
                            color: "secondary",
                            fullWidth: !0,
                            onClick: this.onSubmit
                        }, "Assinar")), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("p", {
                            className: D.a.detailParagraph
                        }, "A cobran\xe7a ser\xe1 realizada apenas ap\xf3s o t\xe9rmino da avalia\xe7\xe3o gratuita."), r.a.createElement("p", {
                            className: D.a.detailParagraph
                        }, "Ao assinar confirmo que li e concordo com os", r.a.createElement("span", null, " Termos de venda"), " e a", r.a.createElement("span", null, " Pol\xedtica de Privacidade. ")), r.a.createElement(T, null)))
                    }
                }]), a
            }(n.Component),
            Z = t(68),
            _ = t.n(Z),
            $ = t(69),
            ee = t.n($),
            ae = t(70),
            te = t.n(ae),
            ne = t(71),
            re = t.n(ne),
            le = t(72),
            ie = t.n(le),
            ce = t(73),
            se = t.n(ce),
            me = t(74),
            oe = t.n(me),
            de = t(75),
            ue = t.n(de),
            pe = t(76),
            Ee = t.n(pe),
            ge = t(77),
            he = t.n(ge),
            fe = t(14),
            ve = t.n(fe),
            be = function (e) {
                function a() {
                    return Object(c.a)(this, a), Object(m.a)(this, Object(o.a)(a).apply(this, arguments))
                }
                return Object(d.a)(a, e), Object(s.a)(a, [{
                    key: "render",
                    value: function () {
                        return this.props.sent ? r.a.createElement(r.a.Fragment, null, r.a.createElement(g.a, {
                            container: !0,
                            className: ve.a.wrapper
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("h1", {
                            className: ve.a.title
                        }, "Pedido Recebido")), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            container: !0,
                            justify: "space-around"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement("img", {
                            src: ee.a,
                            alt: "Kaspersky Antivirus",
                            className: ve.a.productImage
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: 8,
                            container: !0,
                            alignItems: "center"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("div", {
                            className: ve.a.thanksTitle
                        }, "Obrigado!"), r.a.createElement("div", {
                            className: ve.a.thanksSubTitle
                        }, "Agradecemos o seu pedido")))))) : r.a.createElement(r.a.Fragment, null, r.a.createElement(g.a, {
                            container: !0,
                            className: ve.a.wrapper
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            style: {
                                paddingLeft: 5
                            }
                        }, r.a.createElement("h1", {
                            className: ve.a.title
                        }, "Meu Antiv\xedrus")), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            container: !0,
                            justify: "space-around"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement("img", {
                            src: _.a,
                            alt: "Kaspersky Antivirus",
                            className: ve.a.productImage
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: 8
                        }, r.a.createElement("h2", {
                            className: ve.a.title_green
                        }, "Avalie GR\xc1TIS por 30 dias"), r.a.createElement("div", {
                            className: ve.a.subTitle_green
                        }, "*Depois R$ 6,70/m\xeas"), r.a.createElement("div", {
                            className: ve.a.productName
                        }, "Kaspersky Internet Security"), r.a.createElement("div", {
                            className: ve.a.plan
                        }, "Plano Mensal"), r.a.createElement("div", {
                            className: ve.a.plan
                        }, "1 dispositivo")))), r.a.createElement(g.a, {
                            container: !0,
                            justify: "center",
                            alignItems: "center",
                            className: ve.a.savings
                        }, r.a.createElement("img", {
                            src: te.a,
                            alt: "Cofre de porquinho",
                            style: {
                                height: 23
                            }
                        }), "Economia de R$100,00"), r.a.createElement(g.a, {
                            container: !0,
                            justify: "center",
                            alignItems: "center",
                            className: ve.a.flags
                        }, r.a.createElement(g.a, {
                            item: !0
                        }, r.a.createElement("img", {
                            src: "",
                            alt: ""
                        }))), r.a.createElement(g.a, {
                            container: !0,
                            justify: "space-between",
                            alignItems: "center",
                            className: ve.a.flags
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement("img", {
                            src: ie.a,
                            alt: "",
                            style: {
                                height: 16
                            }
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement("img", {
                            src: se.a,
                            alt: "",
                            style: {
                                height: 16
                            }
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement("img", {
                            src: oe.a,
                            alt: "",
                            style: {
                                height: 16
                            }
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement("img", {
                            src: ue.a,
                            alt: "",
                            style: {
                                height: 16
                            }
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement("img", {
                            src: Ee.a,
                            alt: "",
                            style: {
                                height: 16
                            }
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: "auto"
                        }, r.a.createElement("img", {
                            src: he.a,
                            alt: "",
                            style: {
                                height: 16
                            }
                        }))), r.a.createElement(g.a, {
                            container: !0,
                            justify: "center",
                            alignItems: "center",
                            className: ve.a.safeBuy
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, "Compra Segura"), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("img", {
                            src: re.a,
                            alt: "Cofre de porquinho",
                            style: {
                                height: 16
                            }
                        }))))
                    }
                }]), a
            }(n.Component),
            Ae = t(29),
            xe = t.n(Ae),
            ye = t(37),
            Ce = t.n(ye),
            we = t(78),
            Ne = t.n(we),
            ke = t(79),
            Fe = t.n(ke),
            Te = t(80),
            Oe = t.n(Te),
            Ve = t(81),
            Ie = t.n(Ve),
            Ye = function (e) {
                function a() {
                    return Object(c.a)(this, a), Object(m.a)(this, Object(o.a)(a).apply(this, arguments))
                }
                return Object(d.a)(a, e), Object(s.a)(a, [{
                    key: "render",
                    value: function () {
                        var e = this.props.sent;
                        return r.a.createElement(r.a.Fragment, null, e && r.a.createElement(r.a.Fragment, null, r.a.createElement(g.a, {
                            container: !0,
                            className: xe.a.wrapperSent,
                            spacing: 2
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("h1", null, "Como ativar sua licen\xe7a premium")), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            container: !0,
                            justify: "space-between",
                            alignItems: "center",
                            className: xe.a.icon
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 3,
                            container: !0,
                            alignItems: "center",
                            justify: "flex-start"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("img", {
                            src: Ne.a,
                            alt: ""
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("p", null, "A sua licen\xe7a \xe9 ativada no dia da compra."))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 3,
                            container: !0,
                            alignItems: "center",
                            justify: "center"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("img", {
                            src: Fe.a,
                            alt: ""
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("p", null, "Copie o c\xf3digo de ativa\xe7\xe3o que voc\xea receber\xe1 em seu e-mail."))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 3,
                            container: !0,
                            alignItems: "center"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("img", {
                            src: Oe.a,
                            alt: ""
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("p", null, "Abra a sua janela do Kaspersky Antiv\xedrus."))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 3,
                            container: !0,
                            alignItems: "center"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("img", {
                            src: Ie.a,
                            alt: ""
                        })), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("p", null, "Cole o seu c\xf3digo no campo que aparece e, a seguir, clique no bot\xe3o \u2018Ativar\u2019."))))), r.a.createElement(g.a, {
                            container: !0,
                            className: xe.a.wrapperDetails,
                            spacing: 2
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 6,
                            className: xe.a.side
                        }, r.a.createElement("h1", {
                            className: xe.a.orderDetail
                        }, "Informa\xe7\xf5es sobre pedidos"), r.a.createElement("p", null, r.a.createElement("b", null, "N\xfamero do pedido:"), " 70"), r.a.createElement("p", null, r.a.createElement("b", null, "Data do pedido:"), " 04/01/2019 10:54"), r.a.createElement("p", null, "Quando terminarmos de processar seu pedido, voc\xea receber\xe1 um e-mail de confirma\xe7\xe3o no endere\xe7o fornecido."), r.a.createElement("p", null, r.a.createElement("b", null, "Endere\xe7o de Cobran\xe7a:")), r.a.createElement("p", null, "Neemias Santos", r.a.createElement("br", null), "neemias.ferreira@itailers.com.br")), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 6
                        }, r.a.createElement("h1", {
                            className: xe.a.orderDetail
                        }, "D\xfavidas"), r.a.createElement("p", null, "Perguntas sobre a ativa\xe7\xe3o? Visite nossa Base de Conhecimento ou contate o Suporte T\xe9cnico. Esta licen\xe7a n\xe3o pode ser vendida ou ativada fora da Am\xe9rica Latina, Estados Unidos da Am\xe9rica, Ilhas do Caribe e Ilhas Menores Distantes dos Estados Unidos.")))), r.a.createElement(g.a, {
                            container: !0,
                            className: xe.a.wrapper,
                            spacing: 2
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 6
                        }, r.a.createElement("h1", null, "CANAL TECH sobre Kaspersky antiv\xedrus"), r.a.createElement("p", null, "(\u2026) top 3 das principais listas de melhores antiv\xedrus do mundo h\xe1 tempos. Ele oferece recursos avan\xe7ados de varredura e limpeza, al\xe9m de ser capaz de desfazer a\xe7\xf5es realizadas por malwares e por isso est\xe1 no topo desta lista.")), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 6
                        }, r.a.createElement("h1", null, r.a.createElement("img", {
                            src: Ce.a,
                            alt: "Estrela"
                        }), r.a.createElement("img", {
                            src: Ce.a,
                            alt: "Estrela"
                        }), r.a.createElement("img", {
                            src: Ce.a,
                            alt: "Estrela"
                        }), r.a.createElement("img", {
                            src: Ce.a,
                            alt: "Estrela"
                        }), r.a.createElement("img", {
                            src: Ce.a,
                            alt: "Estrela"
                        }), "MELHOR COMPRA REALIZADA"), r.a.createElement("p", null, "Software muito bom. Ap\xf3s alguns dias comecei a conhec\xea-lo melhor e indico. Estou muito satisfeito com o desempenho do antiv\xedrus e com a navega\xe7\xe3o segura em meu pc. N\xe3o pretendo deixar de us\xe1-lo. Recomendo para qualquer pessoa que esteja buscando prote\xe7\xe3o para o seu computador ou notebook. Parab\xe9ns!"))))
                    }
                }]), a
            }(n.Component),
            Me = t(30),
            Be = t.n(Me),
            Pe = t(82),
            Ue = t.n(Pe),
            De = t(83),
            We = t.n(De),
            Le = t(84),
            je = t.n(Le),
            ze = t(85),
            Se = t.n(ze),
            He = function (e) {
                function a() {
                    return Object(c.a)(this, a), Object(m.a)(this, Object(o.a)(a).apply(this, arguments))
                }
                return Object(d.a)(a, e), Object(s.a)(a, [{
                    key: "render",
                    value: function () {
                        return r.a.createElement("div", {
                            className: Be.a.wrapper
                        }, r.a.createElement(O.a, null, r.a.createElement(g.a, {
                            container: !0,
                            className: Be.a.container
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 6,
                            container: !0,
                            alignItems: "center"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            container: !0,
                            alignItems: "center",
                            className: Be.a.mail
                        }, r.a.createElement("img", {
                            src: Ue.a,
                            alt: "Icone de carta"
                        }), "falecom@antiviruslinktel.com.br"), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("p", {
                            className: Be.a.address
                        }, "Itailers Gest\xe3o Empresarial e Marketing Ltda", r.a.createElement("br", null), "CNPJ: 10.876.124/0001-99", r.a.createElement("br", null), "Inscri\xe7\xe3o Estadual: 148.084.552.119", r.a.createElement("br", null), " Rua Doutor Gabriel Piza, 577 | S\xe3o Paulo \u2013 SP 02036-011"))), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            md: 6,
                            container: !0,
                            alignItems: "center"
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            container: !0,
                            alignItems: "center",
                            className: Be.a.mail,
                            style: {
                                opacity: 0
                            }
                        }, "falecom@antiviruslinktel.com.br"), r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement("span", {
                            className: Be.a.buy
                        }, "Compra Segura"), r.a.createElement("div", {
                            className: Be.a.certs
                        }, r.a.createElement("img", {
                            src: We.a,
                            alt: "Certificado SSL"
                        }), r.a.createElement("img", {
                            src: je.a,
                            alt: "Certificado Google"
                        }), r.a.createElement("img", {
                            src: Se.a,
                            alt: "Certificado Certisign"
                        })))))))
                    }
                }]), a
            }(n.Component),
            Re = function (e) {
                function a() {
                    var e, t;
                    Object(c.a)(this, a);
                    for (var n = arguments.length, r = new Array(n), l = 0; l < n; l++) r[l] = arguments[l];
                    //return (t = Object(m.a)(this, (e = Object(o.a)(a)).call.apply(e, [this].concat(r)))).state = {
                    //    sent: !1
                    //},
                        t.setSent = function (e) {
                        document.querySelector("html,body").scrollIntoView({
                            behavior: "smooth"
                        }), t.setState({
                            sent: e
                        })
                    }, t
                }
                return Object(d.a)(a, e), Object(s.a)(a, [{
                    key: "render",
                    value: function () {
                        return r.a.createElement(C.a, {
                            theme: y
                        }, r.a.createElement(A, null), r.a.createElement("div", {
                            className: p.a.bar
                        }, r.a.createElement(T, null)), r.a.createElement(O.a, {
                            className: p.a.container
                        }, r.a.createElement(g.a, {
                            container: !0,
                            spacing: 4
                        }, r.a.createElement(V.a, {
                            clone: !0,
                            order: {
                                xs: 1,
                                sm: 0
                            }
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            sm: 6
                        }, r.a.createElement(q, {
                            setSent: this.setSent,
                            sent: this.state.sent
                        }))), r.a.createElement(V.a, {
                            clone: !0,
                            order: {
                                xs: 0,
                                sm: 1
                            }
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12,
                            sm: 6
                        }, r.a.createElement(be, {
                            sent: this.state.sent
                        }))), r.a.createElement(V.a, {
                            clone: !0,
                            order: 3
                        }, r.a.createElement(g.a, {
                            item: !0,
                            xs: 12
                        }, r.a.createElement(Ye, {
                            sent: this.state.sent
                        }))))), r.a.createElement(He, null))
                    }
                }]), a
            }(n.Component);
        
        
    }],
    [
        [94, 1, 2]
    ]
]);
