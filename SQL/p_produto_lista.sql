if exists (SELECT 1 FROM sys.procedures WHERE name = 'p_produto_lista')
	drop procedure p_produto_lista 
go

CREATE procedure dbo.p_produto_lista(@id_produto_kl int)    
as    
begin    
 select id_produto_kl    
   ,nm_produto_kl    
   ,cd_produto_kl    
   ,nm_urn    
   ,qtd_licencas   
   ,isnull(id_combo, 0) as 'id_combo'
   ,nm_imagem
   ,descricao 
 from t_produto_kl (nolock)    
 where id_produto_kl = @id_produto_kl or @id_produto_kl = 0    
end