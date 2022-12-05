create procedure dbo.p_compra_id_subscricao_seleciona(@id_compra bigint)
as
begin
	select id_inscricao 
	from t_compra where id_compra = @id_compra

end