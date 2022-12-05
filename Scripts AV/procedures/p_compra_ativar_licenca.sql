alter procedure dbo.p_compra_ativar_licenca(@id_compra bigint, @cd_ativacao varchar(200), @link_ativacao varchar(500))
as
begin

	update t_compra
	set dv_enviado = 1, cd_ativacao = @cd_ativacao, link_ativacao = @link_ativacao
	where id_compra = @id_compra

end

