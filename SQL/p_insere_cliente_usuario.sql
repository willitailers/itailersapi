if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_cliente_usuario')
	drop procedure p_insere_cliente_usuario 
go

create procedure dbo.p_insere_cliente_usuario(
@id_cliente int, 
@nm_user_id varchar(500), 
--@nm_transaction_id varchar(500), 
@nm_email varchar(500), 
@dt_start datetime, 
@nm_user_document varchar(500), 
@nm_user_plan varchar(500))
as
begin
	declare @id_cliente_usuario int 

	if exists (select top 1 1 from t_cliente_usuario (nolock) where nm_user_id = @nm_user_id and id_cliente = @id_cliente)
	begin
		select -1 as 'id_cliente_usuario'
		return
	end

	insert into t_cliente_usuario(id_cliente, 
									nm_user_id, 
									nm_transaction_id,
									nm_email, 
									dt_start, 
									nm_user_document, 
									nm_user_plan,
									id_status)
	values (@id_cliente, 
			@nm_user_id, 
			convert(varchar(20), @id_cliente) + '0000' + @nm_user_id, 
			@nm_email, 
			@dt_start, 
			@nm_user_document, 
			@nm_user_plan,
			10)

	select @id_cliente_usuario = SCOPE_IDENTITY()

	select @id_cliente_usuario as 'id_cliente_usuario', convert(varchar(20), @id_cliente) + '0000' + @nm_user_id as 'nm_transaction_id'

end