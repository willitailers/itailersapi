if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_cliente_produto_kl')
	drop procedure p_insere_cliente_produto_kl 
go

create procedure dbo.p_insere_cliente_produto_kl(@id_cliente int, @id_produto_kl int)
as
begin

	insert into t_cliente_produto_kl (id_cliente, id_produto_kl)
	values (@id_cliente, @id_produto_kl)

end