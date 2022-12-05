<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ExpirarLicenca.aspx.cs" Inherits="KL_Vendas.ExpirarLicenca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Expirar Licença</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Expirar Licença</h1>
			</div>
		</div><!--/.row-->
				
		
		<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">Cadastro/Pesquisa</div>
					<div class="panel-body">
						<div class="col-md-12"> 
                                <div class="form-group col-md-4">
									<label>Data Inicial:</label>
									<input class="form-control" placeholder="">
								</div>

                                <div class="form-group col-md-4">
									<label>Data Final:</label>
									<input class="form-control" placeholder="">
								</div>

                               <div class="form-group col-md-4">
									<label>E-mail Cliente:</label>
									<input class="form-control" placeholder="">
								</div>

                                <div class="form-group col-md-4">
									<label>Status:</label>
									<input class="form-control" placeholder="">
								</div>

                                <div class="form-group  col-md-12 text-right">
                                    <button class="btn btn-primary">Incluir Licença</button>
                                    <button class="btn btn-warning">Alterar Licença</button>
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
				
				<div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Lista Licenças expiradas</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                        <table class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th scope="col">Data Inicial</th>
                                                    <th scope="col">Data Final</th>
                                                    <th scope="col">Status</th>
                                                    <th scope="col">E-mail Cliente</th>
                                                </tr>
                                              </thead>
                                              <tbody>
                                                  <tr>
                                                      <td>05/12/2018</td>
                                                      <td>15/01/2019</td>
                                                      <td>---</td>
                                                      <td>cliente@gmail.com</td>
                                                  </tr>
                                                  <tr>
                                                      <td>05/12/2018</td>
                                                      <td>15/01/2019</td>
                                                      <td>---</td>
                                                      <td>cliente@gmail.com</td>
                                                  </tr>
                                                  <tr>
                                                      <td>05/12/2018</td>
                                                      <td>15/01/2019</td>
                                                      <td>---</td>
                                                      <td>cliente@gmail.com</td>
                                                  </tr>
                                                  <tr>
                                                      <td>05/12/2018</td>
                                                      <td>15/01/2019</td>
                                                      <td>---</td>
                                                      <td>cliente@gmail.com</td>
                                                  </tr>
                                              </tbody>
                                        </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

    </div>
</asp:Content>
