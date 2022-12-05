<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="RelatorioVendasToken.aspx.cs" Inherits="KL_Vendas.Admin.RelatorioVendasToken" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Relatorios</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Vendas por Acesso</h1>
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
					<div class="panel-heading">Pesquisa</div>
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
									<label>Status:</label>
									<asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Assinaturas ativas" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Assinaturas canceladas" Value="1" ></asp:ListItem>
                                        <asp:ListItem Text="Pagamentos recusados" Value="2" ></asp:ListItem>
                                        <asp:ListItem Text="Todas" Value="3" ></asp:ListItem>
									</asp:DropDownList>
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
                            <div class="panel-heading">Vendas</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                        <asp:GridView ID="grdPagamentos" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" Font-Size="12px">
                                            <Columns>
                                                <asp:BoundField DataField="nm_link" HeaderText="Campanha" />
                                                <asp:BoundField DataField="url_amigavel" HeaderText="URL" />
                                                <asp:BoundField DataField="nr_compras" HeaderText="Numero de compras" />
                                                <asp:BoundField DataField="vl_compra" HeaderText="Valor Compra" />
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
