

--insert into dbo.t_tp_produto(
--	nm_tp_produto,
--	dv_assinatura ,
--	dv_venda )
--select	'Anti-virus - Mensalidade', 1, 1

--insert into dbo.t_tp_produto(
--	nm_tp_produto,
--	dv_assinatura ,
--	dv_venda )
--select	'Anti-virus ', 0, 1

declare @id_fabricante int 
insert into dbo.t_fabricante (
	nm_fabricante ,
	cnpj ,
	endereco,
	cep ,
	cidade ,
	estado ,
	dv_ativo ,
	dt_criacao ,
	id_usuario_criacao ,
	dt_alteracao,
	id_usuario_alteracao)
values ('Kaspersky Lab', '11.139.530/0001-31', 'Av Queiroz Filho, 1700 EDIF: TORRE SKY TOWER; SALA: 402 E 403', '05319000', 'São Paulo', 'SP', 1, getdate(), 1, null, null)

select @id_fabricante = SCOPE_IDENTITY()

insert into dbo.t_fabricante_info(
	id_fabricante ,
	nm_titulo_info ,
	nm_informacao )
values (@id_fabricante, 'Exemplo', 'Aqui segue a informação que achar necessaria sobre o fabricante')

insert into dbo.t_fabricante_contato(
	id_fabricante ,
	nm_contato ,
	nm_telefone ,
	nm_ramal ,
	nm_observacao ,
	dv_principal )
values (@id_fabricante, 'Luciana', '58652144', null, 'Cuida da nossa conta', 1)

-- Produto
declare @id_produto int
insert into dbo.t_produto(
	id_fabricante ,
	nm_produto					,
	nm_produto_descr_curta		,
	nm_produto_descr_completa	,
	nm_produto_descr_completa2	,
	link_downaload				,
	dv_ativo					,
	dt_criacao					,
	id_usuario_criacao			,
	dt_alteracao				,
	id_usuario_alteracao		)
values (1, 'Antivirus', 'Protege seu PC e todos os itens importantes armazenados nele', 'Seu computador contém muitas informações importantes sobre a sua vida, por isso é importante que você faça tudo o que puder para protegê-las.
O Kaspersky Anti-Virus é a maneira mais inteligente de proteger todo o conteúdo do seu PC contra vírus, spyware e cavalos de Troia, além de impedir que um ransomware bloqueie todos os seus arquivos.', '',
'https://www.kaspersky.com.br/downloads/thank-you/antivirus-free-trial', 1, getdate(), 1, null, null)

select @id_produto = SCOPE_IDENTITY()
---- Sub Produto


insert into dbo.t_produto_sub(
	id_produto						,
	nm_produto_sub					,
	nm_produto_sub_descr_curta		,
	nm_produto_sub_descr_completa	,
	nm_produto_sub_descr_completa2	,
	vl_produto_sub					,
	id_tp_produto					,
	dv_ativo						,
	dt_criacao						,
	id_usuario_criacao				,
	dt_alteracao					,
	id_usuario_alteracao			)
values (@id_produto, '1 PC - 1 Ano', 'proteção para 1 pc por um ano', '','', 48.93, 2, 1, getdate(), 1, null, null),
(@id_produto, '1 PC - 2 Anos', 'proteção para 1 pc por dois anos', '','', 69.93, 2, 1, getdate(), 1, null, null),
(@id_produto, '3 PCs - 1 Ano', 'proteção para 3 pcs por um ano', '','', 104.93, 2, 1, getdate(), 1, null, null),
(@id_produto, '3 PCs - 2 Anos', 'proteção para 3 pcs por dois ano', '','', 160.93, 2, 1, getdate(), 1, null, null),
(@id_produto, '5 PCs - 1 Ano', 'proteção para 5 pcs por um ano', '','', 160.93, 2, 1, getdate(), 1, null, null),
(@id_produto, '5 PCs - 2 Ano', 'proteção para 5 pcs por dois ano', '','', 244.93, 2, 1, getdate(), 1, null, null)