<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ContatoFabricante.aspx.cs" Inherits="KL_Vendas.ContatoFabricante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Contato Fabricante</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Contato Fabricante</h1>
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
								    <asp:DropDownList ID="ddlfabricanteContato" runat="server" DataValueField="id_fabricante" DataTextField="nm_fabricante" CssClass="form-control"></asp:DropDownList>
							    </div>

                                <div class="form-group col-md-4">
									<label>Nome Contato:</label>
									<asp:TextBox runat="server" ID="txtFabricanteContatoNome" CssClass="form-control"></asp:TextBox>
								</div>

                               <div class="form-group col-md-4">
									<label>Telefone Contato:</label>
									<asp:TextBox runat="server" ID="txtFabricanteContatoTelefone" CssClass="form-control"></asp:TextBox>
								</div>

                               <div class="form-group col-md-4">
									<label>Ramal Contato:</label>
									<asp:TextBox runat="server" ID="txtFabricanteContatoRamal" CssClass="form-control"></asp:TextBox>
								</div>

                                <div class="form-group col-md-8">
									<label>Observação:</label>
									<asp:TextBox runat="server" ID="txtFabricanteContatoObs" CssClass="form-control"></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
                                    <label>dv_Principal:</label>
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox runat="server" ID="cbFabricanteContato"  />
                                    </div>
                                </div>

                                <div class="form-group  col-md-8 text-right">
                                    <asp:Button runat="server" ID="btnfabricanteContatoIncluir" Text="Incluir Fabricante Contato" CssClass="btn btn-primary" OnClick="btnfabricanteContatoIncluir_Click"  />
                                    <asp:Button runat="server" ID="btnfabricanteContatoAlterar" Text="Alterar fabricante Contato" CssClass="btn btn-warning" Enabled="false" Visible="false" />
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
				
				<div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Lista Contatos</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                        <asp:GridView ID="grdFabricandoContato" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="nm_fabricante" HeaderText="Nome Fabricante" />
                                                <asp:BoundField DataField="nm_contato" HeaderText="Contato" />
                                                <asp:BoundField DataField="nm_telefone" HeaderText="Telefone" />
                                                <asp:BoundField DataField="nm_ramal" HeaderText="Ramal" />
                                                <asp:BoundField DataField="nm_observacao" HeaderText="Observação" />
                                                <asp:BoundField DataField="dv_principal" HeaderText="Principal" />
                                            </Columns>

                                        </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


    </div>
</asp:Content>
