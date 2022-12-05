alter procedure dbo.p_licenca_consulta(@nm_email varchar(100), @dt_incial datetime, @dt_final datetime, @id_produto int, @id_produto_sub int, @ic_recusado bit)
as
begin
	

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
					,f.endereco
					,f.endereco_nr
					,f.cep
					,f.cidade
					,f.id_tp_pessoa
					,f.nm_razao_social
					,f.nr_telefone
					,a.subscribe_id
					,e.link_downaload
		from		t_compra a
		inner join	t_carrinho b on a.id_carrinho = b.id_carrinho
		inner join	t_carrinho_item c on c.id_carrinho = b.id_carrinho
		inner join	t_produto_sub d on d.id_produto_sub = c.id_produto_sub
		inner join	t_produto e on e.id_produto = d.id_produto
		inner join	t_cliente f on f.id_cliente = b.id_cliente 
		where		f.nm_email = ISNULL(@nm_email, f.nm_email)
		and			a.dt_compra between @dt_incial and @dt_final
		and			(e.id_produto = @id_produto or @id_produto = 0 )
		and			(d.id_produto_sub = @id_produto_sub or @id_produto_sub = 0 )
		and			ISNULL(a.ic_cancelado, 0) = @ic_recusado 
		and			ISNULL(a.ic_estorno, 0) = 0  
		and			a.cd_ativacao is not null  
		order by	a.dt_compra


end
