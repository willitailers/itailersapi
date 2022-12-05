if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_insere_log')
	drop procedure p_insere_log 
go

create procedure dbo.p_insere_log(@nm_log varchar(5000), @id_tipo_log int)
as
begin

	insert into t_log (nm_log, id_tipo_log, dt_log)
	values (@nm_log, @id_tipo_log, getdate())

end