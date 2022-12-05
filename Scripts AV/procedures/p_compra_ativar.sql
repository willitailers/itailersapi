alter procedure dbo.p_compra_ativar(@id_compra bigint, @card_hash varchar(200), @plan_id varchar(200), @subscribe_id varchar(200))
as
begin

	update t_compra
	set dv_enviado = 1, card_hash = @card_hash, plan_id = @plan_id, subscribe_id = @subscribe_id
	where id_compra = @id_compra

end

