create procedure dbo.p_log_inserir(@id_tp_log int, @erro text, @id_usuario_log int)
as
begin
	insert into t_log (id_tp_log, erro, dt_erro, id_usuario_log)
	select @id_tp_log, @erro, getdate(), @id_usuario_log

end


