alter procedure dbo.p_monta_menu(@id_usuario int)
as
begin

	select		a.id_menu, nm_menu, nm_tela , ISNULL(id_menu_pai ,0) as 'id_menu_pai', nm_icone, nr_ordem, convert(int, b.dv_edita) as 'dv_edita',
				case when exists (select top 1 1 from t_menu x where x.id_menu_pai = a.id_menu) then 1 else 0 end as 'dv_filho'
	from		t_menu a
	inner join	t_menu_grupo b on a.id_menu = b.id_menu
	inner join	t_usuario_nivel c on c.id_usuario_nivel = b.id_usuario_nivel
	inner join	t_usuario d on d.id_usuario_nivel = c.id_usuario_nivel
	where		d.id_usuario = @id_usuario
	order by	nr_ordem

end

