<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="InformacaoFabricante.aspx.cs" Inherits="KL_Vendas.InformacaoFabricante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="Home.aspx">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Informação Fabricante</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Informação Fabricante</h1>
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
								    <select class="form-control">
									    <option>A</option>
									    <option>B</option>
									    <option>C</option>
									    <option>D</option>
								    </select>
							    </div>

                                <div class="form-group col-md-4">
									<label>Titulo Info:</label>
									<input class="form-control" placeholder="">
								</div>

                               <div class="form-group col-md-4">
									<label>Info:</label>
									<input class="form-control" placeholder="">
								</div>

                                <div class="form-group  col-md-12 text-right">
                                    <button class="btn btn-primary">Incluir Informação</button>
                                    <button class="btn btn-warning">Alterar Informação</button>
                                </div>
                            
						</div>
					</div>
				</div><!-- /.panel-->
				
				
				<div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">Lista Informações</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                        <table class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th scope="col">Nome Fabricante</th>
                                                    <th scope="col">Titulo info</th>
                                                    <th scope="col">Info</th>
                                                </tr>
                                              </thead>
                                              <tbody>
                                                  <tr>
                                                      <td>A</td>
                                                      <td>...A</td>
                                                      <td>informação A</td>
                                                  </tr>
                                                  <tr>
                                                      <td>A</td>
                                                      <td>...A</td>
                                                      <td>informação A</td>
                                                  </tr>
                                                  <tr>
                                                      <td>A</td>
                                                      <td>...A</td>
                                                      <td>informação A</td>
                                                  </tr>
                                                  <tr>
                                                      <td>A</td>
                                                      <td>...A</td>
                                                      <td>informação A</td>
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
