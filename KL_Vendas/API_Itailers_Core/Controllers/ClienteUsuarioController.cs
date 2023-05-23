using API_Itailers_Core.Entities;
using API_Itailers_Core.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Objetos;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Itailers_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteUsuarioController : ControllerBase
    {
        private readonly string _connectionString;
        public ClienteUsuarioController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/<ClienteUsuarioController>
        [HttpGet]
        public async Task<IActionResult> GetClienteUsuarios()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from t_cliente_usuario";
                var clienteUsuarios = await con.QueryAsync<t_cliente_usuario>(query);

                return clienteUsuarios.Any() ? Ok(clienteUsuarios) : NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ClienteUsuarioController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteUsuario(int id)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var parameters = new { id_cliente_usuario = id };

                con.Open();
                var query = "Select * from t_cliente_usuario where id_cliente_usuario = @id_cliente_usuario";
                var clienteUsuario = await con.QueryFirstOrDefaultAsync<t_cliente_usuario>(query, parameters);

                return clienteUsuario != null ? Ok(clienteUsuario) : NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<ClienteUsuarioController>
        [HttpPost]
        public async Task<IActionResult> Post(ClienteUsuarioRequest model)
        {
            var cliente_usuario = new t_cliente_usuario
            {
                id_cliente = model.id_cliente,
                nm_user_id = model.nm_user_id,
                nm_transaction_id = model.nm_transaction_id,
                nm_email = model.nm_email,
                dt_start = model.dt_start,
                dt_end = model.dt_end,
                nm_user_document = model.nm_user_document,
                nm_user_plan = model.nm_user_plan,
                id_status = model.id_status
            };

            var parameters = new
            {
                cliente_usuario.id_cliente,
                cliente_usuario.nm_user_id,
                cliente_usuario.nm_transaction_id,
                cliente_usuario.nm_email,
                cliente_usuario.dt_start,
                cliente_usuario.dt_end,
                cliente_usuario.nm_user_document,
                cliente_usuario.nm_user_plan,
                cliente_usuario.id_status
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"insert into t_cliente_usuario output INSERTED.id_cliente_usuario values (@id_cliente, @nm_user_id, @nm_transaction_id, @nm_email,
                            @dt_start, @dt_end, @nm_user_document, @nm_user_plan, @id_status)";
                var id = await con.QuerySingleAsync<int>(sql, parameters);

                cliente_usuario.id_cliente_usuario = id;

                return id > 0 ? Ok(cliente_usuario) : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<ClienteUsuarioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteUsuarioRequest model)
        {
            var cliente_usuario = new t_cliente_usuario
            {
                id_cliente = model.id_cliente,
                nm_user_id = model.nm_user_id,
                nm_transaction_id = model.nm_transaction_id,
                nm_email = model.nm_email,
                dt_start = model.dt_start,
                dt_end = model.dt_end,
                nm_user_document = model.nm_user_document,
                nm_user_plan = model.nm_user_plan,
                id_status = model.id_status
            };

            var parameters = new
            {
                id,
                cliente_usuario.id_cliente,
                cliente_usuario.nm_user_id,
                cliente_usuario.nm_transaction_id,
                cliente_usuario.nm_email,
                cliente_usuario.dt_start,
                cliente_usuario.dt_end,
                cliente_usuario.nm_user_document,
                cliente_usuario.nm_user_plan,
                cliente_usuario.id_status
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"update t_cliente_usuario set id_cliente = @id_cliente, nm_user_id = @nm_user_id, nm_transaction_id = @nm_transaction_id,
                                nm_email = @nm_email, dt_start = @dt_start, dt_end = @dt_end, nm_user_document = @nm_user_document, nm_user_plan = @nm_user_plan,
                                id_status = @id_status where id_cliente_usuario = @id";
                var rowsAffected = await con.ExecuteAsync(sql, parameters);

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<ClienteUsuarioController>/5
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
                var sql = "delete from t_cliente_usuario where id_cliente_usuario = @id";
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
