alter procedure dbo.p_relatorio_venda_token(@dt_inicial datetime, @dt_final datetime, @cd_status int)
as
begin
	select		COUNT(*) as 'nr_compras', c.nm_link, c.nr_token, c.url_amigavel, vl_compra
	from		t_compra a
	inner join	t_carrinho b on a.id_carrinho = b.id_carrinho
	inner join	t_parceiro_token c on c.id_parceiro_token = b.id_parceiro_token
	where		a.dt_compra between @dt_inicial and @dt_final
	and			a.dt_aprovacao_compra is not null
	and			(	(@cd_status = 0 and ISNULL(a.ic_cancelado,0) = 0) or
					(@cd_status = 1 and ISNULL(a.ic_cancelado,0) = 1) or
					(@cd_status = 2 and ISNULL(a.ic_recusado,0) = 1) or
					(@cd_status = 3 ) )
	group by	 c.nm_link, c.nr_token, c.url_amigavel, vl_compra
end