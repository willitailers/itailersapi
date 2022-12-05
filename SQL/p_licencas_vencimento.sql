if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_licencas_vencimento')
	drop procedure p_licencas_vencimento 
go
create procedure dbo.p_licencas_vencimento
as
begin
	insert into t_log (nm_log, dt_log, id_tipo_log)
	select		'Cancelamento automático - Subscribe: ' + convert(varchar(100),a.id_subscribe), getdate(), 13
	from		t_lote_kl_item a (nolock)
	inner join	t_lote_fornecedor_item b (nolock) on a.id_lote_fornecedor_item = b.id_lote_fornecedor_item
	where		dt_encerramento <= getdate() and id_status = 2

	select		a.id_lote_kl_item, a.id_subscribe, b.cd_validacao
	from		t_lote_kl_item a (nolock)
	inner join	t_lote_fornecedor_item b (nolock) on a.id_lote_fornecedor_item = b.id_lote_fornecedor_item
	where		dt_encerramento <= getdate()  and id_status = 2
end

