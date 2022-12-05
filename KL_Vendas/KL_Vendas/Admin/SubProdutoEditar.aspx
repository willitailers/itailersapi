<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="SubProdutoEditar.aspx.cs" Inherits="KL_Vendas.Admin.SubProdutoEditar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="lblIdProdutoSub"></asp:Label>
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Sub Produto</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Sub Produto Editar</h1>
			</div>
		</div><!--/.row-->
		<div class="row" runat="server" id="divErro" visible="false">
			<div class="col-lg-12">
				<h1 class="alert bg-danger"><asp:Label runat="server" ID="lblErro"></asp:Label></h1>
			</div>
		</div>		
		
		<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">Cadastro/Pesquisa</div>
					<div class="panel-body">
						<div class="col-md-12"> 
                                <div class="form-group col-md-4">
								    <label>Produto:</label>
								    <asp:DropDownList ID="ddlProduto" runat="server" DataValueField="id_produto" DataTextField="nm_produto" CssClass="form-control" Enabled="false"></asp:DropDownList>
							    </div>

                                <div class="form-group col-md-4">
									<label>Nome Sub-Produto:</label>
									<asp:TextBox runat="server" ID="txtProdutoSub" CssClass="form-control" Enabled="false"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Valor Sub-Produto</label>
                                    <asp:TextBox runat="server" ID="txtValorSUb" CssClass="form-control" Enabled="false"></asp:TextBox>
								</div> 
                                
                                <div class="form-group col-md-4">
									<label>Valor "De" Sub-Produto</label>
                                    <asp:TextBox runat="server" ID="txtValorDeProdutoSub" CssClass="form-control"></asp:TextBox>
								</div>
                                
                                <div class="form-group col-md-4">
                                    <label>Desconto?</label>
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox runat="server" ID="chkDesconto"  />
                                    </div>
                                </div>

                                <div class="form-group col-md-4">
									<label>% de Desconto</label>
                                    <asp:TextBox runat="server" ID="txtValorDesconto" CssClass="form-control"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Sub_Descrição curta:</label>
									<asp:TextBox runat="server" ID="txtProdutoSUbDescrCurta" CssClass="form-control" Rows="3" TextMode="MultiLine" Enabled="false"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Sub_Descrição completa:</label>
									<asp:TextBox runat="server" ID="txtProdutoSubDescrCompleta" CssClass="form-control" Rows="3" TextMode="MultiLine" Enabled="false"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Sub_Descrição completa 2:</label>
									<asp:TextBox runat="server" ID="txtProdutoSubDescrCompleta2" CssClass="form-control" Rows="3" TextMode="MultiLine" Enabled="false"></asp:TextBox>
								</div>
                                
                                <div class="form-group col-md-4">
								    <label>Tipo Produto:</label>
								    <asp:DropDownList ID="ddlTipoProduto" runat="server" DataValueField="id_tp_produto" DataTextField="nm_tp_produto" CssClass="form-control" Enabled="false"></asp:DropDownList>
							    </div>

                                <div class="form-group col-md-4">
                                    <label>Ativo:</label>
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox runat="server" ID="cbProdutoSub" Enabled="false"  />
                                    </div>
                                </div>

                                <div class="form-group  col-md-8 text-right">
                                    <asp:Button runat="server" ID="btnCondicao" Text="Criar Condição Especial" CssClass="btn btn-warning" OnClick="btnCondicao_Click" Enabled="false" />
                                    <asp:Button runat="server" ID="btnProdutoSubIncluir" Text="Alterar Sub Produto" CssClass="btn btn-dark" OnClick="btnProdutoSubIncluir_Click" Enabled="false" />
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
                <asp:Panel runat="server" ID="PnlCE" Visible="false">
                    <asp:Label runat="server" ID="lblCdp" Visible="false"></asp:Label>
                    <div class="row">
			            <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">Condição Especial</div>
                                <div class="panel-body">
						            <div class="col-md-12"> 
                                        <div class="row">
                                            <div class="form-group col-md-4">
									            <label>Valor de Venda</label>
                                                <asp:TextBox runat="server" ID="txtCEVl_Venda" CssClass="form-control" MaxLength="10" ></asp:TextBox>
								            </div>
                                            <div class="form-group col-md-4">
									            <label>Dias de Trial</label>
                                                <asp:TextBox runat="server" ID="txtTrial" CssClass="form-control" MaxLength="10"  Text="0"></asp:TextBox>
								            </div>
                                            <div class="form-group col-md-4">
								                <label>Usar token existente:</label>
								               <asp:DropDownList ID="ddlToken" runat="server" DataValueField="id_parceiro_token" DataTextField="nm_link" CssClass="form-control"></asp:DropDownList>
							                </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-4">
								                <label>Parceiro:</label>
								               <asp:DropDownList ID="ddlParceiro" runat="server" DataValueField="id_parceiro" DataTextField="nm_parceiro" CssClass="form-control"></asp:DropDownList>
							                </div>
                                            <div class="form-group col-md-4">
									            <label>Nome do link:</label>
									            <asp:TextBox runat="server" ID="txtNomeLink" CssClass="form-control"></asp:TextBox>
								            </div>

                                            <div class="form-group col-md-4">
									            <label>Texto url amigavel:</label>
									            <asp:TextBox runat="server" ID="txtUrlAmigavel" CssClass="form-control"></asp:TextBox>
								            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-2">
									            <label>Vincular Produto:</label>
									            <asp:CheckBox runat="server" ID="chkVinculado" OnCheckedChanged="chkVinculado_CheckedChanged" AutoPostBack="true" />
								            </div>
                                            <div class="form-group col-md-4">
									            <label>Produto Vinculado:</label>
									            <asp:DropDownList ID="ddlProdutoVinculado" runat="server" DataValueField="id_produto_sub" DataTextField="nm_produto" CssClass="form-control" TextMode="Number" Enabled="false"></asp:DropDownList>
								            </div>
                                            <div class="form-group col-md-3">
									            <label>Valor Produto Titular:</label>
									            <asp:TextBox runat="server" ID="txtVlProdutoTitular" CssClass="form-control" Enabled="false"></asp:TextBox>
								            </div>
                                            <div class="form-group col-md-3">
									            <label>Valor Produto Vinculado:</label>
									            <asp:TextBox runat="server" ID="txtVlProdutoVinculado" CssClass="form-control" Enabled="false"></asp:TextBox>
								            </div>
                                            <div class="form-group  col-md-12 text-right">
                                                <br />
                                                <asp:Button runat="server" ID="Button1" Text="Cadastrar Condição Especial" CssClass="btn btn-success" OnClick="Button1_Click"  />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group col-md-12">
                                            <asp:GridView runat="server" ID="grdCondicoes" CssClass="table table-striped" AutoGenerateColumns="false" Font-Size="Smaller" >
                                                <Columns>
                                                    <asp:BoundField DataField="vl_produto_sub_especial" HeaderText="Valor" />
                                                    <asp:BoundField DataField="nr_trial" HeaderText="Dias Trial" />
                                                    <asp:BoundField DataField="nr_token" HeaderText="Token" />
                                                    <asp:BoundField DataField="nm_link" HeaderText="Nome Token" />
                                                    <asp:BoundField DataField="url_amigavel" HeaderText="URL Carrinho" />
                                                    <asp:BoundField DataField="nm_link_produto" HeaderText="URL Produto" />
                                                    <asp:BoundField DataField="nm_produto_vinculado" HeaderText="Produto Vinculado" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
               </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
