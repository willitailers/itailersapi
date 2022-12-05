alter procedure dbo.p_produto_sub(@id_produto_sub int, @id_produto int)
as
begin
	select id_produto_sub,
	a.id_produto,
	nm_produto_sub,
	nm_produto_sub_descr_curta,
	nm_produto_sub_descr_completa,
	nm_produto_sub_descr_completa2,
	vl_produto_sub,
	a.id_tp_produto,
	nm_tp_produto,
	convert(int, a.dv_ativo) as 'dv_ativo',
	case when convert(int, a.dv_ativo) = 1 then 'Sim' else 'Não' end as 'cd_ativo',
	a.dt_criacao,
	a.id_usuario_criacao,
	a.dt_alteracao,
	a.id_usuario_alteracao,
	nm_produto,
	ISNULL(vl_produto_sub_desc,0) as 'vl_produto_sub_desc',
	convert(int, dv_desconto) as 'dv_desconto',
	case when convert(int, a.dv_desconto) = 1 then 'Sim' else 'Não' end as 'cd_desconto',
	ISNULL(vl_desconto,0) as 'vl_desconto',
	a.cd_produto_sub_publico
	from t_produto_sub a
	inner join t_produto b on a.id_produto = b.id_produto
	inner join t_tp_produto c on c.id_tp_produto = a.id_tp_produto
	where (id_produto_sub = @id_produto_sub or @id_produto_sub = 0)
		and (a.id_produto = @id_produto or @id_produto = 0)
	order by a.id_produto, a.id_produto_sub
end

