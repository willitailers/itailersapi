<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="NovoPagamento.aspx.cs" Inherits="KL_Vendas.NovoPagamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Pagamentos</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Pagamentos</h1>
			</div>
		</div><!--/.row-->
       <div class="alert bg-danger" role="alert" runat="server" id="divErro" visible="false">
           <em class="fa fa-lg fa-warning">&nbsp;</em> Erro na consulta <a href="#" class="pull-right"><em class="fa fa-lg fa-close"></em></a>
       </div>	
		
		<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">Cadastro/Pesquisa</div>
					<div class="panel-body">
						<div class="col-md-12"> 
                                <div class="form-group col-md-4">
									<label>Data Inicial:</label>
									<asp:TextBox runat="server" ID="txtDt_Inicial" CssClass="form-control" placeholder="dd/mm/aaaa"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Data Final:</label>
									<asp:TextBox runat="server" ID="txtDt_Final" CssClass="form-control" placeholder="dd/mm/aaaa"></asp:TextBox>
								</div>

                               <div class="form-group col-md-4">
									<label>E-mail Cliente:</label>
									<asp:TextBox runat="server" ID="txtEmail_cliente" CssClass="form-control"></asp:TextBox>
								</div>

                                <div class="form-group  col-md-12 text-right">
                                    <asp:Button runat="server" ID="btnConsulta" Text="Consultar" CssClass="btn btn-primary" OnClick="btnConsulta_Click" />
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
				
				<div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Lista Pagamentos</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                        <asp:GridView ID="grdPagamentos" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" Font-Size="12px">
                                            <Columns>
                                                <asp:BoundField DataField="dt_compra" HeaderText="Data" />
                                                <asp:BoundField DataField="produto" HeaderText="Produto" />
                                                <asp:BoundField DataField="nm_email" HeaderText="E-mail Cliente" />
                                                <asp:BoundField DataField="nm_cliente" HeaderText="Nome Cliente" />
                                                <asp:BoundField DataField="subscribe_id" HeaderText="Transação PagarMe" />
                                                <asp:BoundField DataField="dt_aprovacao_compra" HeaderText="Dt. Aprovação Compra" />
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
