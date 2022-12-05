create procedure dbo.p_usuario_nivel_consulta(@id_usuario_nivel int)
as
begin

	select		id_usuario_nivel
				,nm_usuario_nivel
				,dv_admin
	from t_usuario_nivel 
	where id_usuario_nivel =  @id_usuario_nivel or @id_usuario_nivel = 0
end