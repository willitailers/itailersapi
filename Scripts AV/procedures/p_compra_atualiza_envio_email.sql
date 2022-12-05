create procedure dbo.p_compra_atualiza_envio_email(@id_compra int)
as
begin

	update t_compra set dv_envio_email = 1 where id_compra = @id_compra

end