if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_delete_cliente_usuario')
	drop procedure p_delete_cliente_usuario 
go

create procedure dbo.p_delete_cliente_usuario(@id_cliente_usuario int, @delete bit = 0)
as
begin

	if (@delete = 0)
		update t_cliente_usuario 
		set  id_status = 11
		where id_cliente_usuario = @id_cliente_usuario
	else
		delete from t_cliente_usuario where id_cliente_usuario = @id_cliente_usuario

end