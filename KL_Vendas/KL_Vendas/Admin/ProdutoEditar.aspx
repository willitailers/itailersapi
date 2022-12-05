<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ProdutoEditar.aspx.cs" Inherits="KL_Vendas.Admin.ProdutoEditar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="lblIdProduto" Visible="false"></asp:Label>
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
				<h1 class="page-header">Produto Editar</h1>
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
								   <asp:DropDownList ID="ddlprodutoFabricante" runat="server" DataValueField="id_fabricante" DataTextField="nm_fabricante" CssClass="form-control" Enabled="false"></asp:DropDownList>
							    </div>

                                <div class="form-group col-md-4">
									<label>Nome Produto:</label>
									<asp:TextBox runat="server" ID="txtProdutoNome" CssClass="form-control" Enabled="false"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Link(s) download:</label>
									<asp:TextBox runat="server" ID="txtProdutoLink" CssClass="form-control" Enabled="false"></asp:TextBox>
								</div> 

                                <div class="form-group col-md-4">
									<label>Descrição curta:</label>
									<asp:TextBox runat="server" ID="txtProdutoDescrCurta" CssClass="form-control" Enabled="false" Rows="3" TextMode="MultiLine"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Descrição completa:</label>
									<asp:TextBox runat="server" ID="txtProdutoDescrCompleta" CssClass="form-control" Enabled="false" Rows="3" TextMode="MultiLine"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Descrição completa 2:</label>
									<asp:TextBox runat="server" ID="txtProdutoDescrCompleta2" CssClass="form-control" Enabled="false" Rows="3" TextMode="MultiLine"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Codigo Produto</label>
									<asp:TextBox runat="server" ID="txtCdProduto" CssClass="form-control" Enabled="false"></asp:TextBox>
								</div> 

                                <div class="form-group col-md-4">
                                    <label>Ativo:</label>
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox runat="server" ID="cbProduto" Enabled="false" />
                                        
                                    </div>
                                </div>

                                <div class="form-group  col-md-8 text-right">
                                    <asp:Button runat="server" ID="btnProdutoIncluir" Text="Alterar Produto" CssClass="btn btn-dark" OnClick="btnProdutoIncluir_Click" Enabled="false" />
                                   
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
           </div>
        </div>
    </div>
</asp:Content>
