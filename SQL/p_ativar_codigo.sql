if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_ativar_codigo')
	drop procedure p_ativar_codigo 
go

create procedure dbo.p_ativar_codigo(@id_lote_kl_item int, @dt_expiracao_kl datetime, @dt_encerramento datetime, @nm_link_url varchar(1000))
as
begin
	if not exists (select 1 from t_lote_kl_item where id_lote_kl_item = @id_lote_kl_item)
	begin
		select -1 as 'retorno'
		return
	end

	update	t_lote_kl_item
	set		dt_expiracao_kl = @dt_expiracao_kl, dt_ativacao = GETDATE(), dt_encerramento = @dt_encerramento, nm_link_url = @nm_link_url, dv_ativado = 1, id_status  = 2
	where	id_lote_kl_item = @id_lote_kl_item

	select 0 as 'retorno'

end