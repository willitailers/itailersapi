
if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_consulta_token')
	drop procedure p_consulta_token 
go

create procedure dbo.p_consulta_token
as
begin
	select id_token, cd_token, id_fornecedor, convert(int,dv_ativo) as 'dv_ativo'
	from	t_token (nolock)

end