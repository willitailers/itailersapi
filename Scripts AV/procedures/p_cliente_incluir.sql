
alter procedure dbo.p_cliente_incluir(
@id_cliente bigint,
@id_carrinho int,
@nm_email varchar(100),
@nm_cliente varchar(200),
@nr_telefone varchar(20),
@nm_ramal varchar(10),
@nr_celular varchar(20),
@dt_nascimento datetime,
@endereco varchar(200),
@cep varchar(10),
@cidade varchar(100),
@estado varchar(2),
@nm_cliente_sobrenome varchar(200),
@nr_cpf varchar(20),
@id_tp_pessoa int,
@nm_inscricao varchar(100)
,@endereco_nr varchar(100)
,@bairro varchar(100)
,@pais varchar(100)
,@nm_razao_social varchar(200)
,@v_sexo varchar(1) = 'M'

)
as
begin

	if @id_cliente > 0
		delete from t_cliente where id_cliente = @id_cliente

	insert into t_cliente( nm_email, nm_cliente, nr_telefone, nm_ramal, nr_celular, dt_nascimento, endereco, cep, cidade, estado, nm_cliente_sobrenome, nr_cpf, id_tp_pessoa , nm_inscricao, endereco_nr, bairro, pais,nm_razao_social, v_sexo)
	values( @nm_email, @nm_cliente, @nr_telefone, @nm_ramal, @nr_celular, @dt_nascimento, @endereco, @cep, @cidade, @estado, @nm_cliente_sobrenome, @nr_cpf, @id_tp_pessoa , @nm_inscricao, @endereco_nr, @bairro, @pais, @nm_razao_social, @v_sexo)

	select @id_cliente = SCOPE_IDENTITY()

	if (@id_carrinho > 0)
		update t_carrinho set id_cliente = @id_cliente where id_carrinho = @id_carrinho

	select ISNULL(@id_cliente,0) as 'retorno'

end





