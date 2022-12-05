declare @nm_tabela varchar(100)
declare @tbl_campos table (cont int identity(1,1), nm_campo varchar(100), tipo_campo varchar(20), dv_chave bit)

declare @tbl table (campo varchar(4000))
declare @cont_max int, @cont_min int

set nocount on
select @nm_tabela = 'parceiro'

insert into @tbl_campos (nm_campo, tipo_campo, dv_chave)
values ('id_parceiro','int',1)
,('nm_parceiro','varchar(200)',0)
,('nm_identificacao_externa','varchar(100)',0)
,('cnpj','varchar(30)',0)
,('endereco','varchar(200)',0)
,('cep','varchar(10)',0)
,('cidade','varchar(100)',0)
,('estado','varchar(2)',0)
,('dv_ativo','bit',0)
,('dt_criacao','datetime',0)
,('id_usuario_criacao','int',0)
,('dt_alteracao','datetime',0)
,('id_usuario_alteracao','int',0)

select @cont_min = 1, @cont_max = max(cont) from @tbl_campos

insert into @tbl select 'create procedure dbo.p_' + @nm_tabela + '_selecionar ( @' + (select top 1 nm_campo + ' ' + tipo_campo from @tbl_campos where dv_chave = 1) + ' = NULL ) '
insert into @tbl select ' as begin '

insert into @tbl select ' select  '
while @cont_min <= @cont_max
begin
	

	insert into @tbl select case when @cont_min = 1 then '' else ',' end + (select top 1 nm_campo  from @tbl_campos where cont = @cont_min) 
	
	select @cont_min += 1
end

insert into @tbl select ' from t_' + @nm_tabela 
insert into @tbl select ' where ' + (select top 1 nm_campo from @tbl_campos where dv_chave = 1) + ' = ISNULL(@' + (select top 1 nm_campo from @tbl_campos where dv_chave = 1) + ', ' + (select top 1 nm_campo from @tbl_campos where dv_chave = 1) + ')'

insert into @tbl select ' end; '
insert into @tbl select ' go; '


insert into @tbl select 'create procedure dbo.p_' + @nm_tabela + '_atualizar ( '
select @cont_min = 1
while @cont_min <= @cont_max
begin
	
	insert into @tbl select case when @cont_min = 1 then '' else ',' end + '@' + (select top 1 nm_campo + ' ' + tipo_campo from @tbl_campos where cont = @cont_min) + ' = NULL '
	select @cont_min += 1
end

insert into @tbl select ' ) '
insert into @tbl select ' as begin '

insert into @tbl select ' update t_' + @nm_tabela + ' set '
select @cont_min = 1
while @cont_min <= @cont_max
begin
	
	if not exists (select 1 from @tbl_campos where cont = @cont_min and dv_chave = 1)
		insert into @tbl select case when @cont_min = 1 then '' else ',' end +  (select top 1 nm_campo from @tbl_campos where cont = @cont_min) + ' = ISNULL(@' + (select top 1 nm_campo from @tbl_campos where cont = @cont_min) + ', ' + (select top 1 nm_campo from @tbl_campos where cont = @cont_min) + ')'
	select @cont_min += 1
end

insert into @tbl select ' where ' + (select top 1 nm_campo from @tbl_campos where dv_chave = 1) + ' = ISNULL(@' + (select top 1 nm_campo from @tbl_campos where dv_chave = 1) + ', ' + (select top 1 nm_campo from @tbl_campos where dv_chave = 1) + ')'

insert into @tbl select ' end; '
insert into @tbl select ' go; '

insert into @tbl select 'create procedure dbo.p_' + @nm_tabela + '_incluir ( '
select @cont_min = 1
while @cont_min <= @cont_max
begin
	
	if not exists (select 1 from @tbl_campos where cont = @cont_min and dv_chave = 1)
		insert into @tbl select case when @cont_min = 1 then '' else ',' end + '@' + (select top 1 nm_campo + ' ' + tipo_campo from @tbl_campos where cont = @cont_min) + ' = NULL '
	select @cont_min += 1
end

insert into @tbl select ' ) '
insert into @tbl select ' as begin '

insert into @tbl select ' insert into t_' + @nm_tabela + ' ( '
select @cont_min = 1
while @cont_min <= @cont_max
begin
	
	if not exists (select 1 from @tbl_campos where cont = @cont_min and dv_chave = 1)
		insert into @tbl select case when @cont_min = 1 then '' else ',' end + '' + (select top 1 nm_campo from @tbl_campos where cont = @cont_min) 
	select @cont_min += 1
end
insert into @tbl select ' ) '
insert into @tbl select ' select '
select @cont_min = 1
while @cont_min <= @cont_max
begin
	
	if not exists (select 1 from @tbl_campos where cont = @cont_min and dv_chave = 1)
		insert into @tbl select case when @cont_min = 1 then '' else ',' end + '@' + (select top 1 nm_campo from @tbl_campos where cont = @cont_min) 
	select @cont_min += 1
end

insert into @tbl select ' end; '
insert into @tbl select ' go; '

insert into @tbl select 'create procedure dbo.p_' + @nm_tabela + '_excluir ( @' + (select top 1 nm_campo + ' ' + tipo_campo from @tbl_campos where dv_chave = 1) + ' = NULL ) '
insert into @tbl select ' as begin '

insert into @tbl select ' delete  from t_' + @nm_tabela 

insert into @tbl select ' where ' + (select top 1 nm_campo from @tbl_campos where dv_chave = 1) + ' = @' + (select top 1 nm_campo from @tbl_campos where dv_chave = 1) 

insert into @tbl select ' end; '
insert into @tbl select ' go; '


select * from @tbl
