alter procedure dbo.p_fabricante_incluir(
@id_fabricante int,
@nm_fabricante varchar(200),
@cnpj varchar(30),
@endereco varchar(200),
@cep varchar(10),
@cidade varchar(100),
@estado varchar(2),
@dv_ativo bit,
@dt_criacao datetime,
@id_usuario_criacao int,
@dt_alteracao datetime,
@id_usuario_alteracao int)
as
begin

	if (@id_fabricante = 0)
		insert into t_fabricante( nm_fabricante, cnpj, endereco, cep, cidade, estado, dv_ativo, dt_criacao, id_usuario_criacao, dt_alteracao, id_usuario_alteracao)
		values( @nm_fabricante, @cnpj, @endereco, @cep, @cidade, @estado, @dv_ativo, @dt_criacao, @id_usuario_criacao, @dt_alteracao, @id_usuario_alteracao)
	else
		update	t_fabricante
		set		nm_fabricante = @nm_fabricante, 
				cnpj = @cnpj, 
				endereco = @endereco, 
				cep = @cep, 
				cidade = @cidade, 
				estado = @estado, 
				dv_ativo = @dv_ativo, 
				dt_alteracao = @dt_alteracao, 
				id_usuario_alteracao = @id_usuario_alteracao
		where	id_fabricante = @id_fabricante
end


