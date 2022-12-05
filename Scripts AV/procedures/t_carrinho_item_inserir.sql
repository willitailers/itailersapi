alter procedure dbo.t_carrinho_item_inserir (@id_carrinho bigint, @id_produto_sub int, @vl_produto decimal(18,2), @qtd_produto int, @nr_trial int = 0, @id_produto_sub_especial int = 0, @dv_wifi int = 0)
as
begin
	declare @id_carrinho_item bigint
	
	insert into t_carrinho_item(id_carrinho, id_produto_sub, vl_produto, qtd_produto, nr_trial, id_produto_sub_especial, dv_wifi)
	select @id_carrinho, @id_produto_sub, @vl_produto , @qtd_produto, @nr_trial, @id_produto_sub_especial, @dv_wifi 

	select @id_carrinho_item = SCOPE_IDENTITY()

	select ISNULL(@id_carrinho_item,0) as 'retorno'
end



