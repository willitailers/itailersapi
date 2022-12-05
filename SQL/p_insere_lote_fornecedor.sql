if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_lote_fornecedor')
	drop procedure p_insere_lote_fornecedor 
go

create procedure dbo.p_insere_lote_fornecedor(@nr_item_lote_fornecedor int, @nm_arquivo varchar(300))
as
begin
	if exists (select 1 from t_lote_fornecedor (nolock) where nm_arquivo = @nm_arquivo)
	begin
		select -1 as 'retorno'
		return
	end
	declare @id_lote_fornecedor	int 

	insert into t_lote_fornecedor (dt_lote_fornecedor, nr_item_lote_fornecedor, nm_arquivo)
	values (getdate(), @nr_item_lote_fornecedor, @nm_arquivo)

	select @id_lote_fornecedor = SCOPE_IDENTITY()

	select @id_lote_fornecedor as 'retorno'
	return

end