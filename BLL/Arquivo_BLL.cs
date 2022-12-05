using BLL.KL_API;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Arquivo_BLL
    {
        public int CarregaArquivo(Stream arquivo_uol, string nm_arquivo_uol, Stream arquivo_kl, string nm_arquivo_kl, ref string mensagem)
        {
            int retorno = 0, quantidade_uol = 0, quantidade_kl = 0, controle = 1, controle_max = 0;
            int id_lote_fornecedor = 0, id_lote_kl = 0;
            List<string> linhas_uol = new List<string>();
            List<string> linhas_kl = new List<string>();
            try
            {
                new Arquivo_UOL().log_inserir("Inicio do lote: " + nm_arquivo_uol, "100");
                // faz looping para validar se numero dos dois arquivos bate
                string inputContent2;
                using (StreamReader inputStreamReader = new StreamReader(arquivo_uol))
                {
                    while ((inputContent2 = inputStreamReader.ReadLine()) != null)
                        if (!string.IsNullOrEmpty(inputContent2))
                        {
                            linhas_uol.Add(inputContent2);
                            quantidade_uol++;
                        }
                }

                using (StreamReader inputStreamReader = new StreamReader(arquivo_kl))
                {
                    while ((inputContent2 = inputStreamReader.ReadLine()) != null)
                        if (!string.IsNullOrEmpty(inputContent2))
                        {
                            linhas_kl.Add(inputContent2);
                            quantidade_kl++;
                        }
                        
                }

                // valida quantidade de registros
                if (quantidade_uol != quantidade_kl)
                {
                    retorno = -1;
                    mensagem = "Quantidade de licenças diferente da quantidade de codigos de validação.";
                    new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_uol, "-1");
                    return retorno;
                }

                controle_max = quantidade_uol;

                string cd_validacao = "", cd_ativacao_kl = "";
                Int64 cd_validacao_v = 0;
                // validar informações
                foreach (string inputContent in linhas_uol)
                {
                    if (!string.IsNullOrEmpty(inputContent))
                        if (inputContent.Length < 12)
                        {
                            retorno = -1;
                            mensagem = "Arquivo do fornecedor com dados incorretos na linha " + controle.ToString() + " (linha com menos de 12 carascteres).";
                            new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_uol, "-1");
                            return retorno;
                        }
                        else
                        {
                            cd_validacao = inputContent.Substring(4, 8);

                            if (cd_validacao.Contains(" "))
                            {
                                retorno = -1;
                                mensagem = "Arquivo do fornecedor com dados incorretos na linha " + controle.ToString() + " (espaço em branco).";
                                new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_uol, "-1");
                                return retorno;
                            }

                            if (!Int64.TryParse(cd_validacao, out cd_validacao_v))
                            {
                                retorno = -1;
                                mensagem = "Arquivo do fornecedor com dados incorretos na linha " + controle.ToString() + " (codigo de validacao nao numerico).";
                                new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_uol, "-1");
                                return retorno;
                            }
                        }

                    controle++;
                }

                controle = 1;
                foreach (string inputContent in linhas_kl)
                {
                    if (!string.IsNullOrEmpty(inputContent))
                        if (inputContent.Length < 18)
                        {
                            retorno = -1;
                            mensagem = "Arquivo da KL com dados incorretos na linha " + controle.ToString() + " (linha com menos de 8 carascteres).";
                            new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_uol, "-1");
                            return retorno;
                        }
                        else
                        {
                            cd_validacao = inputContent.Substring(0, 20);
                            cd_ativacao_kl = inputContent.Substring(21, 23);
                            if (cd_validacao.Contains(" "))
                            {
                                retorno = -1;
                                mensagem = "Arquivo da KL com dados incorretos na linha " + controle.ToString() + " (espaço em branco no subscribe).";
                                new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_uol, "-1");
                                return retorno;
                            }

                            if (cd_ativacao_kl.Contains(" "))
                            {
                                retorno = -1;
                                mensagem = "Arquivo da KL com dados incorretos na linha " + controle.ToString() + " (espaço em branco na licença).";
                                new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_uol, "-1");
                                return retorno;
                            }
                        }

                    controle++;
                }

                controle = 1;

                new Arquivo_UOL().log_inserir(nm_arquivo_kl, "101");

                id_lote_fornecedor = new Arquivo_UOL().insere_lote_fornecedor(nm_arquivo_uol, quantidade_uol.ToString());

                if (id_lote_fornecedor < 0)
                {
                    retorno = -1;
                    mensagem = id_lote_fornecedor == -1 ? "Ja existe um arquivo do fornecedor com o mesmo nome" : "Erro ao cadastrar lote do fornecedor.";
                    new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_uol, "103");
                    return retorno;
                }

                foreach (string inputContent in linhas_uol)
                {

                    if (!string.IsNullOrEmpty(inputContent))
                    {
                        int id_lote_fornecedor_item = new Arquivo_UOL().insere_lote_fornecedor_item(id_lote_fornecedor.ToString(), inputContent.Substring(4, 8), controle.ToString());

                        if (id_lote_fornecedor_item < 0)
                        {
                            new Arquivo_UOL().lote_deletar(id_lote_fornecedor.ToString());
                            retorno = -1;
                            mensagem = id_lote_fornecedor_item == -1 ? ("codigo de validacao ja existe - codigo: " + inputContent.Substring(4, 8) + " Linha: " + controle.ToString()) : "Erro ao inserir item do lote do fornecedor.";
                            new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_uol, "103");
                            return retorno;
                        }
                        controle++;
                    }

                }

                new Arquivo_UOL().log_inserir(nm_arquivo_kl, "102");
                controle = 1;

                new Arquivo_UOL().log_inserir(nm_arquivo_kl, "201");
                id_lote_kl = new Arquivo_UOL().insere_lote_kl(quantidade_kl.ToString(), nm_arquivo_kl, id_lote_fornecedor.ToString());

                if (id_lote_kl < 0)
                {
                    new Arquivo_UOL().lote_deletar(id_lote_fornecedor.ToString());
                    retorno = -1;
                    mensagem = id_lote_kl == -1 ? "Ja existe um arquivo da KL com o mesmo nome." : "Erro ao cadastrar lote da KL.";
                    new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_kl, "203");
                    return retorno;
                }

                foreach (string inputContent in linhas_kl)
                {
                    if (!string.IsNullOrEmpty(inputContent))
                    {
                        int id_lote_kl_item = new Arquivo_UOL().insere_lote_kl_item(id_lote_kl.ToString(),
                                                                                    id_lote_fornecedor.ToString(),
                                                                                    inputContent.Substring(0, 20),
                                                                                    "KISA",
                                                                                    "1",
                                                                                    "",
                                                                                    "pt",
                                                                                    "",
                                                                                    controle.ToString(),
                                                                                    inputContent.Substring(21, 23));

                        if (id_lote_kl_item < 0)
                        {
                            new Arquivo_UOL().lote_deletar(id_lote_fornecedor.ToString());
                            retorno = -1;
                            mensagem = id_lote_kl_item == -1 ? ("codigo de inscricao ja existe - codigo: " + inputContent.Substring(0, 20) + " Linha: " + controle.ToString()) : "Erro ao inserir item do lote da KL.";
                            new Arquivo_UOL().log_inserir(mensagem + " " + nm_arquivo_kl, "203");
                            return retorno;
                        }
                        controle++;
                    }

                }

                new Arquivo_UOL().log_inserir(nm_arquivo_kl, "202");

                mensagem = "Arquivos processados com sucesso. " + quantidade_uol + " licenças importadas.";
                return retorno;
            }
            catch(Exception ex)
            {
                new Arquivo_UOL().log_inserir("Erro no cadastro do lote: " + ex.Message, "-1");
                mensagem = ex.Message;
                retorno = -1;
                return retorno;
            }
        }

        public void Cancelamento_licencas()
        {
            string id_subscribe = "";
            try
            {
                var dt = new Arquivo_UOL().busca_licenca_cancelamento();

                foreach(DataRow dr in dt.Rows)
                {
                    SubscriptionResponseContainer container = new SubscriptionResponseContainer();

                    id_subscribe = dr["id_subscribe"].ToString();
                    container = new KL_Conexao().CancelamentoUOL(dr["cd_validacao"].ToString(), dr["id_subscribe"].ToString());

                    bool sucesso = true;
                    foreach (object ret in container.Items)
                    {
                        bool erro = true;
                        // verifica se deu erro
                        try
                        {
                            SubscriptionResponseErrorCollection itemErro = new SubscriptionResponseErrorCollection();

                            itemErro = (SubscriptionResponseErrorCollection)ret;
                            erro = false;
                            new Arquivo_UOL().log_inserir("Erro no KL cancelamento de assinatura[1] - " + id_subscribe + " " + itemErro.Items[0].ErrorMessage, "14");

                        }
                        catch (Exception ex)
                        {
                            //new Arquivo_UOL().log_inserir("Erro no KL cancelamento de assinatura[2] - " + id_subscribe + " " + ex.Message, "14");
                        }

                        // se nao deu erro, foi sucesso
                        if (erro)
                            try
                            {
                                SubscriptionResponseItemCollection item = new SubscriptionResponseItemCollection();
                                item = (SubscriptionResponseItemCollection)ret;

                                if (item.ItemsElementName[0] == ItemsChoiceType.HardCancel)
                                    new Arquivo_UOL().Licenca_cancelada(dr["id_lote_kl_item"].ToString());
                                else
                                    new Arquivo_UOL().log_inserir("Erro no KL cancelamento de assinatura[3] - " + id_subscribe + " nao foi Hard Cancel", "14");

                            }
                            catch (Exception ex)
                            {
                                new Arquivo_UOL().log_inserir("Erro no KL cancelamento de assinatura[2] - " + id_subscribe + " " + ex.Message, "14");
                            }
                    }

                }

            }
            catch(Exception ex)
            {
                new Arquivo_UOL().log_inserir("Erro no KL cancelamento de assinatura[3] - " + id_subscribe + " " + ex.Message, "14");
            }
        }
    }
}
