create procedure dbo.p_parceiro_selecionar ( @id_parceiro int = NULL ) 
 as begin 
	 select  
		id_parceiro
		,nm_parceiro
		,nm_identificacao_externa
		,cnpj
		,endereco
		,cep
		,cidade
		,estado
		,dv_ativo
		,dt_criacao
		,id_usuario_criacao
		,dt_alteracao
		,id_usuario_alteracao
	 from t_parceiro
	 where id_parceiro = ISNULL(@id_parceiro, id_parceiro)
 end
 go 
create procedure dbo.p_parceiro_atualizar ( 
@id_parceiro int = NULL 
,@nm_parceiro varchar(200) = NULL 
,@nm_identificacao_externa varchar(100) = NULL 
,@cnpj varchar(30) = NULL 
,@endereco varchar(200) = NULL 
,@cep varchar(10) = NULL 
,@cidade varchar(100) = NULL 
,@estado varchar(2) = NULL 
,@dv_ativo bit = NULL 
,@dt_criacao datetime = NULL 
,@id_usuario_criacao int = NULL 
,@dt_alteracao datetime = NULL 
,@id_usuario_alteracao int = NULL 
 ) 
 as begin 
	update t_parceiro 
	set 
		nm_parceiro = ISNULL(@nm_parceiro, nm_parceiro)
		,nm_identificacao_externa = ISNULL(@nm_identificacao_externa, nm_identificacao_externa)
		,cnpj = ISNULL(@cnpj, cnpj)
		,endereco = ISNULL(@endereco, endereco)
		,cep = ISNULL(@cep, cep)
		,cidade = ISNULL(@cidade, cidade)
		,estado = ISNULL(@estado, estado)
		,dv_ativo = ISNULL(@dv_ativo, dv_ativo)
		,dt_criacao = ISNULL(@dt_criacao, dt_criacao)
		,id_usuario_criacao = ISNULL(@id_usuario_criacao, id_usuario_criacao)
		,dt_alteracao = ISNULL(@dt_alteracao, dt_alteracao)
		,id_usuario_alteracao = ISNULL(@id_usuario_alteracao, id_usuario_alteracao)
	 where id_parceiro = ISNULL(@id_parceiro, id_parceiro)
 end; 
 go 
create procedure dbo.p_parceiro_incluir ( 
@nm_parceiro varchar(200) = NULL 
,@nm_identificacao_externa varchar(100) = NULL 
,@cnpj varchar(30) = NULL 
,@endereco varchar(200) = NULL 
,@cep varchar(10) = NULL 
,@cidade varchar(100) = NULL 
,@estado varchar(2) = NULL 
,@dv_ativo bit = NULL 
,@dt_criacao datetime = NULL 
,@id_usuario_criacao int = NULL 
,@dt_alteracao datetime = NULL 
,@id_usuario_alteracao int = NULL 
 ) 
 as begin 
	 insert into t_parceiro ( 
		nm_parceiro
		,nm_identificacao_externa
		,cnpj
		,endereco
		,cep
		,cidade
		,estado
		,dv_ativo
		,dt_criacao
		,id_usuario_criacao
		,dt_alteracao
		,id_usuario_alteracao
	 ) 
	 select 
		@nm_parceiro
		,@nm_identificacao_externa
		,@cnpj
		,@endereco
		,@cep
		,@cidade
		,@estado
		,@dv_ativo
		,@dt_criacao
		,@id_usuario_criacao
		,@dt_alteracao
		,@id_usuario_alteracao
 end; 
 go
  
create procedure dbo.p_parceiro_excluir ( @id_parceiro int = NULL ) 
as 
begin 
	 delete  from t_parceiro
	 where id_parceiro = @id_parceiro
 end; 
 go