create procedure dbo.p_token_gerar
as
begin
	select left(replace( convert(varchar(200), newid()), '-', ''), 25) as 'retorno'

end