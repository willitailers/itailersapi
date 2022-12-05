using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Objetos;
using BLL;

namespace KL_Vendas.Admin
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEmail.Text = "";
                Consulta_nivel();
                Consulta();
            }
        }

        protected void Consulta()
        {
            MostraMensagem(false, false, "", "");
            DataTable dt = new DataTable();

            Objetos.Usuario user = new Objetos.Usuario();
            user.nm_email = string.IsNullOrEmpty(txtEmail.Text) ? "" : txtEmail.Text;
            user.id_usuario_nivel = int.Parse(ddlNivel.SelectedItem.Value);
            user.dv_ativo = int.Parse(ddlAtivo.SelectedItem.Value);

            dt = new Usuario_BLL().usuario_consulta(user);

            grdUsuario.DataSource = dt;
            grdUsuario.DataBind();

        }

        protected void Consulta_nivel()
        {
            
            DataTable dt = new DataTable();

            dt = new Usuario_BLL().usuario_nivel_consulta(0);

            ddlNivel.DataSource = dt;
            ddlNivel.DataBind();
            ddlNivel.Items.Insert(0, new ListItem("Selecione", "0"));

            ddlNivelCadastro.DataSource = dt;
            ddlNivelCadastro.DataBind();

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            Consulta();
        }

        protected void MostraMensagem(bool visivel_erro, bool visivel_sucesso, string msg_erro, string msg_sucesso)
        {
            divErro.Visible = visivel_erro;
            DivSucesso.Visible = visivel_sucesso;
            lblErro.Text = msg_erro;
            lblSucesso.Text = msg_sucesso;
        }
        
        protected void btnIncluirUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Objetos.Usuario user = new Objetos.Usuario();
                user.nm_email = txt_nm_email.Text;
                user.nm_senha = txtSenha.Text;
                user.nm_usuario_primeiro = txt_nm_usuario_primeiro.Text;
                user.nm_usuario_segundo = txt_nm_usuario_segundo.Text;
                user.id_usuario_nivel = int.Parse(ddlNivelCadastro.SelectedItem.Value);

                int id_usuario = new Usuario_BLL().usuario_inserir(user);

                if (id_usuario > 0)
                {
                    Consulta();
                    MostraMensagem(false, true, "", "Usuario inserido.");
                }
                else
                {
                    MostraMensagem(true, false, "Nome de usuario ja existe no sistema.", "");
                }


            }
            catch (Exception ex)
            {
                MostraMensagem(true, false, "Erro ao Inserir. " + ex.Message, "");
            }
        }
    }
}