
if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_consulta_codigo_fornecedor')
	drop procedure p_consulta_codigo_fornecedor 
go

create procedure dbo.p_consulta_codigo_fornecedor(@cd_validacao varchar(500))
as
begin
	select		a.cd_validacao, a.dt_limite, convert( int, a.dv_ativo) as 'dv_ativo',  b.id_subscribe, b.cd_produto, b.qtd_licenca, convert( int, b.dv_ativado) as 'dv_ativado', b.dt_ativacao, b.dt_expiracao_kl, b.id_lote_kl_item, a.id_lote_fornecedor, cd_ativacao_kl
	from		t_lote_fornecedor_item a (nolock)
	inner join	t_lote_kl_item b (nolock) on a.id_lote_fornecedor_item = b.id_lote_fornecedor_item
	where		a.cd_validacao = @cd_validacao

end