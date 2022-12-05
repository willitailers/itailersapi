create procedure dbo.p_ligas_tabela(@id_liga int)
as
begin
	
		 select  a.id_evento, isnull(b.nm_time_exibicao, b.nm_time) + ' X ' + isnull(c.nm_time_exibicao, c.nm_time) as 'times',  --dateadd(hour, -2,a.dt_jogo) as 'Dt_jogo',  
			isnull(b.nm_time_exibicao, b.nm_time) as 'timecasa', isnull(c.nm_time_exibicao,c.nm_time) as 'timefora',  
			CONVERT(VARCHAR(10), dateadd(hour, dbo.f_hora(),a.dt_jogo), 103) as 'dia_jogo',  convert(VARCHAR(5), dateadd(hour, dbo.f_hora(),a.dt_jogo), 108) as 'hora_jogo',  
			d.id_evento_mercado as 'id_evento_mercado_1', d.vl_odds as 'vl_odds_1',  
			e.id_evento_mercado as 'id_evento_mercado_2', e.vl_odds as 'vl_odds_2',  
			f.id_evento_mercado as 'id_evento_mercado_X', f.vl_odds as 'vl_odds_X',
			ISNULL(g.nm_liga_exibicao, g.nm_liga) as 'nm_liga', coalesce(h.nm_pais_exibicao, h.nm_pais, 'Internacional') as 'nm_pais',
			x.id_evento_mercado as 'id_evento_mercado_1X', x.vl_odds as 'vl_odds_1X',  
			y.id_evento_mercado as 'id_evento_mercado_X2', y.vl_odds as 'vl_odds_X2',  
			z.id_evento_mercado as 'id_evento_mercado_AMBS', z.vl_odds as 'vl_odds_AMBS'
		 from  t_evento a  
		 inner join t_time b on a.id_time_casa = b.id_time  
		 inner join t_time c on c.id_time = a.id_time_visitante  
		 inner join t_evento_mercado d on d.id_mercado_opcao = 1066 and a.id_evento  = d.id_evento -- 1   
		 inner join t_evento_mercado e on e.id_mercado_opcao = 1068 and a.id_evento  = e.id_evento -- 2  
		 inner join t_evento_mercado f on f.id_mercado_opcao = 1067 and a.id_evento  = f.id_evento -- X 
		 left join t_evento_mercado x on x.id_mercado_opcao = 1073 and a.id_evento  = x.id_evento -- 1X   
		 left join t_evento_mercado y on y.id_mercado_opcao = 1075 and a.id_evento  = y.id_evento -- X2  
		 left join t_evento_mercado z on z.id_mercado_opcao = 1130 and a.id_evento  = z.id_evento -- AMBAS SIM 
		 inner join t_liga g on g.id_liga = a.id_liga
		 left join t_pais h on g.id_pais = h.id_pais
		 inner join t_liga_prioridade w on w.id_liga = g.id_liga
		 where g.id_liga = @id_liga
			and a.ic_ativo = 1  
			and a.id_tp_evento = 1
		 order by  ISNULL(w.nr_ordem, 99), g.id_liga, a.dt_jogo, a.id_evento

end