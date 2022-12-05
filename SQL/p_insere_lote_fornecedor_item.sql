if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_lote_fornecedor_item')
	drop procedure p_insere_lote_fornecedor_item 
go

create procedure p_insere_lote_fornecedor_item (@id_lote_fornecedor int, @cd_validacao varchar(500), @nr_ordem int)
as
begin
	if exists (select top 1 1 from t_lote_fornecedor_item (nolock) where cd_validacao = @cd_validacao)
	begin
		select -1 as 'retorno'
		return
	end
	
	declare @id_lote_fornecedor_item int
	insert into t_lote_fornecedor_item (id_lote_fornecedor ,cd_validacao ,dt_limite ,dv_ativo, nr_ordem)
	values (@id_lote_fornecedor, @cd_validacao, convert(date, dateadd(day, 180, getdate())), 1, @nr_ordem)

	select @id_lote_fornecedor_item = SCOPE_IDENTITY()

	select @id_lote_fornecedor_item as 'retorno'
	return

end 
	