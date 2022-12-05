<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="KL_Vendas.Admin.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Usuario</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Usuario</h1>
			</div>
		</div><!--/.row-->
		<div class="alert alert-danger" runat="server" id="divErro" visible="false">
            <em class="fa fa-lg fa-warning">&nbsp;</em><asp:Label runat="server" ID="lblErro"></asp:Label>
		</div>	
        <div class="alert alert-success" runat="server" id="DivSucesso" visible="false">
            <em class="fa fa-lg fa-warning">&nbsp;</em><asp:Label runat="server" ID="lblSucesso"></asp:Label>
		</div>	
		
		<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">Cadastro/Pesquisa</div>
					<div class="panel-body">
						<div class="col-md-12"> 

                                <div class="form-group col-md-4">
									<label>Login:</label>
									<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
								</div>
                                <div class="form-group col-md-4">
									<label>Ativo:</label>
									<asp:DropDownList ID="ddlAtivo" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Ativo" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
									</asp:DropDownList>
								</div>

                                <div class="form-group col-md-4">
									<label>Nivel:</label>
									<asp:DropDownList runat="server" ID="ddlNivel" DataTextField="nm_usuario_nivel" DataValueField="id_usuario_nivel" CssClass="form-control"></asp:DropDownList>
								</div>

                                <div class="form-group  col-md-12 text-right">
                                    <asp:Button runat="server" ID="btnConsultar" Text="Buscar" CssClass="btn btn-primary" OnClick="btnConsultar_Click" />
                                    <button type="button" class="btn btn-success" data-toggle="modal" data-target="#exampleModal">
                                      + Incluir Usuario
                                    </button>
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                  <div class="modal-dialog " role="document">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Incluir Usuario</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                        </button>
                      </div>
                      <div class="modal-body">
                            <div class="form-group col-md-12">
							    <label>Login:</label>
							    <asp:TextBox runat="server" ID="txt_nm_email" CssClass="form-control"></asp:TextBox>
						    </div>
                            <div class="form-group col-md-12">
							    <label>Primeiro Nome:</label>
							    <asp:TextBox runat="server" ID="txt_nm_usuario_primeiro" CssClass="form-control"></asp:TextBox>
						    </div>
                            <div class="form-group col-md-12">
							    <label>Segundo Nome:</label>
							    <asp:TextBox runat="server" ID="txt_nm_usuario_segundo" CssClass="form-control"></asp:TextBox>
						    </div>
                            <div class="form-group col-md-12">
							    <label>Nivel:</label>
							    <asp:DropDownList runat="server" ID="ddlNivelCadastro" DataTextField="nm_usuario_nivel" DataValueField="id_usuario_nivel" CssClass="form-control"></asp:DropDownList>
						    </div>
                            <div class="form-group col-md-12">
							    <label>Senha:</label>
							    <asp:TextBox runat="server" ID="txtSenha" CssClass="form-control" TextMode="Password"></asp:TextBox>
						    </div>
                      </div>
                      <div class="modal-footer">
                        <asp:Button runat="server" ID="btnIncluirUsuario" CssClass="btn btn-success" Text="Incluir" OnClick="btnIncluirUsuario_Click" />
                      </div>
                    </div>
                  </div>
                </div>
				
				<div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Lista Usuarios</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                        <asp:GridView ID="grdUsuario" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="id_usuario" HeaderText="#" />
                                                <asp:BoundField DataField="nm_email" HeaderText="Login" />
                                                <asp:BoundField DataField="nm_usuario_primeiro" HeaderText="Usuario" />
                                                <asp:BoundField DataField="nm_usuario_nivel" HeaderText="Nivel" />
                                            </Columns>

                                        </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
              </div>
        </div>
    </div>
</asp:Content>
