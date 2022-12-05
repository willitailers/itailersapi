using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KL_Arquivo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string filenameUOL = "", filenameKL = "", msg_erro = "";
            divSucesso.Visible = false;
            divErro.Visible = false;

            try
            {
                if (FileUOL.PostedFile.ContentLength < 8388608 || FileKL.PostedFile.ContentLength < 8388608)
                {
                    if (FileUOL.HasFile || FileKL.HasFile)
                    {
                        //Aqui ele vai filtrar pelo tipo de arquivo
                        if (Path.GetExtension(FileUOL.FileName) != ".txt" || Path.GetExtension(FileKL.FileName) != ".txt")
                        {
                            // Mensagem notifica que é permitido carregar apenas 
                            // as imagens definida la em cima.
                            msg_erro = (string.IsNullOrEmpty(msg_erro) ? msg_erro : msg_erro + " | ") + "É permitido carregar apenas arquivos txt!";

                            divSucesso.Visible = false;
                            divErro.Visible = true;
                            lblErro.Text = msg_erro;
                            return;
                        }
                    }
                    else
                    {
                        // nao enviou os 2 arquivos
                        msg_erro = (string.IsNullOrEmpty(msg_erro) ? msg_erro : msg_erro + " | ") + "Favor enviar os dois arquivos";

                        divSucesso.Visible = false;
                        divErro.Visible = true;
                        lblErro.Text = msg_erro;
                        return;
                    }
                }
                else
                {
                    // Mensagem notifica quando imagem é superior a 8 MB
                    msg_erro = (string.IsNullOrEmpty(msg_erro) ? msg_erro : msg_erro + " | ") + "Não é permitido carregar mais do que 8 MB";

                    divSucesso.Visible = false;
                    divErro.Visible = true;
                    lblErro.Text = msg_erro;
                    return;
                }


                //Pega o nome do arquivo
                string nomeUOL = System.IO.Path.GetFileName(FileUOL.FileName);
                //Pega a extensão do arquivo
                //string extensaoUOL = Path.GetExtension(FileUOL.FileName);
                //Gera nome novo do Arquivo numericamente
                filenameUOL = DateTime.Now.ToString("yyyyMMddHHmmss") + nomeUOL;
                //Caminho a onde será salvo
                FileUOL.SaveAs(ConfigurationManager.AppSettings.Get("path_comprovantes") + filenameUOL);

                //Pega o nome do arquivo
                string nomeKL = System.IO.Path.GetFileName(FileKL.FileName);
                //Pega a extensão do arquivo
                //string extensaoKL = Path.GetExtension(FileKL.FileName);
                //Gera nome novo do Arquivo numericamente
                filenameKL =  DateTime.Now.ToString("yyyyMMddHHmmss") + nomeKL;
                //Caminho a onde será salvo
                FileKL.SaveAs(ConfigurationManager.AppSettings.Get("path_comprovantes") + filenameKL);

                string mensagem = "";
                // cadastra arquivo na base
                int retorno = new BLL.Arquivo_BLL().CarregaArquivo(FileUOL.PostedFile.InputStream, nomeUOL, FileKL.PostedFile.InputStream, nomeKL, ref mensagem);

                if (retorno < 0)
                {
                    divSucesso.Visible = false;
                    divErro.Visible = true;
                    lblErro.Text = mensagem;
                    return;
                }
                else
                {
                    divSucesso.Visible = true;
                    divErro.Visible = false;
                    lblSucesso.Text = mensagem;
                    return;
                }

                // ativa as licenças

                //divSucesso.Visible = false;
                //divErro.Visible = true;
            }
            catch(Exception ex)
            {
                divSucesso.Visible = false;
                divErro.Visible = true;
                lblErro.Text = "Erro - " + ex.Message;
            }
            
        }
    }
}