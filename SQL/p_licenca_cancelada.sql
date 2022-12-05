if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_licenca_cancelada')
	drop procedure p_licenca_cancelada 
go
create procedure dbo.p_licenca_cancelada(@id_lote_kl_item int)
as
begin

	update		a
	set			dt_cancelamento = getdate(), id_status = 3
	from		t_lote_kl_item a (nolock)
	where		id_lote_kl_item = @id_lote_kl_item

	insert into t_log (nm_log, dt_log, id_tipo_log)
	select		'Cancelado - Subscribe: ' + convert(varchar(100),a.id_subscribe), getdate(), 13
	from		t_lote_kl_item a (nolock)
	where		id_lote_kl_item = @id_lote_kl_item
end


