if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_cliente_token')
	drop procedure p_insere_cliente_token 
go

create procedure dbo.p_insere_cliente_token(@nm_token varchar(500), @dt_validade_token datetime, @id_status int)
as
begin

	insert into t_cliente_token (nm_token, dt_validade_token, id_status)
	values (@nm_token, @dt_validade_token, @id_status)

end