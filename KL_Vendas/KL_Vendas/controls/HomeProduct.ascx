<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeProduct.ascx.cs" Inherits="KL_Vendas.controls.HomeProduct" %>

<div class="card mb-4 shadow-sm no-border">
<div class="card-header p-3 bg-blue">
    <h4 class="my-0 font-weight-normal"></h4>
</div>
<div class="card-body">
    <div class="desc" id="DivDesconto<%= this.ProdutoId %>">
        <div class="porcent"><span class="highlight" id="VlDesconto<%= this.ProdutoId %>"></span> de desconto</div>
        <div class="recommended">Linktel recomenda</div>
    </div>
    <div class="product">
        <div class="product-image">
            <img src="<%= this.CaminhoImagem %>"  />
        </div>
        <div class="about-product">
            <p>
            <%= this.DescricaoProduto %> 
            </p>
        </div>
        <div class="separator"></div>
        <div class="price-product">
            <div class="price">
            <p class="btn btn-product m-0" id="price<%= this.ProdutoId %>">R$ 0,00/mês</p>
            </div>
            <div class="price-old" id="DivPrecoAntigo_<%= this.ProdutoId %>">
            <p class=" m-0" id="price_old<%= this.ProdutoId %>"><s>R$ 0,00/mês</s></p>
            <p class=" m-0">Economize!</p>
            </div>
        </div>
        <div class="separator"></div>
        <div class="select-plan">

            <div class="radio-group" style="min-height: 140px;">

            <asp:Repeater ID="repeaterChecks" runat="server">
                <ItemTemplate>
                    <div class="form-element radio <%# (ValidarDesconto(Eval("vl_desconto_str"))) %>" style="<%# (ValidarEval(Eval("nm_produto_sub"))) %>">
                        <input class="form-check-input" type="radio" name="radio-price<%# Eval("id_produto") %>" id='check<%# Eval("id_produto_sub") %>' vl-produto="<%# Eval("vl_produto_sub")  %>" value="<%# Eval("vl_produto_sub")  %>"
                            id-product="<%# Eval("id_produto") %>" sub-produto="<%# Eval("cd_produto_sub_publico") %>" vl-desconto="<%# Eval("vl_desconto")  %>" vl-desconto-str="<%# Eval("vl_desconto_str")  %>" 
                            dc-desc="<%# Eval("vl_produto_sub_desc")  %>" dc-desc-str="<%# Eval("vl_produto_sub_desc_str")  %>" <%# Eval("checado")  %> >
                        <label>
                        <span class="choose">
                            <span><%# Eval("nm_produto_sub") %></span>
                        </span>
                        <span class="price">
                            <span class="price-new">R$<%# Eval("vl_produto_sub") %>/mês</span>
                        </span>
                        </label>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>

        </div>
        <div class="form-buttons text-center p-3">
            <input type="button" class="btn btn-product" value="CONHEÇA" onClick="window.location.href='<%= this.link_pagina %>?nr_token=<%= this.Token %>'" >
            <!--a type="button" class="btn btn-product" id-product="<%= this.ProdutoId %>" id="button<%= this.ProdutoId %>">CONHEÇA</a-->
                        
        </div>
    </div>
</div>
</div>
