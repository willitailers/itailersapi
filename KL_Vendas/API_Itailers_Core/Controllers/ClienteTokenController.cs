using API_Itailers_Core.Entities;
using API_Itailers_Core.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Objetos;
using System.Data.SqlClient;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Itailers_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteTokenController : ControllerBase
    {
        private readonly string _connectionString;
        public ClienteTokenController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/<ClienteTokenController>
        [HttpGet]
        public async Task<IActionResult> GetClienteTokens()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from t_cliente_token";
                var clienteTokens = await con.QueryAsync<t_cliente_token>(query);

                return clienteTokens.Any() ? Ok(clienteTokens) : NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ClienteTokenController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteToken(int id)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var parameters = new { t_cliente_token = id };

                con.Open();
                var query = "Select * from t_cliente_token where id_cliente_token = @t_cliente_token";
                var clienteToken = await con.QueryFirstOrDefaultAsync<t_cliente_token>(query, parameters);

                return clienteToken != null ? Ok(clienteToken) : NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<ClienteTokenController>
        [HttpPost]
        public async Task<IActionResult> Post(ClienteTokenRequest model)
        {
            var clienteToken = new t_cliente_token
            {
                id_cliente = model.id_cliente,
                nm_token = model.nm_token,
                dt_validade_token = model.dt_validade_token,
                id_status = model.id_status,
            };

            var parameters = new
            {
                clienteToken.id_cliente,
                clienteToken.nm_token,
                clienteToken.dt_validade_token,
                clienteToken.id_status
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"insert into t_cliente_token output INSERTED.id_cliente_token 
                                    values (@id_cliente, @nm_token, @dt_validade_token, @id_status)";
                var id = await con.QuerySingleAsync<int>(sql, parameters);

                clienteToken.id_cliente_token = id;

                return id > 0 ? Ok(clienteToken) : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<ClienteTokenController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteTokenRequest model)
        {
            var clienteToken = new t_cliente_token
            {
                id_cliente = model.id_cliente,
                nm_token = model.nm_token,
                dt_validade_token = model.dt_validade_token,
                id_status = model.id_status
            };

            var parameters = new
            {
                id,
                clienteToken.id_cliente,
                clienteToken.nm_token,
                clienteToken.dt_validade_token,
                clienteToken.id_status
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"update t_cliente_token set id_cliente = @id_cliente, nm_token = @nm_token,
                                    dt_validade_token = @dt_validade_token, id_status = @id_status where id_cliente_token = @id";
                var rowsAffected = await con.ExecuteAsync(sql, parameters);

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<ClienteTokenController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var parameters = new
            {
                id
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "delete from t_cliente_token where id_cliente_token = @id";
                var rowsAffected = await con.ExecuteAsync(query, parameters);

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
