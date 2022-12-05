alter procedure dbo.p_produto_incluir(
@id_produto int,
@id_fabricante int,
@nm_produto varchar(200),
@nm_produto_descr_curta varchar(250),
@nm_produto_descr_completa varchar(1000),
@nm_produto_descr_completa2 varchar(1000),
@link_downaload varchar(300),
@dv_ativo bit,
@dt_criacao datetime,
@id_usuario_criacao int,
@dt_alteracao datetime,
@id_usuario_alteracao int,
@cd_produto varchar(200))
as
begin
	if (@id_produto = 0)
		insert into t_produto(id_fabricante, nm_produto, nm_produto_descr_curta, nm_produto_descr_completa, nm_produto_descr_completa2, link_downaload, dv_ativo, dt_criacao, id_usuario_criacao, dt_alteracao, id_usuario_alteracao, cd_produto)
		values( @id_fabricante, @nm_produto, @nm_produto_descr_curta, @nm_produto_descr_completa, @nm_produto_descr_completa2, @link_downaload, @dv_ativo, @dt_criacao, @id_usuario_criacao, @dt_alteracao, @id_usuario_alteracao, @cd_produto)
	else
		update t_produto
		set 
		id_fabricante = @id_fabricante, 
		nm_produto = @nm_produto, 
		nm_produto_descr_curta = @nm_produto_descr_curta, 
		nm_produto_descr_completa = @nm_produto_descr_completa, 
		nm_produto_descr_completa2 = @nm_produto_descr_completa2, 
		link_downaload = @link_downaload, 
		dv_ativo = @dv_ativo, 
		dt_alteracao = @dt_alteracao, 
		id_usuario_alteracao = @id_usuario_alteracao, 
		cd_produto = @cd_produto
		where id_produto = @id_produto 
end

