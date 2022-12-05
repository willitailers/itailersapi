if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_lote_kl')
	drop procedure p_insere_lote_kl 
go

create procedure dbo.p_insere_lote_kl(@nr_item_lote_kl int, @nm_arquivo varchar(300), @id_lote_fornecedor int)
as
begin
	if exists (select 1 from t_lote_kl (nolock) where nm_arquivo = @nm_arquivo)
	begin
		select -1 as 'retorno'
		return
	end
	declare @id_lote_kl	int 

	insert into t_lote_kl (dt_lote_kl, nr_item_lote_kl, nm_arquivo, id_lote_fornecedor)
	values (getdate(), @nr_item_lote_kl, @nm_arquivo, @id_lote_fornecedor)

	select @id_lote_kl = SCOPE_IDENTITY()

	select @id_lote_kl as 'retorno'
	return

end