create procedure dbo.p_seleciona_produto_sub_todos
as
begin
	select		b.id_produto_sub ,a.nm_produto + ' - ' + b.nm_produto_sub as 'nm_produto'
	from		t_produto a
	inner join	t_produto_sub b on a.id_produto = b.id_produto
	where		b.dv_ativo = 1
	order by	a.id_produto, b.id_produto_sub


end