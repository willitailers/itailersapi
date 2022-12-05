alter procedure dbo.p_compra_selecionar(@id_compra bigint, @id_carrinho bigint)
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
					,a.cd_ativacao
					,a.link_ativacao
					,b.id_compra_dados_pagamento
					,b.id_tp_pagamento
					,b.nr_cartao
					,b.nm_cartao
					,b.dt_cartao
					,b.id_banco
					,b.cc
					,b.cc_dg
					,b.ag
					,b.ag_dg
					,b.cvv
		from		t_compra a
		inner join	t_compra_dados_pagamento b on a.id_compra = b.id_compra
		where		(@id_compra > 0 and a.id_compra = @id_compra)
					or (@id_carrinho > 0 and a.id_carrinho = @id_carrinho)

end