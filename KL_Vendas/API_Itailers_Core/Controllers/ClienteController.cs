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
    public class ClienteController : ControllerBase
    {
        private readonly string _connectionString;
        public ClienteController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from t_cliente";
                var clientes = await con.QueryAsync<t_cliente>(query);

                return clientes.Any() ? Ok(clientes) : NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var parameters = new { id_cliente = id };

                con.Open();
                var query = "Select * from t_cliente where id_cliente = @id_cliente";
                var cliente = await con.QueryFirstOrDefaultAsync<t_cliente>(query, parameters);

                return cliente != null ? Ok(cliente) : NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<ClienteController>
        [HttpPost]
        public async Task<IActionResult> Post(ClienteRequest model)
        {
            var cliente = new t_cliente { nm_cliente = model.nm_cliente };

            var parameters = new
            {
                cliente.nm_cliente
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = "insert into t_cliente output INSERTED.id_cliente values (@nm_cliente)";
                var id = await con.QuerySingleAsync<int>(sql, parameters);

                cliente.id_cliente = id;

                return id > 0 ? Ok(cliente) : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteRequest model)
        {
            var cliente = new t_cliente { id_cliente = id, nm_cliente = model.nm_cliente };

            var parameters = new
            {
                id,
                cliente.nm_cliente
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = "update t_cliente set nm_cliente = @nm_cliente where id_cliente = @id";
                var rowsAffected = await con.ExecuteAsync(sql, parameters);

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<ClienteController>/5
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
                var sql = "delete from t_cliente where id_cliente = @id";
                var rowsAffected = await con.ExecuteAsync(sql, parameters);

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
