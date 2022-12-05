alter procedure dbo.p_carrinho_selecionar ( @cd_carrinho varchar(200), @id_carrinho bigint)
as
begin
	select	a.id_carrinho, a.id_cliente, a.cd_carrinho, a.nm_token,
			b.id_carrinho_item, b.qtd_produto, b.vl_produto,
			c.id_produto_sub, c.nm_produto_sub, c.nm_produto_sub_descr_curta,
			d.id_produto, d.nm_produto, d.nm_caminho_img,
			ISNULL(b.nr_trial,0) as 'nr_trial' 
	from t_carrinho a
	inner join t_carrinho_item b on a.id_carrinho = b.id_carrinho
	inner join t_produto_sub c on c.id_produto_sub = b.id_produto_sub
	inner join t_produto d on d.id_produto = c.id_produto
	where (a.cd_carrinho = ISNULL(@cd_carrinho, a.cd_carrinho))
		and (a.id_carrinho = @id_carrinho or @id_carrinho = 0)

end
