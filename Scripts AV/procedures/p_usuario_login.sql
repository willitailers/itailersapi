CREATE procedure dbo.p_usuario_login(@nm_email varchar(100), @nm_senha varchar(500))
as
begin
	select	 a.id_usuario
			,a.nm_email
			,a.nm_usuario_primeiro
			,a.nm_usuario_segundo
			,a.dv_ativo
			,a.id_usuario_nivel
			,a.nm_auth
	from	t_usuario a
	where	a.nm_email = @nm_email
			and a.nm_senha = @nm_senha
end

