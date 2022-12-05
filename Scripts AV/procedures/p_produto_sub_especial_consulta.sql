alter procedure dbo.p_produto_sub_especial_consulta(@id_produto_sub int, @id_parceiro_token int)
as
begin
	select		a.id_produto_sub
				,a.id_produto
				,a.nm_produto_sub
				,a.nm_produto_sub_descr_curta
				,a.nm_produto_sub_descr_completa
				,a.nm_produto_sub_descr_completa2
				,a.vl_produto_sub
				,b.nm_produto
				,b.cd_produto
				,b.nm_caminho_img
				,a.id_tp_produto
				,d.nm_link
				,c.vl_produto_sub_especial
				,d.nr_token
				,d.url_amigavel
				,c.nr_trial
				, case b.id_produto when 3 then 'https://www.antiviruslinktel.com.br/antivirus?nr_token=' + d.nr_token 
									when 4 then 'https://www.antiviruslinktel.com.br/internet-security?nr_token=' + d.nr_token  
									when 5 then 'https://www.antiviruslinktel.com.br/protecao-total?nr_token=' + d.nr_token  
									when 6 then 'https://www.antiviruslinktel.com.br/antivirus-android?nr_token=' + d.nr_token 
									when 10 then 'https://www.antiviruslinktel.com.br/protecao-para-criancas?nr_token=' + d.nr_token 
									when 7 then 'https://www.antiviruslinktel.com.br/empresas?nr_token=' + d.nr_token 
									else '' end as 'nm_link_produto'
				, case when e.id_produto_sub is null then '-' else e.nm_produto_sub + ' (' + convert(varchar(10), c.vl_produto_sub_titular) + ' - ' + convert(varchar(10), c.vl_produto_sub_vinculado) + ')' end as 'nm_produto_vinculado'
	from		t_produto_sub a
	inner join	t_produto b on a.id_produto = b.id_produto 
	inner join	t_produto_sub_especial c on c.id_produto_sub = a.id_produto_sub
	inner join	t_parceiro_token d on d.id_parceiro_token = c.id_parceiro_token
	left join	t_produto_sub e on e.id_produto_sub = c.id_produto_sub_vinculado
	where		(a.id_produto_sub = @id_produto_sub or @id_produto_sub = 0)
				and (c.id_parceiro_token = @id_parceiro_token or @id_parceiro_token = 0 )
end