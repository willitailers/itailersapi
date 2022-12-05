<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="FabricanteEditar.aspx.cs" Inherits="KL_Vendas.Admin.FabricanteEditar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="lblIdFabricante" Visible="false"></asp:Label>
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Fabricante</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Fabricante Editar</h1>
			</div>
		</div><!--/.row-->
				
		
		<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">Cadastro/Pesquisa</div>
					<div class="panel-body">
						<div class="col-md-12"> 

                                <div class="form-group col-md-4">
									<label>Nome Fabricante:</label>
									<asp:TextBox runat="server" ID="txtNomeFabricante" CssClass="form-control" Enabled="true" ></asp:TextBox>
								</div>

                               <div class="form-group col-md-4">
									<label>CNPJ:</label>
									<asp:TextBox runat="server" ID="txtCNPJFabricante" CssClass="form-control" Enabled="true" ></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Endereço:</label>
									<asp:TextBox runat="server" ID="txtEnderecoFabricante" CssClass="form-control" Enabled="true" ></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>CEP:</label>
									<asp:TextBox runat="server" ID="txtCEPFabricante" CssClass="form-control" Enabled="true" ></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Cidade:</label>
									<asp:TextBox runat="server" ID="txtCidadeFabricante" CssClass="form-control" Enabled="true" ></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
									<label>Estado:</label>
									<asp:TextBox runat="server" ID="txtEstadoFabricante" CssClass="form-control" Enabled="true" ></asp:TextBox>
								</div>

                                <div class="form-group col-md-4">
                                    <label>Ativo:</label>
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox runat="server" ID="cbAtivoFabricante" Enabled="true"  />
                                    </div>
                                </div>

                                <div class="form-group  col-md-8 text-right">
                                    <asp:Button runat="server" ID="btnFabricanteIncluir" Text="Alterar" CssClass="btn btn-primary" OnClick="btnFabricanteIncluir_Click" Enabled="true"  />
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
              </div>
        </div>
    </div>
</asp:Content>
