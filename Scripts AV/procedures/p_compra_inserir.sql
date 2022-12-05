alter procedure dbo.p_compra_inserir (@id_carrinho bigint, @vl_compra decimal(18,2), @vl_desconto decimal(18,2), @id_sub_produto int,
							@id_produto int, @nr_cpf varchar(20) = NULL)
as
begin
	declare @id_compra bigint, @dt_compra datetime, @id_compra_ int , @novo int = 1, @id_carrinho_ int = 0, @id_inscricao varchar(200) = ''
	delete from t_compra where id_carrinho = @id_carrinho

	if exists (select 1 from t_cliente a 
		inner join t_carrinho b on a.id_cliente = b.id_cliente
		inner join t_compra c on c.id_carrinho = b.id_carrinho
		where	a.nr_cpf = @nr_cpf and c.dv_enviado = 1)
	begin
		select	top 1 @dt_compra = c.dt_compra , @id_compra_ = c.id_compra, @id_carrinho_ = b.id_carrinho, @id_inscricao = c.id_inscricao
		from	t_cliente a 
		inner join t_carrinho b on a.id_cliente = b.id_cliente
		inner join t_compra c on c.id_carrinho = b.id_carrinho
		where	a.nr_cpf = @nr_cpf and c.dv_enviado = 1
		order by	b.id_carrinho desc

		if (@dt_compra >= dateadd(minute, -1, getdate()))
		begin
			select @id_compra = @id_compra_, @novo = 0, @id_carrinho = @id_carrinho_
		end
		else
		begin
			update	a
			set		nr_cpf = ISNULL(@nr_cpf, nr_cpf)
			from	t_cliente a inner join t_carrinho b on a.id_cliente = b.id_cliente
			where	b.id_carrinho = @id_carrinho
	
			insert into t_compra (id_carrinho
								,dt_compra
								,vl_compra
								,vl_desconto
								,id_sub_produto
								,id_produto
								,id_inscricao)
			select				@id_carrinho, getdate(),@vl_compra, @vl_desconto, @id_sub_produto, @id_produto, NEWID()

			select @id_compra = SCOPE_IDENTITY()
		end

	end
	else
	begin
		update	a
		set		nr_cpf = ISNULL(@nr_cpf, nr_cpf)
		from	t_cliente a inner join t_carrinho b on a.id_cliente = b.id_cliente
		where	b.id_carrinho = @id_carrinho
	
		insert into t_compra (id_carrinho
							,dt_compra
							,vl_compra
							,vl_desconto
							,id_sub_produto
							,id_produto
							,id_inscricao)
		select				@id_carrinho, getdate(),@vl_compra, @vl_desconto, @id_sub_produto, @id_produto, NEWID()

		select @id_compra = SCOPE_IDENTITY()
	end

	select ISNULL(@id_compra,0) as 'retorno', @novo as 'novo', @id_carrinho as 'id_carrinho', @id_inscricao as 'id_inscricao'

end


