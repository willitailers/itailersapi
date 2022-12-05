if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_consulta_cliente_licenca')
	drop procedure p_consulta_cliente_licenca 
go

create procedure dbo.p_consulta_cliente_licenca(@id_cliente varchar(500), @nm_user_id varchar(500), @nm_urn varchar(100))
as
begin

	select	b.id_cliente_usuario,
			c.id_cliente_licenca,
			b.nm_transaction_id,
			nm_user_id,
			id_produto_kl,
			nm_produto_kl,
			cd_produto_kl,
			nm_urn,
			qtd_licencas,
			nm_subscriber_id,
			nm_ativacao_android,
			nm_ativacao_iphone,
			nm_ativacao_windows,
			nm_ativacao_mac,
			cd_ativacao_kl
	from	t_cliente a (nolock)
	inner join t_cliente_usuario b (nolock) on a.id_cliente = b.id_cliente
	inner join t_cliente_licenca c (nolock) on c.id_cliente_usuario = b.id_cliente_usuario
	inner join t_produto_kl d (nolock) on d.id_produto_kl = c.id_produto
	where	a.id_cliente = @id_cliente
	and b.nm_user_id = @nm_user_id
	and (d.nm_urn = @nm_urn or @nm_urn = 'all')
	and c.id_status = 2


end