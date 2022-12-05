alter procedure dbo.p_pedido_detalhe(@id_carrinho bigint, @id_compra bigint, @id_inscricao varchar(200))
as
begin
	
	select		a.id_compra
					,a.id_carrinho
					,a.dt_compra
					,a.vl_compra
					,a.vl_desconto
					,a.id_sub_produto
					,a.id_produto
					,a.dt_aprovacao_compra
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
					,e.link_downaload
					,d.qtd_licencas
					,ISNULL(convert(int,a.dv_envio_email),0) as 'dv_envio_email'
					,ISNULL(c.nr_trial,0)  as 'nr_trial'
					,ISNULL(h.id_produto_sub_especial,0) as 'id_produto_sub_especial'
					,convert(int, ISNULL(h.ic_produto_vinculado,0))  as 'ic_produto_vinculado'
					,i.nm_produto_sub as 'nm_produto_sub_vinculado'
		from		t_compra a
		inner join	t_carrinho b on a.id_carrinho = b.id_carrinho
		inner join	t_carrinho_item c on c.id_carrinho = b.id_carrinho
		inner join	t_produto_sub d on d.id_produto_sub = c.id_produto_sub
		inner join	t_produto e on e.id_produto = d.id_produto
		inner join	t_cliente f on f.id_cliente = b.id_cliente 
		inner join	t_fabricante g on g.id_fabricante = e.id_fabricante
		left join	t_produto_sub_especial h on h.id_produto_sub_especial = c.id_produto_sub_especial
		left join	t_produto_sub i on i.id_produto_sub = h.id_produto_sub_vinculado
		where		(@id_compra > 0 and a.id_compra = @id_compra)
					or (@id_carrinho > 0 and a.id_carrinho = @id_carrinho)
					or (@id_inscricao <> '' and a.id_inscricao = @id_inscricao)

end
