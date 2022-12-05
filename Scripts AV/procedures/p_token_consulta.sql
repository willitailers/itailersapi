alter procedure dbo.p_token_consulta(@nr_token varchar(25))
as
begin

	select		a.nm_parceiro, a.nm_identificacao_externa, b.nr_token, e.id_produto, e.id_fabricante, e.nm_produto, e.nm_produto_descr_curta, e.nm_produto_descr_completa
				,f.nm_produto_sub, f.nm_produto_sub_descr_curta, f.nm_produto_sub_descr_completa, e.nm_caminho_img, f.vl_produto_sub, f.id_produto_sub
	from		t_parceiro a
	inner join	t_parceiro_token b on a.id_parceiro = b.id_parceiro
	inner join	t_parceiro_token_produto c on c.id_parceiro_token = b.id_parceiro_token
	inner join	t_parceiro_token_produto_sub d on d.id_parceiro_token_produto = c.id_parceiro_token_produto
	inner join	t_produto e on e.id_produto = c.id_produto
	inner join	t_produto_sub f on f.id_produto = e.id_produto and d.id_produto_sub = f.id_produto_sub
	where		b.nr_token = @nr_token

end

