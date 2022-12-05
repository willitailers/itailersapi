alter procedure dbo.p_cliente_selecionar(@id_cliente int)  
as  
begin  
		select id_cliente
				,nm_email
				,nm_cliente
				,nr_telefone
				,nm_ramal
				,nr_celular
				,dt_nascimento
				,endereco
				,cep
				,cidade
				,estado
				,nm_cliente_sobrenome
				,nr_cpf
				,id_tp_pessoa
				,nm_inscricao
				,endereco_nr
				,bairro
				,pais 
				,nm_razao_social
	from t_cliente  
	where id_cliente = @id_cliente  
end

