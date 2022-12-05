create procedure dbo.p_compra_confirma_wifi(@id_compra int)
as
begin
	update t_compra 
	set dv_envio_wifi = 1
	where id_compra = @id_compra
end