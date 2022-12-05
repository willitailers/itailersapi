create procedure dbo.p_retorna_string_generica(@Length int, @CharacterData varchar(max) output)
as
begin

declare @BinaryData varbinary(max)

set @BinaryData=crypt_gen_random (@Length) 

set @CharacterData=cast('' as xml).value('xs:base64Binary(sql:variable("@BinaryData"))', 'varchar(max)')

return @CharacterData

end