<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="SubProduto.aspx.cs" Inherits="KL_Vendas.SubProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
				<h1 class="page-header">Sub Produto</h1>
			</div>
		</div><!--/.row-->
				
		
		<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">Cadastro/Pesquisa</div>
					<div class="panel-body">
						<div class="col-md-12"> 
                                <div class="form-group col-md-4">
								    <label>Produto:</label>
								    <asp:DropDownList ID="ddlProduto" runat="server" DataValueField="id_produto" DataTextField="nm_produto" CssClass="form-control"></asp:DropDownList>
							    </div>

                                <div class="form-group col-md-4">
									<label>Nome Sub_Produto:</label>
									<asp:TextBox runat="server" ID="txtProdutoSub" CssClass="form-control"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Valor Sub-Produto</label>
                                    <asp:TextBox runat="server" ID="txtValorSUb" CssClass="form-control"></asp:TextBox>
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
									<asp:TextBox runat="server" ID="txtProdutoSUbDescrCurta" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Sub_Descrição completa:</label>
									<asp:TextBox runat="server" ID="txtProdutoSubDescrCompleta" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Sub_Descrição completa 2:</label>
									<asp:TextBox runat="server" ID="txtProdutoSubDescrCompleta2" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
								</div>
                                
                                <div class="form-group col-md-4">
								    <label>Tipo Produto:</label>
								    <asp:DropDownList ID="ddlTipoProduto" runat="server" DataValueField="id_tp_produto" DataTextField="nm_tp_produto" CssClass="form-control"></asp:DropDownList>
							    </div>

                                <div class="form-group col-md-4">
                                    <label>Ativo:</label>
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox runat="server" ID="cbProdutoSub"  />
                                    </div>
                                </div>

                                <div class="form-group  col-md-8 text-right">
                                    <asp:Button runat="server" ID="btnProdutoSubIncluir" Text="Incluir Produto Sub" CssClass="btn btn-success" OnClick="btnProdutoSubIncluir_Click" />
                                    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="btn btn-default" OnClick="btnBuscar_Click" />
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
				
				<div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Lista Sub Produtos</div>
                            <div class="panel-body">
                                <asp:GridView ID="grdProdutoSub" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" Font-Size="Small">
                                    <Columns>
                                        <asp:hyperlinkfield headertext="Sub Produto"
                                          datatextfield="nm_produto_sub"
                                          DataNavigateUrlFields="id_produto_sub" 
                                          datanavigateurlformatstring="SubProdutoEditar.aspx?id={0}" />
                                        <asp:BoundField DataField="nm_produto" HeaderText="Produto" />
                                        <asp:BoundField DataField="nm_produto_sub_descr_curta" HeaderText="Descricao Curta" />
                                        <asp:BoundField DataField="nm_tp_produto" HeaderText="Tipo produto" />
                                        <asp:BoundField DataField="vl_produto_sub" HeaderText="Valor Sub Produto" />
                                        <asp:BoundField DataField="vl_produto_sub_desc" HeaderText="Valor Promoção" />
                                        <asp:BoundField DataField="vl_desconto" HeaderText="Valor Desconto" />
                                        <asp:BoundField DataField="cd_desconto" HeaderText="Desconto" />                                        
                                        <asp:BoundField DataField="cd_ativo" HeaderText="Ativo" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
</div>
</div>
    </div>
</asp:Content>
