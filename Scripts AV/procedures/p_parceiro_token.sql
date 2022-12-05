alter  procedure dbo.p_parceiro_token(@id_parceiro_token int, @id_parceiro int)
as
	begin
		select	 a.id_parceiro_token,
				a.id_parceiro,
				a.id_parceiro_dados_pagto,
				a.nr_token,
				a.dv_ativo,
				a.dt_criacao,
				a.id_usuario_criacao,
				a.dt_alteracao,
				a.id_usuario_alteracao
				,a.nm_link
				,a.url_amigavel
				,a.url_acesso
				,b.nm_parceiro
	from t_parceiro_token a
	inner join t_parceiro b on a.id_parceiro = b.id_parceiro
	where (id_parceiro_token = @id_parceiro_token or @id_parceiro_token = 0)
			and (a.id_parceiro = @id_parceiro or @id_parceiro = 0)
end

