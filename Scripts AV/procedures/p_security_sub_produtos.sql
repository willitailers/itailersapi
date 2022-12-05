alter procedure dbo.p_security_sub_produtos(@id_produto int, @id_produto_sub int = 0, @nr_token varchar(25) = 'zz')
as
begin

	if exists (select 1 from t_produto_sub_especial a inner join t_parceiro_token b on a.id_parceiro_token = b.id_parceiro_token where b.nr_token = @nr_token)
	begin
		declare @id_token_paceiro int = (select top 1 id_parceiro_token from t_parceiro_token where nr_token = @nr_token)

		select  s.id_produto_sub
				,s.id_produto
				,s.nm_produto_sub
				,s.nm_produto_sub_descr_curta
				,s.nm_produto_sub_descr_completa
				,s.nm_produto_sub_descr_completa2
				,ISNULL(ss.vl_produto_sub_especial ,s.vl_produto_sub) as 'vl_produto_sub'
				,s.id_tp_produto
				,s.dv_ativo
				,s.dt_criacao
				,s.id_usuario_criacao
				,s.dt_alteracao
				,s.id_usuario_alteracao
				,s.cd_produto_sub_publico
				,case when ISNULL(ss.vl_produto_sub_especial,s.vl_produto_sub) < s.vl_produto_sub then s.vl_produto_sub  else ISNULL(s.vl_produto_sub_desc, 0.00)  end as vl_produto_sub_desc
				,case when ISNULL(ss.vl_produto_sub_especial,s.vl_produto_sub) < s.vl_produto_sub then 1 else convert(int, ISNULL(s.dv_desconto, 0)) end as dv_desconto
				,case when ISNULL(ss.vl_produto_sub_especial,s.vl_produto_sub) < s.vl_produto_sub then 100 - convert(int,ISNULL(ss.vl_produto_sub_especial,0) * 100 /  s.vl_produto_sub) else convert(int, ISNULL(s.vl_desconto, 0)) end  as vl_desconto
				,ISNULL(ss.nr_trial, 0)  as nr_trial
		from	t_produto_sub s with(nolock)
		left join	t_produto_sub_especial ss with(nolock) on s.id_produto_sub = ss.id_produto_sub and ss.id_parceiro_token = @id_token_paceiro
		where	(s.id_produto = @id_produto or @id_produto = 0)
				and (s.id_produto_sub = @id_produto_sub or @id_produto_sub = 0)
				and s.dv_ativo = 1
	end
	else
	begin
		select  s.id_produto_sub
				,s.id_produto
				,s.nm_produto_sub
				,s.nm_produto_sub_descr_curta
				,s.nm_produto_sub_descr_completa
				,s.nm_produto_sub_descr_completa2
				,s.vl_produto_sub
				,s.id_tp_produto
				,s.dv_ativo
				,s.dt_criacao
				,s.id_usuario_criacao
				,s.dt_alteracao
				,s.id_usuario_alteracao
				,s.cd_produto_sub_publico
				,isnull(s.vl_produto_sub_desc, 0.00) as vl_produto_sub_desc
				,convert(int, ISNULL(s.dv_desconto, 0)) as dv_desconto
				,convert(int, ISNULL(s.vl_desconto, 0)) as vl_desconto
				,0  as nr_trial
		from	t_produto_sub s with(nolock)
		where	(s.id_produto = @id_produto or @id_produto = 0)
				and (s.id_produto_sub = @id_produto_sub or @id_produto_sub = 0)
				and s.dv_ativo = 1
	end

end

