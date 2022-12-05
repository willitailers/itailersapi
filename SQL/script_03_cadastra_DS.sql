
insert into t_cliente (nm_cliente) values ('Distribusoftware')
insert into t_cliente (nm_cliente) values ('Teste')

insert into t_cliente_token (id_cliente
,nm_token
,dt_validade_token
,id_status)
values (1, '3lemrj5s83j40d', '2050-01-01', 2)

insert into t_cliente_token (id_cliente
,nm_token
,dt_validade_token
,id_status)
values (2, 'kgflr94873krm4', '2050-01-01', 2)

insert into t_cliente_certificado (id_cliente_token
,nm_thumbprint
,nm_usuario_certificado
,nm_senha_certificado)
values (1, '51C7E065E9CF98F1539B5B18570AF5D6DEC2D60F', 'Info_Distrisoftware_new', '@Itailers2021#@p1Un1c4')

insert into t_cliente_certificado (id_cliente_token
,nm_thumbprint
,nm_usuario_certificado
,nm_senha_certificado)
values (2, '51C7E065E9CF98F1539B5B18570AF5D6DEC2D60F', 'Info_Distrisoftware_new', '@Itailers2021#@p1Un1c4')

insert into t_produto_kl (id_produto_kl
,nm_produto_kl
,cd_produto_kl
,nm_urn
,qtd_licencas
,id_combo)
values (1, 'Karpersky Internet Security', 'KIS','urn:sva:kaspersky:internetsecurity1', 1, 0 ),
(2, 'Karpersky Internet Security', 'KIS','urn:sva:kaspersky:internetsecurity3', 3, 0 ),
(3, 'Karpersky Internet Security', 'KIS','urn:sva:kaspersky:internetsecurity5', 5, 0 ),
(4, 'Karpersky Internet Security', 'KIS','urn:sva:kaspersky:internetsecurity10', 10, 0 ),
(10, 'Karpersky Internet Security for Android', 'KISA','urn:sva:kaspersky:kisa1', 1, 0 ),
(11, 'Karpersky Internet Security for Android', 'KISA','urn:sva:kaspersky:kisa3', 3, 0 ),
(20, 'Karpersky Password Manager', 'KPMA','urn:sva:kaspersky:passwordmanager', 1, 0 ),
(30, 'Karpersky Safe Kids', 'KSK','urn:sva:kaspersky:safekids', 1, 0 ),
(40, 'Karpersky Secutiry', 'KISMD','urn:sva:kaspersky:security', 1, 0 )


insert into t_cliente_produto_certificado (id_cliente
,id_produto_kl
,id_cliente_certificado,
dv_ativa_cadastro)
select	1, id_produto_kl, 1, 0
from	t_produto_kl


insert into t_cliente_produto_certificado (id_cliente
,id_produto_kl
,id_cliente_certificado,
dv_ativa_cadastro)
select	2, id_produto_kl, 2, 0
from	t_produto_kl
