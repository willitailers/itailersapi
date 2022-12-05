alter procedure dbo.p_compra_atualiza_compra(@id_compra bigint, @ic_recusado int, @id_usuario int)
as
begin

	if (@ic_recusado = 0)
		update	t_compra
		set		dt_aprovacao_compra = getdate(),id_usuario_aprovacao = @id_usuario
		where	id_compra = @id_compra
	else
		update	t_compra
		set		ic_recusado = 1, id_usuario_recusa = @id_usuario, dt_recusa = GETDATE()
		where	id_compra = @id_compra

end