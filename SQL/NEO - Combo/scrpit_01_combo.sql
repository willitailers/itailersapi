create table dbo.t_produto (id_produto int, 
nm_produto varchar(100),
cd_produto varchar(100),
nm_urn varchar(100),
qtd_licencas int)


alter table dbo.t_produto_kl add id_combo int 

create table dbo.t_produto_kl_combo (id_combo int, id_produto int)
go

delete from t_produto
insert into t_produto (id_produto, nm_produto, cd_produto, nm_urn, qtd_licencas)
values (10,	'Kaspersky Internet Security',	'KIS1FDBR',	'urn:sva:kaspersky:internetsecurity',	1),
(11,	'Kaspersky Internet Security',	'KIS3FDBR',	'urn:sva:kaspersky:internetsecurity',	3),
(12,	'Kaspersky Internet Security',	'KIS5FDBR',	'urn:sva:kaspersky:internetsecurity',	5),
(20, 'Kaspersky Password Manager',	'KPMFDBR',	'urn:sva:kaspersky:passwordmanager',	1),
(30, 'Kaspersky Safekids',	'KPMFDBR',	'urn:sva:kaspersky:safekids',	1),
(40, 'Kaspersky Internet Security for Android', 'KL1091KGAMI', 'urn:sva:kaspersky:kisa', 1),
(50, 'KISMD', 'KL1939KGAMI', 'urn:sva:kaspersky:kismd', 1)

delete from t_produto_kl
insert into t_produto_kl (id_produto_kl, nm_produto_kl, cd_produto_kl, nm_urn, qtd_licencas, id_combo)
values (1,	'Kaspersky Internet Security',	'KIS3FDBR',	'urn:sva:kaspersky:internetsecurity',	3, null),
(2,	'Kaspersky Password Manager',	'KPMFDBR',	'urn:sva:kaspersky:passwordmanager',	1, null),
(3,	'Kaspersky Safekids',	'KPMFDBR',	'urn:sva:kaspersky:safekids',	1, null),
(4, 'Kaspersky Combo', '', 'urn:sva:kaspersky:combo1', 1, 1)

insert into t_produto_kl_combo (id_combo, id_produto)
values (1, 11), (1, 20), (1, 40)




