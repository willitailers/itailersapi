if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_lote_kl_item')
	drop procedure p_insere_lote_kl_item 
go

create procedure p_insere_lote_kl_item (@id_lote_kl	int
,@id_lote_fornecedor int
,@id_subscribe	varchar(500)
,@dt_expiracao_kl	datetime = null
,@cd_produto	varchar(50)
,@qtd_licenca	int
,@activation_type	varchar(50)
,@dt_ativacao	datetime  = null
,@dt_encerramento	datetime  = null
,@nm_linguagem	varchar(10)
,@id_plataforma	int
,@nm_link_url	varchar(1000)  = null
,@cd_ativacao_kl varchar(500)
,@nr_ordem	int)
as
begin
	if exists (select top 1 1 from t_lote_kl_item (nolock) where id_subscribe = @id_subscribe)
	begin
		select -1 as 'retorno'
		return
	end
	
	declare @id_lote_fornecedor_item int, @id_lote_kl_item int

	select @id_lote_fornecedor_item = id_lote_fornecedor_item
	from	t_lote_fornecedor_item (nolock)
	where	id_lote_fornecedor = @id_lote_fornecedor and nr_ordem = @nr_ordem


	insert into t_lote_kl_item (id_lote_kl
								,id_subscribe
								,dt_expiracao_kl
								,cd_produto
								,qtd_licenca
								,activation_type
								,dt_ativacao
								,dt_encerramento
								,nm_linguagem
								,id_plataforma
								,id_lote_fornecedor_item
								,cd_ativacao_kl
								,nm_link_url
								,dv_ativado
								,nr_ordem)
	values (@id_lote_kl, 
			@id_subscribe,
			null,
			@cd_produto,
			@qtd_licenca,
			@activation_type,
			null,
			null,
			@nm_linguagem,
			@id_plataforma,
			@id_lote_fornecedor_item,
			@cd_ativacao_kl,
			null,
			0,
			@nr_ordem)

	select @id_lote_kl_item = SCOPE_IDENTITY()

	select @id_lote_kl_item as 'retorno'
	return

end 
	