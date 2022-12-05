if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_deletar_lote')
	drop procedure p_deletar_lote 
go

create procedure dbo.p_deletar_lote(@id_lote_fornecedor int)
as
begin

	delete		b
	from		t_lote_kl a (nolock)
	inner join	t_lote_kl_item b (nolock) on a.id_lote_kl = b.id_lote_kl
	where		a.id_lote_fornecedor = @id_lote_fornecedor 

	delete		a
	from		t_lote_kl a (nolock)
	where		a.id_lote_fornecedor = @id_lote_fornecedor 

	delete		b
	from		t_lote_fornecedor a (nolock)
	inner join	t_lote_fornecedor_item b (nolock) on a.id_lote_fornecedor = b.id_lote_fornecedor
	where		a.id_lote_fornecedor = @id_lote_fornecedor 

	delete		a
	from		t_lote_fornecedor a (nolock)
	where		a.id_lote_fornecedor = @id_lote_fornecedor 

end