if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_retorna_certificado')
	drop procedure p_retorna_certificado 
go

create procedure dbo.p_retorna_certificado(@nm_token varchar(500))
as
begin

	select a.id_cliente, id_cliente_certificado, nm_thumbprint, nm_usuario_certificado, nm_senha_certificado
	from t_cliente a (nolock) 
	inner join t_cliente_token c (nolock) on a.id_cliente = c.id_cliente
	inner join t_cliente_certificado b (nolock) on b.id_cliente_token = c.id_cliente_token	
	where c.nm_token = @nm_token

end