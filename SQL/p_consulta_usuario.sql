if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_consulta_usuario')
	drop procedure p_consulta_usuario 
go

create procedure dbo.p_consulta_usuario(@id_cliente int, @nm_user_id varchar(500))
as
begin

	select	id_cliente_usuario
			,id_cliente
			,nm_user_id
			,nm_transaction_id
	from	t_cliente_usuario b (nolock)
	where	b.id_cliente = @id_cliente and nm_user_id = @nm_user_id

end