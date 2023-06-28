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
    public class ClienteLicencaController : ControllerBase
    {
        private readonly string _connectionString;
        public ClienteLicencaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/<ClienteLicencaController>
        [HttpGet]
        public async Task<IActionResult> GetClienteLicencas()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from t_cliente_licenca";
                var clienteLicencas = await con.QueryAsync<t_cliente_licenca>(query);

                foreach (var item in clienteLicencas)
                {
                    item.cd_ativacao_kl = "******-******-******-******";
                    item.nm_ativacao_mac = "******-******-******-******";
                    item.nm_ativacao_android = "******-******-******-******";
                    item.nm_ativacao_iphone = "******-******-******-******";
                    item.nm_ativacao_windows = "******-******-******-******";
                }

                return clienteLicencas.Any() ? 
                    Ok(clienteLicencas) : 
                    NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ClienteLicencaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteLicenca(int id)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var parameters = new { id_cliente_licenca = id };

                con.Open();
                var query = "Select * from t_cliente_licenca where id_cliente_licenca = @id_cliente_licenca";
                var vendorTheme = await con.QueryFirstOrDefaultAsync<t_cliente_licenca>(query, parameters);

                return vendorTheme != null ? Ok(vendorTheme) : NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<ClienteLicencaController>
        [HttpPost]
        public async Task<IActionResult> Post(ClienteLicencaRequest model)
        {
            var cliente_licenca = new t_cliente_licenca 
            {
                cd_ativacao_kl = model.cd_ativacao_kl,
                dt_ativacao = model.dt_ativacao,
                dt_cancelamento = model.dt_cancelamento,
                dt_expiracao = model.dt_expiracao,
                id_cliente_usuario = model.id_cliente_usuario,
                id_produto = model.id_produto,
                id_status = model.id_status,
                nm_ativacao_android = model.nm_ativacao_android,
                nm_ativacao_iphone = model.nm_ativacao_iphone,
                nm_ativacao_mac = model.nm_ativacao_mac,
                nm_ativacao_windows = model.nm_ativacao_windows,
                nm_subscriber_id = model.nm_subscriber_id
            };

            var parameters = new
            {
                cliente_licenca.cd_ativacao_kl,
                cliente_licenca.dt_ativacao,
                cliente_licenca.dt_cancelamento,
                cliente_licenca.dt_expiracao,
                cliente_licenca.id_cliente_usuario,
                cliente_licenca.id_produto,
                cliente_licenca.id_status,
                cliente_licenca.nm_ativacao_android,
                cliente_licenca.nm_ativacao_iphone,
                cliente_licenca.nm_ativacao_mac,
                cliente_licenca.nm_ativacao_windows,
                cliente_licenca.nm_subscriber_id
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"insert into t_cliente_licenca output INSERTED.id_cliente_licenca values (@id_cliente_usuario, @id_produto, @cd_ativacao_kl, @nm_subscriber_id, 
                                @nm_ativacao_android, @nm_ativacao_iphone, @nm_ativacao_windows, @nm_ativacao_mac, @dt_ativacao, @dt_expiracao, 
                                @dt_cancelamento, @id_status)";
                var id = await con.QuerySingleAsync<int>(sql, parameters);

                cliente_licenca.id_cliente_licenca = id;

                return id > 0 ? Ok(cliente_licenca) : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<ClienteLicencaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteLicencaRequest model)
        {
            var cliente_licenca = new t_cliente_licenca
            {
                cd_ativacao_kl = model.cd_ativacao_kl,
                dt_ativacao = model.dt_ativacao,
                dt_cancelamento = model.dt_cancelamento,
                dt_expiracao = model.dt_expiracao,
                id_cliente_usuario = model.id_cliente_usuario,
                id_produto = model.id_produto,
                id_status = model.id_status,
                nm_ativacao_android = model.nm_ativacao_android,
                nm_ativacao_iphone = model.nm_ativacao_iphone,
                nm_ativacao_mac = model.nm_ativacao_mac,
                nm_ativacao_windows = model.nm_ativacao_windows,
                nm_subscriber_id = model.nm_subscriber_id
            };

            var parameters = new
            {
                id,
                cliente_licenca.cd_ativacao_kl,
                cliente_licenca.dt_ativacao,
                cliente_licenca.dt_cancelamento,
                cliente_licenca.dt_expiracao,
                cliente_licenca.id_cliente_usuario,
                cliente_licenca.id_produto,
                cliente_licenca.id_status,
                cliente_licenca.nm_ativacao_android,
                cliente_licenca.nm_ativacao_iphone,
                cliente_licenca.nm_ativacao_mac,
                cliente_licenca.nm_ativacao_windows,
                cliente_licenca.nm_subscriber_id
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"update t_cliente_licenca set id_cliente_usuario = @id_cliente_usuario, id_produto = @id_produto, cd_ativacao_kl = @cd_ativacao_kl, 
                                nm_subscriber_id = @nm_subscriber_id, nm_ativacao_android = @nm_ativacao_android, nm_ativacao_iphone = @nm_ativacao_iphone, 
                                nm_ativacao_windows = @nm_ativacao_windows, nm_ativacao_mac = @nm_ativacao_mac, dt_ativacao = @dt_ativacao,
                                dt_expiracao = @dt_expiracao, dt_cancelamento = @dt_cancelamento, id_status = @id_status where id_cliente_licenca = @id";
                var rowsAffected = await con.ExecuteAsync(sql, parameters);

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<ClienteLicencaController>/5
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
                var sql = "delete from t_cliente_licenca where id_cliente_licenca = @id";
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
