create procedure dbo.p_melhor_produto(@tp_acao int, @id_melhor_produto int, @nm_pagina varchar(100), @nm_titulo_melhor_produto varchar(150), @nm_melhor_produto varchar(200))
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
		select id_melhor_produto, nm_pagina, nm_titulo_melhor_produto, nm_melhor_produto from t_melhor_produto_pagina where nm_pagina = ISNULL(@nm_pagina, nm_pagina)
		order by id_melhor_produto asc
	end

	if (@tp_acao = 2)
	begin
		insert into t_melhor_produto_pagina (nm_pagina, nm_melhor_produto, nm_titulo_melhor_produto)
		values (@nm_pagina, @nm_melhor_produto, @nm_titulo_melhor_produto)
	end

	if (@tp_acao = 3)
	begin
		delete from t_melhor_produto_pagina where id_melhor_produto = @id_melhor_produto
	end

end