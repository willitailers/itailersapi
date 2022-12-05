if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_cancelamento_cliente_licenca')
	drop procedure p_cancelamento_cliente_licenca 
go

create procedure dbo.p_cancelamento_cliente_licenca(@id_cliente_licenca int)
as
begin

	update t_cliente_licenca 
	set dt_cancelamento = getdate(), id_status = 3
	where id_cliente_licenca = @id_cliente_licenca


end