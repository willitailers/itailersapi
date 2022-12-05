
-- Parceiro
insert into dbo.t_parceiro(
	nm_parceiro					,
	nm_identificacao_externa	,
	cnpj						,
	endereco					,
	cep							,
	cidade						,
	estado						,
	dv_ativo					,
	dt_criacao					,
	id_usuario_criacao			,
	dt_alteracao				,
	id_usuario_alteracao		)
values ('Web Media', '123456', '02.346.813/0001-63', 'Rua dos Alfineiros, 4', '02156333', 'Oxford', 'SP', 1, getdate(), 1, null, null),
('network System', '456789', '59.765.162/0001-63', 'Rua dos bobos, 0', '02156333', 'Williard', 'SP', 1, getdate(), 1, null, null),
('BooBox', '987321', '21.562.452/0001-37', 'Rua Ipiringa coma São joão', '02156333', 'Stike City', 'SP', 1, getdate(), 1, null, null)

insert into dbo.t_parceiro_contato(
	id_parceiro					,
	nm_contato					,
	nr_telefone					,
	nr_celular					,
	nr_ramal					,
	nr_observacao				,
	dv_ativo					,
	dt_criacao					,
	id_usuario_criacao			,
	dt_alteracao				,
	id_usuario_alteracao		)
values (1, 'Valeria','24569541', '98542212', '', 'Representante comercial', 1, getdate(), '', null, null),
(1, 'Marcos Paulo','8554563', '958463325', '', 'Dono empresa', 1, getdate(), '', null, null),
(2, 'Vinicius','33658965', '955632585', '', 'Representante comercial', 1, getdate(), '', null, null),
(3, 'mMarco Gomes','4556965', '966584474', '', 'Socio', 1, getdate(), '', null, null)

insert into dbo.t_parceiro_dados_pagto(
	id_parceiro				,
	id_banco				,
	nr_conta				,
	nr_cc					,
	nm_titular				,
	cpf_cnpj				,
	dv_pagto_diario			,
	dv_pagto_semanal		,
	dv_pagto_mensal			,
	dv_pagto_dia_mes		,
	nr_dia					,
	dv_ativo				,
	dt_criacao				,
	id_usuario_criacao		,
	dt_alteracao			,
	id_usuario_alteracao	)
values (1, 104, '00256-9', '0002569-7', 'Webmedia Ltda.', '02.346.813/0001-63', 0, 1, 0 ,0, 4, 1, getdate(), 1, null, null),
(2, 237, '2568-8', '8456-9', 'network System Ltda.', '59.765.162/0001-63', 0, 0, 0 ,1, 10, 1, getdate(), 1, null, null),
(3, 341, '0265-9', '85663-8', 'BooBox Sistemas Ltda.', '21.562.452/0001-37', 0, 0, 1 ,0, 25, 1, getdate(), 1, null, null)


---- Parceiro Conta (Token)
insert into dbo.t_parceiro_token(
	id_parceiro				,
	id_parceiro_dados_pagto ,
	nr_token				,
	dv_ativo				,
	dt_criacao				,
	id_usuario_criacao		,
	dt_alteracao			,
	id_usuario_alteracao	)
values (1, 1, (select convert(varchar(25), left(replace(NEWID(), '-',''), 25))), 1, getdate(), 1, null, null),
(1, 1, (select convert(varchar(25), left(replace(NEWID(), '-',''), 25))), 1, getdate(), 1, null, null),
(2, 2, (select convert(varchar(25), left(replace(NEWID(), '-',''), 25))), 1, getdate(), 1, null, null),
(3, 3, (select convert(varchar(25), left(replace(NEWID(), '-',''), 25))), 1, getdate(), 1, null, null)

------ parceiro conta - produto sub
insert into dbo.t_parceiro_token_produto(
	id_parceiro_token			,
	id_produto					,
	dv_ativo					,
	dt_criacao					,
	id_usuario_criacao			,
	dt_alteracao				,
	id_usuario_alteracao		)
select id_parceiro_token, 1, 1,getdate(), 1, null, null
from t_parceiro_token


--select * from t_produto_sub
------ parceiro conta - produto sub -- preco
insert into dbo.t_parceiro_token_produto_sub(
	id_parceiro_token_produto	,
	id_produto_sub				,
	dv_ativo					,
	dt_criacao					,
	id_usuario_criacao			,
	dt_alteracao				,
	id_usuario_alteracao		)
values (1, 1, 1, getdate(), 1, null, null), 
(1, 2, 1, getdate(), 1, null, null), 
(1, 3, 1, getdate(), 1, null, null), 
(1, 4, 1, getdate(), 1, null, null), 
(2, 1, 1, getdate(), 1, null, null), 
(2, 2, 1, getdate(), 1, null, null), 
(2, 3, 1, getdate(), 1, null, null), 
(2, 4, 1, getdate(), 1, null, null), 
(2, 5, 1, getdate(), 1, null, null), 
(2, 6, 1, getdate(), 1, null, null), 
(3, 1, 1, getdate(), 1, null, null), 
(3, 2, 1, getdate(), 1, null, null), 
(4, 5, 1, getdate(), 1, null, null), 
(4, 6, 1, getdate(), 1, null, null) 

--create table dbo.t_parceiro_token_produto_sub_preco(
--	id_parceiro_token_produto_sub int,
--	vl_preco numeric(18,2) not null,
--	dt_inicial datetime,
--	dt_final datetime,
--	dt_criacao datetime not null,
--	id_usuario_criacao int not null,
--	dt_alteracao datetime null,
--	id_usuario_alteracao int null );