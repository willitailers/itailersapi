alter procedure dbo.p_carrinho_inserir ( @nm_token varchar(500), @id_cliente int)
as
begin
	declare @id_carrinho bigint
	declare @id_parceiro_token  int
	declare @cd_carrinho varchar(200) = replace(convert(varchar(200), NEWID()) ,'-','')

	select @id_parceiro_token = id_parceiro_token from t_parceiro_token where nr_token = @nm_token

	insert into t_carrinho (dt_compra, nm_token , id_cliente, id_parceiro_token, cd_carrinho)
	select getdate(), @nm_token, @id_cliente, @id_parceiro_token, @cd_carrinho

	select @id_carrinho = SCOPE_IDENTITY()

	select ISNULL(@id_carrinho, 0) as 'id_carrinho', @cd_carrinho as 'cd_carrinho', @nm_token as 'nm_token'

end
