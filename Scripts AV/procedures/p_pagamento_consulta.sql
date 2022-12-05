alter procedure dbo.p_pagamento_consulta(@nm_email varchar(100), @id_tp_consulta int, @dt_incial datetime, @dt_final datetime)
as
begin
	/*
	@id_tp_consulta
	1 - pendentes
	2 - aprovados
	3 - recusado
	*/
	select		a.id_compra
					,a.id_carrinho
					,convert(varchar(10),a.dt_compra, 103) + ' ' + convert(varchar(5),a.dt_compra, 108) as 'dt_compra'
					,a.vl_compra
					,a.vl_desconto
					,convert(varchar(10),a.dt_aprovacao_compra, 103) + ' ' + convert(varchar(5),a.dt_aprovacao_compra, 108) as 'dt_aprovacao_compra'
					,a.ic_recusado
					,a.ic_cancelado
					,a.ic_estorno
					,a.dt_estorno
					,a.id_usuario_estorno
					,a.nm_motivo_estorno
					,a.id_inscricao
					,ISNULL(a.cd_ativacao, '-') as 'cd_ativacao'
					,a.link_ativacao
					,b.dt_compra
					,c.qtd_produto
					,e.nm_produto
					,e.nm_produto + ' ' + d.nm_produto_sub as 'produto'
					,e.nm_produto_descr_curta
					,e.nm_produto_descr_completa
					,d.nm_produto_sub
					,d.nm_produto_sub_descr_curta
					,d.nm_produto_sub_descr_completa
					,d.nm_produto_sub_descr_completa2
					,d.vl_produto_sub
					,e.nm_caminho_img
					,ISNULL(e.cd_produto, '-') as 'cd_produto'
					,f.id_cliente
					,c.vl_produto as 'vl_produto_carrinho'
					,f.nr_cpf
					,f.nm_email
					,ISNULL(a.dv_enviado, 0) as 'dv_enviado'
					,f.nm_cliente
					,f.nm_cliente_sobrenome
					,f.nm_cliente + ' ' + f.nm_cliente_sobrenome as 'nome'
					,f.endereco
					,f.endereco_nr
					,f.cep
					,f.cidade
					,f.id_tp_pessoa
					,f.nm_razao_social
					,f.nr_telefone
					,a.subscribe_id
					,RIGHT(g.nr_cartao, 4) as 'nr_cartao'
					,e.link_downaload
					,d.qtd_licencas
					,convert(int, ISNULL(c.dv_wifi,0)) as 'dv_wifi'
					,convert(int, ISNULL(a.dv_envio_wifi,0)) as 'dv_envio_wifi'
					,v_sexo
					,f.dt_nascimento
		from		t_compra a
		inner join	t_carrinho b on a.id_carrinho = b.id_carrinho
		inner join	t_carrinho_item c on c.id_carrinho = b.id_carrinho
		inner join	t_produto_sub d on d.id_produto_sub = c.id_produto_sub
		inner join	t_produto e on e.id_produto = d.id_produto
		inner join	t_cliente f on f.id_cliente = b.id_cliente 
		left join	t_compra_dados_pagamento g on g.id_compra = a.id_compra
		where		f.nm_email = ISNULL(@nm_email, f.nm_email)
		and			((@id_tp_consulta = 1 and  dv_enviado = 1 and ISNULL(ic_recusado,0) = 0 and dt_aprovacao_compra is null) or 
					(@id_tp_consulta = 2 and  dv_enviado = 1 and dt_aprovacao_compra is not null and ISNULL(ic_cancelado,0) = 0 ) or 
					(@id_tp_consulta = 3 and  ic_recusado = 1) )
		and			a.dt_compra between @dt_incial and @dt_final
		order by	a.dt_compra  desc


end


