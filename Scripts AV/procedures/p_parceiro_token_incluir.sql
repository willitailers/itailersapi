alter procedure dbo.p_parceiro_token_incluir(
@id_parceiro int,
@nm_link	varchar(200),
@url_amigavel	varchar(200),
@url_acesso	varchar(200),
@nr_token varchar(25))
as
begin


	insert into t_parceiro_token(id_parceiro,id_parceiro_dados_pagto, nr_token, dv_ativo, dt_criacao, id_usuario_criacao, nm_link, url_amigavel, url_acesso)
	values(@id_parceiro, 1, @nr_token, 1, getdate(), 1, @nm_link, @url_amigavel, @url_acesso )

	select @nr_token as 'retorno'

end