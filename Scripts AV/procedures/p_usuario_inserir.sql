create procedure dbo.p_usuario_inserir(@nm_email varchar(100), @nm_senha varchar(100), @nm_usuario_primeiro varchar(100), @nm_usuario_segundo varchar(100), @id_usuario_nivel int, @dv_ativo bit)
as
begin
	if exists (select 1 from t_usuario where nm_email = @nm_email)
	begin
		select -1 as 'id_usuario'
	end

	declare @id_usuario int

	insert into t_usuario (nm_email, nm_senha, nm_usuario_primeiro, nm_usuario_segundo, id_usuario_nivel, dv_ativo)
	values (@nm_email, @nm_senha, @nm_usuario_primeiro, @nm_usuario_segundo, @id_usuario_nivel, @dv_ativo)

	select @id_usuario = SCOPE_IDENTITY()

	select @id_usuario as 'id_usuario'

end