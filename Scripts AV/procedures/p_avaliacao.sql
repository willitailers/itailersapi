
alter procedure dbo.p_avaliacao(@tp_acao int, @id_avalicao int, @nm_pagina varchar(100), @nm_titulo varchar(150), @nm_avaliacao varchar(2000))
as
begin
/*
@tp_acao 
1 = consulta
2 = incluir
3 = excluir
*/

	if (@tp_acao = 1)
	begin
		select id_avalicao, nm_pagina, nm_titulo, nm_avaliacao from t_avalicao_pagina where nm_pagina = ISNULL(@nm_pagina, nm_pagina)
		order by id_avalicao asc
	end

	if (@tp_acao = 2)
	begin
		insert into t_avalicao_pagina (nm_pagina, nm_avaliacao, nm_titulo)
		values (@nm_pagina, @nm_avaliacao, @nm_titulo)
	end

	if (@tp_acao = 3)
	begin
		delete from t_avalicao_pagina where id_avalicao = @id_avalicao
	end

end