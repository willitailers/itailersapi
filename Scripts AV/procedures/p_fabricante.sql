alter procedure dbo.p_fabricante(@id_fabricante int)
as
begin
	select id_fabricante,
	nm_fabricante,
	cnpj,
	endereco,
	cep,
	cidade,
	estado,
	convert(int, dv_ativo) as 'dv_ativo',
	case when convert(int, dv_ativo) = 1 then 'Sim' else 'Não' end as 'cd_ativo',
	dt_criacao,
	id_usuario_criacao,
	dt_alteracao,
	id_usuario_alteracao
	from t_fabricante
	where id_fabricante = @id_fabricante or @id_fabricante = 0
end

