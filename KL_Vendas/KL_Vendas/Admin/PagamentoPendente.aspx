<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PagamentoPendente.aspx.cs" Inherits="KL_Vendas.PagamentoPendente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Pagamento Pendente</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Pagamento Pendente</h1>
			</div>
		</div><!--/.row-->
		<div class="alert bg-danger" role="alert" runat="server" id="divErro" visible="false">
           <em class="fa fa-lg fa-warning">&nbsp;</em> <asp:Label runat="server" ID="lblErro"></asp:Label>
       </div>			
		<div class="alert bg-success" role="alert" runat="server" id="div1" visible="false">
           <em class="fa fa-lg fa-warning">&nbsp;</em> <asp:Label runat="server" ID="lblSucesso"></asp:Label>
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
                            <div class="panel-heading">Lista Pagamentos Pendentes</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                        <asp:GridView ID="grdPagamentos" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" Font-Size="12px" OnRowCommand="grdPagamentos_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="id_compra" HeaderText="#" />
                                                <asp:BoundField DataField="dt_compra" HeaderText="Data" />
                                                <asp:BoundField DataField="produto" HeaderText="Produto" />
                                                <asp:BoundField DataField="nm_email" HeaderText="E-mail Cliente" />
                                                <asp:BoundField DataField="nm_cliente" HeaderText="Nome Cliente" />
                                                <asp:BoundField DataField="subscribe_id" HeaderText="Transação PagarMe" />
                                                <asp:TemplateField HeaderText="Recusar" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnRecusa" runat="server" CommandName="Recusa" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-danger" Text="Recusar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aprovar" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAprova" runat="server" CommandName="Aprova" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-success" Text="Aprovar" />
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
