if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_cliente_licenca')
	drop procedure p_insere_cliente_licenca 
go

create procedure dbo.p_insere_cliente_licenca(
@id_cliente_licenca int, 
@id_cliente_usuario int, 
@id_produto int, 
@cd_ativacao_kl varchar(500), 
@nm_subscriber_id varchar(500),
@nm_ativacao_android varchar(500), 
@nm_ativacao_iphone varchar(500), 
@nm_ativacao_windows varchar(500),
@nm_ativacao_mac varchar(500),
@dt_ativacao datetime, 
@dt_expiracao datetime,
@id_status int)
as
begin

	if (@id_cliente_licenca = 0)
	begin	
		insert into t_cliente_licenca (id_cliente_usuario, id_produto, cd_ativacao_kl, nm_subscriber_id, nm_ativacao_android, nm_ativacao_iphone, nm_ativacao_windows, nm_ativacao_mac, dt_ativacao, dt_expiracao, id_status)
		values (@id_cliente_usuario, @id_produto, @cd_ativacao_kl, @nm_subscriber_id, @nm_ativacao_android, @nm_ativacao_iphone, @nm_ativacao_windows, @nm_ativacao_mac, @dt_ativacao, @dt_expiracao, @id_status)

		select @id_cliente_licenca = SCOPE_IDENTITY()

	end
	else
	begin
		update	t_cliente_licenca
		set		nm_ativacao_android = ISNULL(@nm_ativacao_android,nm_ativacao_android) ,
				nm_ativacao_iphone =  ISNULL(@nm_ativacao_iphone, nm_ativacao_iphone),
				nm_ativacao_windows =  ISNULL(@nm_ativacao_windows, nm_ativacao_windows),
				nm_ativacao_mac =  ISNULL(@nm_ativacao_mac, nm_ativacao_mac),
				dt_expiracao = ISNULL(@dt_expiracao, dt_expiracao),
				id_status = ISNULL(@id_status, id_status)
		where	id_cliente_licenca = @id_cliente_licenca
	end


	select @id_cliente_licenca as 'id_cliente_licenca'

end