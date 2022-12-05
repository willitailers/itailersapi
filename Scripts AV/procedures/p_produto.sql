alter procedure dbo.p_produto(@id_produto int, @id_fabricante int, @nm_produto varchar(200))
as
begin
	select id_produto,
	a.id_fabricante,
	nm_produto,
	nm_produto_descr_curta,
	nm_produto_descr_completa,
	nm_produto_descr_completa2,
	link_downaload,
	convert(int, a.dv_ativo) as 'dv_ativo',
	case when a.dv_ativo = 1 then 'S' else 'N' end as 'ic_ativo',
	a.dt_criacao,
	a.id_usuario_criacao,
	a.dt_alteracao,
	a.id_usuario_alteracao,
	b.nm_fabricante
	,a.cd_produto
	from t_produto a
	inner join t_fabricante b on a.id_fabricante = b.id_fabricante
	where (id_produto = @id_produto or @id_produto = 0)
			and (a.id_fabricante = @id_fabricante or @id_fabricante = 0)
			and (nm_produto like '%' + ISNULL(@nm_produto,'') + '%')
end

