<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Links.aspx.cs" Inherits="KL_Vendas.Links" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Gerar Links</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Gerar Links</h1>
			</div>
		</div><!--/.row-->
				
		
		<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">Cadastro/Pesquisa</div>
					<div class="panel-body">
						<div class="col-md-12"> 
                                <div class="form-group col-md-3">
								    <label>Parceiro:</label>
								   <asp:DropDownList ID="ddlParceiro" runat="server" DataValueField="id_parceiro" DataTextField="nm_parceiro" CssClass="form-control"></asp:DropDownList>
							    </div>

                                <div class="form-group col-md-3">
									<label>Nome do link:</label>
									<asp:TextBox runat="server" ID="txtNomeLink" CssClass="form-control"></asp:TextBox>
								</div>

                                <div class="form-group col-md-3">
									<label>texto url amigavel:</label>
									<asp:TextBox runat="server" ID="txtUrlAmigavel" CssClass="form-control"></asp:TextBox>
								</div>

                                <div class="form-group col-md-3">
									<label>url basica:</label>
									<asp:DropDownList ID="ddlUrl" runat="server" CssClass="form-control"></asp:DropDownList>
								</div>

                                <div class="form-group  col-md-12 text-right">
                                    <asp:Button runat="server" ID="btnProdutoIncluir" Text="Incluir URL" CssClass="btn btn-dark" OnClick="btnProdutoIncluir_Click" />
                                   
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
				
				<div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Lista Links</div>
                            <asp:GridView ID="grdProduto" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" Font-Size="12px">
                                <Columns>
                                    <asp:BoundField DataField="nm_parceiro" HeaderText="Parceiro" />
                                    <asp:BoundField DataField="nm_link" HeaderText="Nome Link" />
                                    <asp:BoundField DataField="url_amigavel" HeaderText="URL amigavel" />
                                    <asp:BoundField DataField="url_acesso" HeaderText="URL original" />
                                    <asp:BoundField DataField="nr_token" HeaderText="Cd. Token" />
                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                </div>
           </div>
        </div>
    </div>
</asp:Content>
