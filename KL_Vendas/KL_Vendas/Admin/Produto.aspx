<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="KL_Vendas.Produto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Produto</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Produto</h1>
			</div>
		</div><!--/.row-->
				
		
		<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">Cadastro/Pesquisa</div>
					<div class="panel-body">
						<div class="col-md-12"> 
                                <div class="form-group col-md-4">
								    <label>Fabricante:</label>
								   <asp:DropDownList ID="ddlprodutoFabricante" runat="server" DataValueField="id_fabricante" DataTextField="nm_fabricante" CssClass="form-control"></asp:DropDownList>
							    </div>

                                <div class="form-group col-md-4">
									<label>Nome Produto:</label>
									<asp:TextBox runat="server" ID="txtProdutoNome" CssClass="form-control"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Link(s) download:</label>
									<asp:TextBox runat="server" ID="txtProdutoLink" CssClass="form-control"></asp:TextBox>
								</div> 

                                <div class="form-group col-md-4">
									<label>Descrição curta:</label>
									<asp:TextBox runat="server" ID="txtProdutoDescrCurta" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Descrição completa:</label>
									<asp:TextBox runat="server" ID="txtProdutoDescrCompleta" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Descrição completa 2:</label>
									<asp:TextBox runat="server" ID="txtProdutoDescrCompleta2" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Codigo Produto</label>
									<asp:TextBox runat="server" ID="txtCdProduto" CssClass="form-control"></asp:TextBox>
								</div> 

                                <div class="form-group col-md-4">
                                    <label>Ativo:</label>
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox runat="server" ID="cbProduto"  />
                                        
                                    </div>
                                </div>

                                <div class="form-group  col-md-8 text-right">
                                    <asp:Button runat="server" ID="btnProdutoIncluir" Text="Incluir Produto" CssClass="btn btn-success" OnClick="btnProdutoIncluir_Click" />
                                    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="btn btn-default" OnClick="btnBuscar_Click" />
                                   
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
				
				<div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Lista Produtos</div>
                            <asp:GridView ID="grdProduto" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" Font-Size="12px">
                                <Columns>
                                    <asp:BoundField DataField="nm_fabricante" HeaderText="Nome Fabricante" />
                                    <asp:hyperlinkfield headertext="Produto"
                                          datatextfield="nm_produto"
                                          DataNavigateUrlFields="id_produto" 
                                          datanavigateurlformatstring="ProdutoEditar.aspx?id={0}" />
                                    <asp:BoundField DataField="nm_produto_descr_curta" HeaderText="Descrição Curta" />
                                    <asp:BoundField DataField="nm_produto_descr_completa" HeaderText="Descrição Completa" />
                                    <asp:BoundField DataField="link_downaload" HeaderText="Link download" />
                                    <asp:BoundField DataField="cd_produto" HeaderText="Codigo" />
                                    <asp:BoundField DataField="ic_ativo" HeaderText="Ativo?" />
                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:Button ID="btnExlcuir" runat="server" CommandName="Excluir" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-danger" Text="Excluir" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                </div>
           </div>
        </div>
    </div>
</asp:Content>
