alter procedure dbo.p_produto_sub_especial_inserir(@id_produto_sub int, 
@vl_produto_sub_especial decimal(18,2), 
@nr_trial int, 
@id_parceiro int,
@nm_link	varchar(200),
@url_amigavel	varchar(200),
@url_acesso	varchar(200),
@nr_token varchar(25),
@id_parceiro_token int,
@ic_produto_vinculado int,
@id_produto_sub_vinculado int ,
@vl_produto_sub_titular decimal(18,2),
@vl_produto_sub_vinculado decimal(18,2)
)
as
begin
	if (@id_parceiro_token = 0)
	begin
		insert into t_parceiro_token(id_parceiro,id_parceiro_dados_pagto, nr_token, dv_ativo, dt_criacao, id_usuario_criacao, nm_link, url_amigavel, url_acesso)
		values(@id_parceiro, 1, @nr_token, 1, getdate(), 1, @nm_link, @url_amigavel, @url_acesso ) 

		select @id_parceiro_token = SCOPE_IDENTITY()
	end

	insert into t_produto_sub_especial (id_produto_sub
										,vl_produto_sub_especial
										,id_parceiro_token
										,nr_trial
										,ic_produto_vinculado
										,id_produto_sub_vinculado
										,vl_produto_sub_titular
										,vl_produto_sub_vinculado)
	values (@id_produto_sub, @vl_produto_sub_especial, @id_parceiro_token, @nr_trial, @ic_produto_vinculado, @id_produto_sub_vinculado, @vl_produto_sub_titular, @vl_produto_sub_vinculado)

	select @id_produto_sub as 'retorno'
end