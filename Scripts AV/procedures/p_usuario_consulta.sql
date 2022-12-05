alter procedure dbo.p_usuario_consulta(@nm_email varchar(100), @dv_ativo bit, @id_usuario_nivel int)
as
begin
	select		a.id_usuario, a.nm_email, a.nm_usuario_primeiro, a.nm_usuario_segundo, CONVERT(int, a.dv_ativo) as 'dv_ativo' , b.id_usuario_nivel, b.nm_usuario_nivel
	from		t_usuario a
	inner join	t_usuario_nivel b on a.id_usuario_nivel = b.id_usuario_nivel
	where		a.nm_email like '%' + ISNULL(@nm_email,'') + '%'
				and (a.dv_ativo = @dv_ativo)
				and (b.id_usuario_nivel = @id_usuario_nivel or @id_usuario_nivel = 0)
end