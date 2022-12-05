<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BusinessProduct.ascx.cs" Inherits="KL_Vendas.controls.BusinessProduct" %>

<div class="col-sm-4 product-item">
    <div class="product-item-box">
        <div class="product-item-box_desc">
            <div class="product-img">
                <img src="src/product-icon-KSOS.png" alt="Proteção especial para PCS">
            </div>
            <div class="product-desc">
                <span>Proteção essencial para PCS</span>
                <h4 class="text-green"><%= this.PrimeiroNomeProduto %></h4>
                <h2><%= this.RestanteNomeProduto %></h2>
                <p>
                    <span><i class="fa-file-pdf fa-w-12 fa-2x"></i><a href="https://media.kaspersky.com/br/business-security/KSOS6_Datasheet_BR.pdf"
                        target="_blank">Folha de dados</a> (2 páginas)</span>
                </p>
            </div>
            <div class="col-md-12" id="action">
                <div class="product-item-box-business_item">
                    <div class="desc-desconto" <% if (this.ValorDesconto == 0) { %> style="visibility: hidden" <% } %>>
                        <%= this.ValorDesconto.ToString("P0") %> DE DESCONTO
                    </div>
                    <div class="product-price">
                        <form>
                            <div class="form-group">
                                <label for="" class="price-actual"><%= (this.ValorProduto - (this.ValorProduto * this.ValorDesconto)).ToString("C2")  %></label>
                                <label for="" class="price-old" <% if (this.ValorDesconto == 0) { %> style="visibility: hidden" <% } %>>
                                    <%= this.ValorProduto.ToString("C2") %></label>
                            </div>
                        </form>
                    </div>
                    <div class="license_text">
                        <p style="font-weight: bolder;">SUA LICENÇA COBRE:</p>
                        <asp:Repeater ItemType="System.string" runat="server" ID="repeaterLicencas">
                            <ItemTemplate>
                                <p><%# Item %> </p>
                            </ItemTemplate>
                        </asp:Repeater>
                        <p style="color: red;">Protegido por 1 ano</p>
                    </div>
                    <a type="button" class="btn button-comprar-red" sd-product="<%= this.CdSubProduto %>" id="button<%= this.SubProdutoId %>">Comprar</a>
                </div>
            </div>
        </div>
    </div>
</div>
