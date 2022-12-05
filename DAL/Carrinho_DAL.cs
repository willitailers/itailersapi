using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Objetos;

namespace DAL
{
    public class Carrinho_DAL
    {
        public DataTable token_selecionar(string nr_token)
        {
            DataBase db = new DataBase();
            db.procedure = "p_token_consulta";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nr_token", nr_token));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }
        
        public DataTable produto_selecionar(string cd_produto_sub_publico, string nr_token = "")
        {
            DataBase db = new DataBase();
            db.procedure = "p_produto_sub_publico_selecionar";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@cd_produto_sub_publico", cd_produto_sub_publico));
            par.Add(db.retorna_parametros("@nr_token", nr_token));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public Carrinho inserir_carrinho(string nr_token)
        {
            DataTable dt = new DataTable();
            DataBase db = new DataBase();
            db.procedure = "p_carrinho_inserir";

            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@nm_token", nr_token));
            par.Add(db.retorna_parametros("@id_cliente", "0"));
            db.parametros = par;

            dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);

            if (dt.Rows.Count > 0)
            {
                return new Carrinho { id_carrinho = Int64.Parse(dt.Rows[0]["id_carrinho"].ToString()), cd_carrinho = dt.Rows[0]["cd_carrinho"].ToString(), id_cliente = "0", nr_token = dt.Rows[0]["nm_token"].ToString(), carrinhoItems = new List<CarrinhoItem>() };
            }
            else
            {
                return new Carrinho { id_carrinho = 0, cd_carrinho = "", id_cliente = "0", nr_token = "", carrinhoItems = new List<CarrinhoItem>() };
            }

        }

        public Carrinho carrinho_selecionar(string cd_carrinho, string id_carrinho)
        {
            DataTable dt = new DataTable();
            DataBase db = new DataBase();
            db.procedure = "p_carrinho_selecionar";

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@cd_carrinho", cd_carrinho));
            par.Add(db.retorna_parametros("@id_carrinho", id_carrinho));
            db.parametros = par;

            dt = Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);

            if (dt.Rows.Count > 0)
            {
                Carrinho cart = new Carrinho { id_carrinho = Int64.Parse(dt.Rows[0]["id_carrinho"].ToString()), cd_carrinho = dt.Rows[0]["cd_carrinho"].ToString(), id_cliente = dt.Rows[0]["id_cliente"].ToString(), nr_token = dt.Rows[0]["nm_token"].ToString()};
                cart.carrinhoItems = new List<CarrinhoItem>();

                foreach (DataRow dr in dt.Rows)
                {
                    cart.carrinhoItems.Add(new CarrinhoItem() {
                        id_produto = int.Parse(dr["id_produto"].ToString()),
                        id_produto_sub = int.Parse(dr["id_produto_sub"].ToString()),
                        nm_produto = dr["nm_produto"].ToString(),
                        nm_produto_sub = dr["nm_produto_sub"].ToString(),
                        quantidade_produto = int.Parse(dr["qtd_produto"].ToString()),
                        vl_produto = dr["vl_produto"].ToString(),
                        nm_produto_sub_descr_curta = dr["nm_produto_sub_descr_curta"].ToString(),
                        nm_caminho_img = dr["nm_caminho_img"].ToString(),
                        nr_trial = int.Parse(dr["nr_trial"].ToString())
                    });

                }

                return cart;
            }
            else
            {
                return new Carrinho { id_carrinho = 0, cd_carrinho = "", id_cliente = "0", nr_token = "", carrinhoItems = new List<CarrinhoItem>() };
            }

        }

