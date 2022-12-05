alter procedure dbo.p_compra_dados_pagamento_inserir (@id_compra bigint, @id_tp_pagamento int,
				@nr_cartao varchar(20), @nm_cartao varchar(20), @dt_cartao varchar(5), 
				@id_banco int, @cc varchar(10), @cc_dg varchar(5), @ag varchar(10), @ag_dg varchar(5), @cvv varchar(4))
as
begin
	declare @id_compra_dados_pagamento bigint

	delete from t_compra_dados_pagamento where id_compra = @id_compra

	insert into dbo.t_compra_dados_pagamento (id_compra
				,id_tp_pagamento
				,nr_cartao
				,nm_cartao
				,dt_cartao
				,id_banco
				,cc
				,cc_dg
				,ag
				,ag_dg
				,cvv)
	select		 @id_compra
				,@id_tp_pagamento
				,@nr_cartao
				,@nm_cartao
				,@dt_cartao
				,@id_banco
				,@cc
				,@cc_dg
				,@ag
				,@ag_dg
				,@cvv

	select @id_compra_dados_pagamento = SCOPE_IDENTITY()

	select ISNULL(@id_compra_dados_pagamento, 0) as 'retorno'

end