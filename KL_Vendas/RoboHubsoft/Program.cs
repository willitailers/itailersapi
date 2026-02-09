using Dapper;
using Microsoft.Data.SqlClient;
using RoboHubsoft.Helpers;
using RoboHubsoft.Models;
using RoboHubsoft.Service;
using System.Data;
using System.Net;

const string ConnectionString = "Server=35.232.96.49;Database=db_itailers_api;User Id=sqlserver;Password=Sup3rM4n;TrustServerCertificate=True;";

Console.WriteLine("Robô de Processamento de Clientes Iniciado.");

// 3. Implementação da consulta de clientes
var clientes = await GetClientesAsync();

if (clientes == null || clientes.Count == 0)
{
    Console.WriteLine("Nenhum cliente encontrado. Encerrando.");
    return;
}

Console.WriteLine($"Encontrados {clientes.Count} clientes para processamento.");

// 4. Lógica de Threading (usando Task para paralelismo)
var tasks = new List<Task>();
foreach (var cliente in clientes)
{
    // Inicia uma nova Task para cada cliente
    //tasks.Add(Task.Run(() => ProcessarClienteAsync(cliente)));

    tasks.Add(Task.Run(() => ProcessarFilaProcessamentoHubsoftAsync(cliente)));
}

Console.WriteLine("Todos os robôs de clientes foram iniciados. Pressione Ctrl+C para encerrar.");

// Mantém o programa principal rodando indefinidamente
await Task.Delay(Timeout.Infinite);

static async Task<List<t_hubsoft_cliente>> GetClientesAsync()
{
    Console.WriteLine("Consultando clientes no banco de dados...");

    try
    {
        using IDbConnection db = new SqlConnection(ConnectionString);
        var sql = "SELECT * FROM t_hubsoft_cliente where ativo = 1";
        var clientes = (await db.QueryAsync<t_hubsoft_cliente>(sql)).AsList();
        return clientes;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao consultar clientes: {ex.Message}");
        // Em um ambiente real, você deve implementar um log mais robusto.
        return new List<t_hubsoft_cliente>();
    }
}

