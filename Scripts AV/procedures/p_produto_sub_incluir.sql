alter procedure dbo.p_produto_sub_incluir(
@id_produto_sub int,
@id_produto int,
@nm_produto_sub varchar(200),
@nm_produto_sub_descr_curta varchar(250),
@nm_produto_sub_descr_completa varchar(1000),
@nm_produto_sub_descr_completa2 varchar(1000),
@vl_produto_sub numeric(18,2),
@id_tp_produto int,
@dv_ativo bit,
@dt_criacao datetime,
@id_usuario_criacao int,
@dt_alteracao datetime,
@id_usuario_alteracao int,
@vl_produto_sub_desc	numeric(18,2),
@dv_desconto	bit,
@vl_desconto	numeric(18,2)
)
as
begin
	if (@id_produto_sub = 0)
		insert into t_produto_sub(	id_produto, 
									nm_produto_sub, 
									nm_produto_sub_descr_curta, 
									nm_produto_sub_descr_completa, 
									nm_produto_sub_descr_completa2, 
									vl_produto_sub, 
									id_tp_produto, 
									dv_ativo, 
									dt_criacao, 
									id_usuario_criacao, 
									dt_alteracao, 
									id_usuario_alteracao, 
									cd_produto_sub_publico,
									vl_desconto,
									vl_produto_sub_desc,
									dv_desconto)
		values( @id_produto, 
				@nm_produto_sub, 
				@nm_produto_sub_descr_curta, 
				@nm_produto_sub_descr_completa, 
				@nm_produto_sub_descr_completa2, 
				@vl_produto_sub, 
				@id_tp_produto, 
				@dv_ativo, 
				@dt_criacao, 
				@id_usuario_criacao, 
				@dt_alteracao, 
				@id_usuario_alteracao, 
				convert(varchar(20),  left(newid(), 20)),
				@vl_desconto,
				@vl_produto_sub_desc,
				@dv_desconto)
	else
		update	t_produto_sub
		set		id_produto = @id_produto, 
				nm_produto_sub = @nm_produto_sub, 
				nm_produto_sub_descr_curta = @nm_produto_sub_descr_curta, 
				nm_produto_sub_descr_completa = @nm_produto_sub_descr_completa, 
				nm_produto_sub_descr_completa2 = @nm_produto_sub_descr_completa2, 
				vl_produto_sub = @vl_produto_sub, 
				id_tp_produto = @id_tp_produto, 
				dv_ativo = @dv_ativo, 
				dt_alteracao = @dt_alteracao, 
				id_usuario_alteracao = @id_usuario_alteracao,
				vl_desconto = @vl_desconto,
				vl_produto_sub_desc = @vl_produto_sub_desc,
				dv_desconto = @dv_desconto
		where	id_produto_sub = @id_produto_sub

end

