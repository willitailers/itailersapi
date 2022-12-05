-- tabelas dominio
create table dbo.t_banco(
	id_banco int,
	nm_banco varchar(200));

create table dbo.t_tp_produto(
	id_tp_produto int identity(1,1) primary key,
	nm_tp_produto varchar(200) not null,
	dv_assinatura bit,
	dv_venda bit);

create table dbo.t_tp_pagamento (
	id_tp_pagamento int identity(1,1) primary key,
	nm_tp_pagamento varchar(100),
	dv_ativo bit);

-- Fabricante
create table dbo.t_fabricante (
	id_fabricante int identity(1,1) primary key,
	nm_fabricante varchar(200) not null,
	cnpj varchar(30) not null,
	endereco varchar(200),
	cep varchar(10),
	cidade varchar(100),
	estado varchar(2),
	dv_ativo bit,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );

create table dbo.t_fabricante_info(
	id_fabricante_info int identity(1,1) primary key,
	id_fabricante int not null,
	nm_titulo_info varchar(100),
	nm_informacao varchar(4000));

create table dbo.t_fabricante_contato(
	id_fabricante_contato int identity(1,1) primary key,
	id_fabricante int not null,
	nm_contato varchar(100),
	nm_telefone varchar(20),
	nm_ramal varchar(10),
	nm_observacao varchar(500),
	dv_principal bit);

-- Produto
create table dbo.t_produto(
	id_produto int identity(1,1) primary key,
	id_fabricante int not null,
	nm_produto varchar(200),
	nm_produto_descr_curta varchar(250),
	nm_produto_descr_completa varchar(1000),
	nm_produto_descr_completa2 varchar(1000),
	link_downaload varchar(300),
	dv_ativo bit,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );
---- Sub Produto


create table dbo.t_produto_sub(
	id_produto_sub int identity(1,1) primary key,
	id_produto int not null,
	nm_produto_sub varchar(200) not null,
	nm_produto_sub_descr_curta varchar(250),
	nm_produto_sub_descr_completa varchar(1000),
	nm_produto_sub_descr_completa2 varchar(1000),
	vl_produto_sub numeric(18,2),
	id_tp_produto int not null,
	dv_ativo bit,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );

-- Parceiro
create table dbo.t_parceiro(
	id_parceiro int identity(1,1) primary key,
	nm_parceiro varchar(200) not null,
	nm_identificacao_externa varchar(100),
	cnpj varchar(30) not null,
	endereco varchar(200),
	cep varchar(10),
	cidade varchar(100),
	estado varchar(2),
	dv_ativo bit,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );

create table dbo.t_parceiro_contato(
	id_parceiro_contato int identity(1,1) primary key,
	id_parceiro int not null,
	nm_contato varchar(100),
	nr_telefone varchar(20),
	nr_celular varchar(20),
	nr_ramal varchar(10),
	nr_observacao varchar(500),
	dv_ativo bit,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );

create table dbo.t_parceiro_dados_pagto(
	id_parceiro_dados_pagto int identity(1,1) primary key,
	id_parceiro int not null,
	id_banco int,
	nr_conta varchar(10),
	nr_cc varchar(20),
	nm_titular varchar(200),
	cpf_cnpj varchar(30),
	dv_pagto_diario bit,
	dv_pagto_semanal bit,
	dv_pagto_mensal bit,
	dv_pagto_dia_mes bit,
	nr_dia int,
	dv_ativo bit,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );


---- Parceiro Conta (Token)
create table dbo.t_parceiro_token(
	id_parceiro_token int identity(1,1) primary key,
	id_parceiro int not null,
	id_parceiro_dados_pagto int not null,
	nr_token varchar(25) not null,
	dv_ativo bit,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );

create table dbo.t_parceiro_token_acesso(
	id_parceiro_token int not null,
	nr_acessos int not null);


------ parceiro conta - produto sub
create table dbo.t_parceiro_token_produto(
	id_parceiro_token_produto int identity(1,1) primary key,
	id_parceiro_token int not null,
	id_produto int not null,
	dv_ativo bit,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );

------ parceiro conta - produto sub -- preco
create table dbo.t_parceiro_token_produto_sub(
	id_parceiro_token_produto_sub int identity(1,1) primary key,
	id_parceiro_token_produto int not null,
	id_produto_sub int not null,
	dv_ativo bit,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );

create table dbo.t_parceiro_token_produto_sub_preco(
	id_parceiro_token_produto_sub int,
	vl_preco numeric(18,2) not null,
	dt_inicial datetime,
	dt_final datetime,
	dt_criacao datetime not null,
	id_usuario_criacao int not null,
	dt_alteracao datetime null,
	id_usuario_alteracao int null );

	--------------------------------------------------------- FIM produto / parceiro
-- Cliente
create table dbo.t_cliente (
	id_cliente int identity(1,1) primary key,
	nm_email varchar(100) not null,
	nm_cliente varchar(200),
	nr_telefone varchar(20),
	nm_ramal varchar(10),
	nr_celular varchar(20),
	dt_nascimento datetime,
	endereco varchar(200),
	cep varchar(10),
	cidade varchar(100),
	estado varchar(2));

-- Pagamento
--create table dbo.t_pagamento(
--	id_pagamento int identity(1,1) primary key,
--	id_cliente int not null,
--	data_compra datetime,
--	data_pagamento datetime,
--	vl_pagamento datetime,
--	id_tp_pagamento int,
--	id_pagamento_identificacao varchar(100),
--	dt_criacao datetime not null,
--	id_usuario_criacao int not null,
--	dt_alteracao datetime null,
--	id_usuario_alteracao int null );

--create table 