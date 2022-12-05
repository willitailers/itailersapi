create procedure dbo.p_assinatura_cancelar(@id_compra bigint, @id_usuario int)
as
begin

	
		update	t_compra
		set		ic_cancelado = 1, id_usuario_cancelamento = @id_usuario, dt_cancelamento = GETDATE()
		where	id_compra = @id_compra

end