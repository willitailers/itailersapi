<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="MelhorProduto.aspx.cs" Inherits="KL_Vendas.Admin.MelhorProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Melhor Produto pra mim</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Avaliação</h1>
			</div>
		</div><!--/.row-->
		<div class="alert bg-warning" role="alert" id="DivErro" runat="server"><em class="fa fa-lg fa-warning">&nbsp;</em> <asp:Label runat="server" ID="lblErro"></asp:Label> </div>	
		
		<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">Cadastro/Pesquisa</div>
					<div class="panel-body">
						<div class="col-md-12"> 
                                <div class="form-group col-md-2">
								    <label>Pagina:</label>
								    <asp:DropDownList ID="ddlPagina" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="KAV" Value="KAV"></asp:ListItem>
                                        <asp:ListItem Text="KIS" Value="KIS"></asp:ListItem>
                                        <asp:ListItem Text="KTS" Value="KTS"></asp:ListItem>
                                        <asp:ListItem Text="KISA" Value="KISA"></asp:ListItem>
                                        <asp:ListItem Text="KSK" Value="KSK"></asp:ListItem>
								    </asp:DropDownList>
							    </div>

                                <div class="form-group col-md-5">
									<label>Titulo:</label>
									<asp:TextBox runat="server" ID="txtTitulo" CssClass="form-control" MaxLength="150"></asp:TextBox>
								</div>

                                <div class="form-group col-md-5">
									<label>Texto:</label>
									<asp:TextBox runat="server" ID="txtAvaliacao" CssClass="form-control" MaxLength="200" ></asp:TextBox>
								</div>

                                <div class="form-group  col-md-12 text-right">
                                    <asp:Button runat="server" ID="btnProdutoSubIncluir" Text="Incluir" CssClass="btn btn-success" OnClick="btnProdutoSubIncluir_Click" />
                                    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="btn btn-default" OnClick="btnBuscar_Click" />
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
				
				<div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Melhor Produto</div>
                            <div class="panel-body">
                                <asp:GridView ID="grdAvaliacao" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" Font-Size="Small" OnRowCommand="grdAvaliacao_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="id_melhor_produto" HeaderText="#" />
                                        <asp:BoundField DataField="nm_pagina" HeaderText="Pagina" />
                                        <asp:BoundField DataField="nm_titulo_melhor_produto" HeaderText="Titulo" />
                                        <asp:BoundField DataField="nm_melhor_produto" HeaderText="Texto" />
                                        <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:Button ID="btnExlcuir" runat="server" CommandName="Excluir" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-danger" Text="Excluir" />
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
    </div>
</asp:Content>