static async Task ProcessarClienteAsync(t_hubsoft_cliente cliente)
{
    Console.WriteLine($"[Cliente {cliente.id}] Robô iniciado para {cliente.nome}.");

    while (true)
    {
        try
        {
            Console.WriteLine($"[Cliente {cliente.id}] Executando ciclo de processamento...");

            Console.WriteLine($"[Cliente {cliente.id}] Fazendo Request API Hubsoft E Salvando na Fila Processamento...");
            await FazerRequestApiHubsoftESalvarNaFilaProcessamentoAsync(cliente);

            Console.WriteLine($"[Cliente {cliente.id}] Iniciando processamento da Fila Processamento...");

            //Le a fila processamento, ativa os usuarios na api kiddlepass

            Console.WriteLine($"[Cliente {cliente.id}] Ciclo concluído. Aguardando 30 minutos.");

            // Espera de 30 minutos (1800000 milissegundos)
            await Task.Delay(TimeSpan.FromMinutes(30));
        }
        catch (Exception ex)
        {
            // Em caso de erro (API ou DB), loga e espera 1 minuto antes de tentar novamente
            Console.WriteLine($"[Cliente {cliente.id}] Erro no ciclo de processamento: {ex.Message}. Tentando novamente em 1 minuto.");
            await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }
}
static async Task ProcessarFilaProcessamentoHubsoftAsync(t_hubsoft_cliente cliente)
{
    Console.WriteLine($"[Cliente {cliente.id}] Processando Fila");

    while (true)
    {
        try
        {
            Console.WriteLine($"[Cliente {cliente.id}] Iniciando processamento da Fila Processamento...");

            List<t_hubsoft_filaprocessamento> filaprocessamento = new List<t_hubsoft_filaprocessamento>();
            filaprocessamento = await RetornaFilaDeProcessamentoHubsoft(cliente);

            if (filaprocessamento.Any())
            {
                KiddlepassApiClient kiddlepassApiClient = new KiddlepassApiClient();

                int count = 1;
                int countTotal = filaprocessamento.Count;
                foreach (var itemProcessar in filaprocessamento)
                {
                    
                    count++;

                    for (int i = 0; i < 10; i++)
                    {
                        string email = !string.IsNullOrEmpty(itemProcessar.email) ? itemProcessar.email : "";
                        var upstream = await kiddlepassApiClient.PostInsertUserAsync(itemProcessar.cpf, "Regional", "2", "default", itemProcessar.email, itemProcessar.nome);
                        var bodyText = await upstream.Content.ReadAsStringAsync();

                        t_kiddlepass_usuarios t_Kiddlepass_Usuarios = new t_kiddlepass_usuarios()
                        {
                            userid = itemProcessar.cpf,
                            id_cliente = 2,
                            product_type = "default",
                            status = "active",
                            email = email,
                            name = itemProcessar.nome
                        };

                        if (upstream.IsSuccessStatusCode)
                        {
                            try
                            {
                                await InserirUsuarioKiddlepass(t_Kiddlepass_Usuarios);

                                await AtualizarItemFilaProcessamentoIniciado(itemProcessar.id);
                            }
                            catch (System.Exception ex)
                            {
                            }

                            Console.WriteLine($"Processado Sucesso {count} de {countTotal}");

                            break;
                        }
                        else if (upstream.StatusCode == HttpStatusCode.Conflict && bodyText.Contains("already exists"))
                        {
                            await InserirUsuarioKiddlepass(t_Kiddlepass_Usuarios);
                            await AtualizarItemFilaProcessamentoIniciado(itemProcessar.id);

                            Console.WriteLine($"Processado Já Existe {count} de {countTotal}");

                            break;
                        }
                        else if (upstream.StatusCode == HttpStatusCode.TooManyRequests)
                        {
                            Console.WriteLine($"Processado Muitos Requests, esperando {count} de {countTotal}");
                            Thread.Sleep(TimeSpan.FromSeconds(2));
                        }
                    }
                }
            }

            Console.WriteLine($"[Cliente {cliente.id}] Ciclo concluído. Aguardando 30 minutos.");

            // Espera de 30 minutos (1800000 milissegundos)
            await Task.Delay(TimeSpan.FromMinutes(30));
        }
        catch (Exception ex)
        {
            // Em caso de erro (API ou DB), loga e espera 1 minuto antes de tentar novamente
            Console.WriteLine($"[Cliente {cliente.id}] Erro no ciclo de processamento: {ex.Message}. Tentando novamente em 1 minuto.");
            await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }
}

static async Task FazerRequestApiHubsoftESalvarNaFilaProcessamentoAsync(t_hubsoft_cliente cliente)
{
    HubsoftApiClient hubsoftApiClient = new HubsoftApiClient();

    //Busca todos os clientes da Hubsoft para o cliente passado no parametro
    List<Cliente> clientesHubsoft = await hubsoftApiClient.GetAllClientes(cliente);

    //Aplicar filtros
    List<Cliente> clientesSalvar = clientesHubsoft.Where(w => w.Ativo && w.TipoPessoa == "pf").ToList();
    clientesSalvar = clientesSalvar.Where(w => w.Servicos.Any(a => a.StatusPrefixo == "servico_habilitado")).ToList();
    clientesSalvar = clientesSalvar.Where(w => w.Servicos.Any(a => a.Valor != 0)).ToList();
    clientesSalvar = clientesSalvar.Where(w => w.Servicos.Any(a => string.IsNullOrEmpty(a.DataCancelamento))).ToList();

    List<t_kiddlepass_usuarios> kiddlepass_usuarios = new List<t_kiddlepass_usuarios>();
    kiddlepass_usuarios = await RetornaKiddlepassUsuariosPorHubsoftClienteId(cliente);

    if (kiddlepass_usuarios.Any())
        clientesSalvar = clientesSalvar.Where(w => !kiddlepass_usuarios.Select(s => s.userid).Contains(CpfHelper.NormalizarCpf(w.CpfCnpj))).ToList();

    List<t_hubsoft_filaprocessamento> filaprocessamento = new List<t_hubsoft_filaprocessamento>();
    filaprocessamento = await RetornaFilaDeProcessamentoHubsoft(cliente);

    if (filaprocessamento.Any())
        clientesSalvar = clientesSalvar.Where(w => !filaprocessamento.Select(s => s.cpf).Contains(CpfHelper.NormalizarCpf(w.CpfCnpj))).ToList();

    //Salvar Resultado no Banco de dados
    await SalvarResultadoHubsoftFilaProcessamentoAsync(clientesSalvar, cliente.id);

    Console.WriteLine($"[Cliente {cliente.id}] Dados da API processados e salvos no DB.");
}

static async Task<List<t_kiddlepass_usuarios>> RetornaKiddlepassUsuariosPorHubsoftClienteId(t_hubsoft_cliente hubsoft_cliente)
{
    using IDbConnection db = new SqlConnection(ConnectionString);

    var sql = @"Select ku.* from t_kiddlepass_usuarios ku 
	join t_hubsoft_kiddlepass_relacao hkr on ku.id_cliente = hkr.id_cliente_kiddlepass
    where hkr.id_cliente_hubsoft = @id_cliente_hubsoft";

    return (await db.QueryAsync<t_kiddlepass_usuarios>(sql, new
    {
        id_cliente_hubsoft = hubsoft_cliente.id
    })).AsList();
}

static async Task<List<t_hubsoft_filaprocessamento>> RetornaFilaDeProcessamentoHubsoft(t_hubsoft_cliente hubsoft_cliente)
{
    using IDbConnection db = new SqlConnection(ConnectionString);

    var sql = @"Select * from t_hubsoft_filaprocessamento where id_cliente = @id_cliente and processando = 0";

    return (await db.QueryAsync<t_hubsoft_filaprocessamento>(sql, new
    {
        id_cliente = hubsoft_cliente.id
    })).AsList();
}

static async Task SalvarResultadoHubsoftFilaProcessamentoAsync(List<Cliente> clientes, int p_id_cliente)
{
    foreach (var item in clientes)
    {
        try
        {
            using IDbConnection db = new SqlConnection(ConnectionString);

            var sql = "INSERT INTO t_hubsoft_filaprocessamento (id_cliente, nome, email, cpf, processando, dt_criacao) " +
                "VALUES (@id_cliente, " +
                "       @nome, " +
                "       @email, " +
                "       @cpf, " +
                "       @processando, " +
                "       getdate())";

            string email = "";

            if (!string.IsNullOrEmpty(item.EmailPrincipal) || !string.IsNullOrEmpty(item.EmailSecundario))
                email = item.EmailPrincipal == null ? item.EmailSecundario : item.EmailPrincipal;

            await db.ExecuteAsync(sql, new
            {
                id_cliente = p_id_cliente,
                nome = item.NomeRazaoSocial,
                email = email,
                cpf = CpfHelper.NormalizarCpf(item.CpfCnpj),
                processando = false
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar resultado da API para o cliente {item.IdCliente}, CPF: {item.CpfCnpj}: {ex.Message}");
            throw; // Propaga o erro para que o loop tente novamente
        }
    }
}

static async Task InserirUsuarioKiddlepass(t_kiddlepass_usuarios t_Kiddlepass_Usuario)
{
    try
    {
        using IDbConnection db = new SqlConnection(ConnectionString);

        var sql = "insert into t_kiddlepass_usuarios (userid, id_cliente, product_type, status, email, name, data_criacao, data_atualizacao) " +
            "values (@userid, @id_cliente, 'default', @status, @email, @name, getdate(), null)";

        await db.ExecuteAsync(sql, new
        {
            userid = t_Kiddlepass_Usuario.userid,
            id_cliente = t_Kiddlepass_Usuario.id_cliente,
            product_type = t_Kiddlepass_Usuario.product_type,
            status = t_Kiddlepass_Usuario.status,
            email = t_Kiddlepass_Usuario.email,
            name = t_Kiddlepass_Usuario.name
        });
    }
    catch (Exception ex)
    {
        throw; // Propaga o erro para que o loop tente novamente
    }
}

static async Task AtualizarItemFilaProcessamentoIniciado(int id)
{
    using IDbConnection db = new SqlConnection(ConnectionString);

    var sql = "update t_hubsoft_filaprocessamento set processando = 1 where id = @id";

    await db.ExecuteAsync(sql, new
    {
        id = id
    });
}
