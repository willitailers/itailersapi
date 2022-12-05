alter procedure dbo.p_security_produtos(@id_tipo_produto int, @id_produto int = 0)
as
begin

	select	p.id_produto
			,p.id_fabricante
			,p.nm_produto
			,p.nm_produto_descr_curta
			,p.nm_produto_descr_completa
			,p.nm_produto_descr_completa2
			,p.link_downaload
			,p.dv_ativo
			,p.dt_criacao
			,p.id_usuario_criacao
			,p.dt_alteracao
			,p.id_usuario_alteracao
			,p.nm_caminho_img
			,p.cd_produto
			,p.link_pagina
	from	t_produto p with(nolock)
	join	t_produto_sub s with(nolock) on p.id_produto = s.id_produto 
	where	s.id_tp_produto = @id_tipo_produto
			and (p.id_produto = @id_produto or @id_produto = 0)
			and p.dv_ativo = 1
	group by 
			p.id_produto
			,p.id_fabricante
			,p.nm_produto
			,p.nm_produto_descr_curta
			,p.nm_produto_descr_completa
			,p.nm_produto_descr_completa2
			,p.link_downaload
			,p.dv_ativo
			,p.dt_criacao
			,p.id_usuario_criacao
			,p.dt_alteracao
			,p.id_usuario_alteracao
			,p.nm_caminho_img
			,p.cd_produto
			,p.link_pagina

end