        public string inserir_carrinho_item(string id_carrinho, string id_produto_sub, string vl_produto, string qtd_produto, string nr_trial, string id_produto_sub_especial, string dv_wifi)
        {
            DataBase db = new DataBase();
            db.procedure = "t_carrinho_item_inserir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_carrinho", id_carrinho));
            par.Add(db.retorna_parametros("@id_produto_sub", id_produto_sub));
            par.Add(db.retorna_parametros("@vl_produto", vl_produto));
            par.Add(db.retorna_parametros("@qtd_produto", qtd_produto));
            par.Add(db.retorna_parametros("@nr_trial", nr_trial));
            par.Add(db.retorna_parametros("@id_produto_sub_especial", id_produto_sub_especial));
            par.Add(db.retorna_parametros("@dv_wifi", dv_wifi));

            db.parametros = par;

            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public string inserir_cliente(  string id_cliente,
                                        string id_carrinho, 
                                        string nm_email, 
                                        string nm_cliente, 
                                        string endereco,
                                        string cep,
                                        string cidade,
                                        string estado,
                                        string nm_cliente_sobrenome,
                                        string nr_cpf,
                                        string id_tp_pessoa,
                                        string nm_inscricao,
                                        string endereco_nr,
                                        string bairro,
                                        string pais,
                                        string nm_razao_social,
                                        string nr_telefone,
                                        string v_sexo,
                                        string dt_nascimento)
        {
            DataBase db = new DataBase();
            db.procedure = "p_cliente_incluir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_cliente", id_cliente));
            par.Add(db.retorna_parametros("@id_carrinho", id_carrinho));
            par.Add(db.retorna_parametros("@nm_email", nm_email));
            par.Add(db.retorna_parametros("@nm_cliente", nm_cliente));
            par.Add(db.retorna_parametros("@nr_telefone", nr_telefone));
            par.Add(db.retorna_parametros("@nm_ramal", ""));
            par.Add(db.retorna_parametros("@nr_celular", ""));
            par.Add(db.retorna_parametros("@dt_nascimento", dt_nascimento));
            par.Add(db.retorna_parametros("@endereco", endereco));
            par.Add(db.retorna_parametros("@cep", cep));
            par.Add(db.retorna_parametros("@cidade", cidade));
            par.Add(db.retorna_parametros("@estado", estado));
            par.Add(db.retorna_parametros("@nm_cliente_sobrenome", nm_cliente_sobrenome));
            par.Add(db.retorna_parametros("@nr_cpf", nr_cpf));
            par.Add(db.retorna_parametros("@id_tp_pessoa", id_tp_pessoa));
            par.Add(db.retorna_parametros("@nm_inscricao", nm_inscricao));

            par.Add(db.retorna_parametros("@endereco_nr", endereco_nr));
            par.Add(db.retorna_parametros("@bairro", bairro));
            par.Add(db.retorna_parametros("@pais", pais));
            par.Add(db.retorna_parametros("@nm_razao_social", nm_razao_social));
            par.Add(db.retorna_parametros("@v_sexo", v_sexo));

            db.parametros = par;

            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable compra_inserir(string id_carrinho,
                                        string vl_compra,
                                        string vl_desconto,
                                        string id_sub_produto,
                                        string id_produto,
                                        string nr_cpf)
        {
            DataBase db = new DataBase();
            db.procedure = "p_compra_inserir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_carrinho", id_carrinho));
            par.Add(db.retorna_parametros("@vl_compra", vl_compra));
            par.Add(db.retorna_parametros("@vl_desconto", vl_desconto));
            par.Add(db.retorna_parametros("@id_sub_produto", id_sub_produto));
            par.Add(db.retorna_parametros("@id_produto", id_produto));
            par.Add(db.retorna_parametros("@nr_cpf", nr_cpf));

            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public string compra_id_subscricao_seleciona(string id_compra)
        {
            DataBase db = new DataBase();
            db.procedure = "p_compra_id_subscricao_seleciona";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));

            db.parametros = par;

            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public string compra_dados_pagamento_inserir(string id_compra,
                                        string id_tp_pagamento,
                                        string nr_cartao,
                                        string nm_cartao,
                                        string dt_cartao,
                                        string id_banco,
                                        string cc,
                                        string cc_dg,
                                        string ag,
                                        string ag_dg,
                                        string cvv)
        {
            DataBase db = new DataBase();
            db.procedure = "p_compra_dados_pagamento_inserir";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));
            par.Add(db.retorna_parametros("@id_tp_pagamento", id_tp_pagamento));
            par.Add(db.retorna_parametros("@nr_cartao", nr_cartao));
            par.Add(db.retorna_parametros("@nm_cartao", nm_cartao));
            par.Add(db.retorna_parametros("@dt_cartao", dt_cartao));
            par.Add(db.retorna_parametros("@id_banco", id_banco));
            par.Add(db.retorna_parametros("@cc", cc));
            par.Add(db.retorna_parametros("@cc_dg", cc_dg));
            par.Add(db.retorna_parametros("@ag", ag));
            par.Add(db.retorna_parametros("@ag_dg", ag_dg));
            par.Add(db.retorna_parametros("@cvv", cvv));

            db.parametros = par;

            return Generico.Exec_retorno_string(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable cliente_selecionar(string id_cliente)
        {
            DataBase db = new DataBase();
            db.procedure = "p_cliente_selecionar";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_cliente", id_cliente));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable compra_selecionar(string id_compra, string id_carrinho)
        {
            DataBase db = new DataBase();
            db.procedure = "p_compra_selecionar";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));
            par.Add(db.retorna_parametros("@id_carrinho", id_carrinho));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public DataTable pedido_selecionar(string id_compra, string id_carrinho, string id_inscricao)
        {
            DataBase db = new DataBase();
            db.procedure = "p_pedido_detalhe";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));
            par.Add(db.retorna_parametros("@id_carrinho", id_carrinho));
            par.Add(db.retorna_parametros("@id_inscricao", id_inscricao));
            db.parametros = par;

            return Generico.Exec_tabela(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public int compra_atualiza_licenca(string id_compra, string cd_ativacao, string link_ativacao)
        {
            DataBase db = new DataBase();
            db.procedure = "p_compra_ativar_licenca";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));
            par.Add(db.retorna_parametros("@cd_ativacao", cd_ativacao));
            par.Add(db.retorna_parametros("@link_ativacao", link_ativacao));
            db.parametros = par;

            return Generico.Exec_linhas_afetadas(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public int compra_atualiza_envio_email(string id_compra)
        {
            DataBase db = new DataBase();
            db.procedure = "p_compra_atualiza_envio_email";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));
            db.parametros = par;

            return Generico.Exec_linhas_afetadas(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public int compra_pagamento_aceito(string id_compra, string ic_recusado, string id_usuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_compra_atualiza_compra";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));
            par.Add(db.retorna_parametros("@ic_recusado", ic_recusado));
            par.Add(db.retorna_parametros("@id_usuario", id_usuario));
            db.parametros = par;

            return Generico.Exec_linhas_afetadas(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public int assinatura_cancelar(string id_compra, string id_usuario)
        {
            DataBase db = new DataBase();
            db.procedure = "p_assinatura_cancelar";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));
            par.Add(db.retorna_parametros("@id_usuario", id_usuario));
            db.parametros = par;

            return Generico.Exec_linhas_afetadas(db, DAL.Constantes_DAL.Conexao_AV);
        }

        public int compra_atualiza_assinatura(string id_compra, string card_hash, string card_id, string plan_id, Int32 subscribe_id)
        {
            DataBase db = new DataBase();
            db.procedure = "p_compra_ativar";

            parametros p = new parametros();
            List<parametros> par = new List<parametros>();

            par.Add(db.retorna_parametros("@id_compra", id_compra));
            par.Add(db.retorna_parametros("@card_hash", card_hash));
            //par.Add(db.retorna_parametros("@card_id", card_id));
            par.Add(db.retorna_parametros("@plan_id", plan_id));
            par.Add(db.retorna_parametros("@subscribe_id", subscribe_id.ToString()));
            db.parametros = par;

            return Generico.Exec_linhas_afetadas(db, DAL.Constantes_DAL.Conexao_AV);
        }
    }
}
