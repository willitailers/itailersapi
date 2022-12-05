alter procedure dbo.p_parceiro(@id_parceiro int)
as
begin
	select id_parceiro,
nm_parceiro,
nm_identificacao_externa,
cnpj,
endereco,
cep,
cidade,
estado,
dv_ativo,
dt_criacao,
id_usuario_criacao,
dt_alteracao,
id_usuario_alteracao
from t_parceiro
where id_parceiro = @id_parceiro or @id_parceiro = 0

end

