alter procedure dbo.p_produto_sub_especial_token(@id_parceiro_token int)
as
begin
	select	distinct a.id_parceiro_token, a.nr_token, a.nm_link
	from	t_parceiro_token a
	inner join t_produto_sub_especial b on a.id_parceiro_token = b.id_parceiro_token
	where a.id_parceiro_token = @id_parceiro_token or @id_parceiro_token = 0
end