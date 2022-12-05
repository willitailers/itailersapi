if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_consulta_cliente_produto')
	drop procedure p_consulta_cliente_produto 
go

create procedure dbo.p_consulta_cliente_produto(@id_cliente int, @id_cliente_certificado int)
as
begin

	select	d.cd_produto_kl,
			d.id_produto_kl,
			d.nm_produto_kl,
			d.nm_urn,
			d.qtd_licencas
	from	t_cliente a (nolock)
	inner join t_cliente_produto_certificado b (nolock) on a.id_cliente = b.id_cliente
	inner join t_produto_kl d (nolock) on d.id_produto_kl = b.id_produto_kl
	where	a.id_cliente = @id_cliente
	and		b.id_cliente_certificado = @id_cliente_certificado
end