
create table dbo.t_cliente(
id_cliente int identity(1,1) primary key,
nm_cliente varchar(500))

go
create table dbo.t_cliente_token(
id_cliente_token int identity(1,1) primary key,
id_cliente int,
nm_token varchar(500),
dt_validade_token datetime,
id_status int not null)


go
create table dbo.t_cliente_usuario(
id_cliente_usuario int identity(1,1) primary key,
id_cliente int not null,
nm_user_id varchar(500),
nm_transaction_id varchar(500),
nm_email varchar(500),
dt_start datetime,
nm_user_document varchar(500),
nm_user_plan varchar(500),
id_status int)

go

create table dbo.t_cliente_licenca(
id_cliente_licenca int identity(1,1) primary key,
id_cliente_usuario int not null,
id_produto int not null,
cd_ativacao_kl varchar(500) not null,
nm_subscriber_id varchar(500),
nm_ativacao_android varchar(500),
nm_ativacao_iphone varchar(500),
nm_ativacao_windows varchar(500),
nm_ativacao_mac varchar(500),
dt_ativacao datetime,
dt_expiracao datetime,
dt_cancelamento datetime,
id_status int)

go
create table dbo.t_cliente_certificado(
	id_cliente_certificado int identity(1,1) primary key,
	id_cliente_token int not null,
	nm_thumbprint varchar(500),
	nm_usuario_certificado  varchar(500),
	nm_senha_certificado varchar(500))

go
create table dbo.t_cliente_produto_certificado(
	id_cliente int not null,
	id_produto_kl int not null,
	id_cliente_certificado int not null,
	dv_ativa_cadastro int)

GO
CREATE TABLE [dbo].[t_produto_kl](
	[id_produto_kl] [int] NULL,
	[nm_produto_kl] [varchar](100) NULL,
	[cd_produto_kl] [varchar](100) NULL,
	[nm_urn] [varchar](100) NULL,
	[qtd_licencas] [int] NULL,
	[id_combo] [int] NULL
) ON [PRIMARY]
GO

