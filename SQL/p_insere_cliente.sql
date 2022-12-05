if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_cliente')
	drop procedure p_insere_cliente 
go

create procedure dbo.p_insere_cliente(@nm_cliente varchar(500))
as
begin

	insert into t_cliente(nm_cliente)
	values (@nm_cliente)

end