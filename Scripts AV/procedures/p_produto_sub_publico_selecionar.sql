alter procedure dbo.p_produto_sub_publico_selecionar(@cd_produto_sub_publico varchar(20), @nr_token varchar(25) = '')
as
begin
	if exists (select 1 from t_parceiro_token a inner join t_produto_sub_especial b on a.id_parceiro_token = b.id_parceiro_token where a.nr_token = @nr_token)
		select		a.id_produto_sub
					,a.id_produto
					,a.nm_produto_sub
					,a.nm_produto_sub_descr_curta
					,a.nm_produto_sub_descr_completa
					,a.nm_produto_sub_descr_completa2
					,c.vl_produto_sub_especial as 'vl_produto_sub'
					,b.nm_produto
					,b.cd_produto
					,b.nm_caminho_img
					,a.id_tp_produto
					,ISNULL(c.nr_trial, 0) as 'nr_trial'
					,convert(int, ISNULL(c.ic_produto_vinculado ,0)) as 'ic_produto_vinculado'
					,f.nm_produto as 'nm_produto_sub_vinculado'
					,c.id_produto_sub_especial
					,case when ISNULL(e.id_tp_produto,0) = 6 or a.id_tp_produto = 6 then 1 else 0 end as 'dv_wifi'
		from		t_produto_sub a
		inner join	t_produto b on a.id_produto = b.id_produto 
		inner join	t_produto_sub_especial c on c.id_produto_sub = a.id_produto_sub
		inner join	t_parceiro_token d on d.id_parceiro_token = c.id_parceiro_token
		left join	t_produto_sub e on e.id_produto_sub = c.id_produto_sub_vinculado
		left join	t_produto f on f.id_produto = e.id_produto
		where		a.cd_produto_sub_publico = @cd_produto_sub_publico and d.nr_token = @nr_token
	else
		select		a.id_produto_sub
					,a.id_produto
					,a.nm_produto_sub
					,a.nm_produto_sub_descr_curta
					,a.nm_produto_sub_descr_completa
					,a.nm_produto_sub_descr_completa2
					,a.vl_produto_sub
					,b.nm_produto
					,b.cd_produto
					,b.nm_caminho_img
					,a.id_tp_produto
					,0 as 'nr_trial'
					,0 as 'ic_produto_vinculado'
					,'' as 'nm_produto_sub_vinculado'
					,0 as 'id_produto_sub_especial'
					,case when a.id_tp_produto = 6 then 1 else 0 end as 'dv_wifi'
		from		t_produto_sub a
		inner join	t_produto b on a.id_produto = b.id_produto 
		where		a.cd_produto_sub_publico = @cd_produto_sub_publico

end