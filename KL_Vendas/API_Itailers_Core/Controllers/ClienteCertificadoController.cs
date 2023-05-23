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
    public class ClienteCertificadoController : ControllerBase
    {
        private readonly string _connectionString;
        public ClienteCertificadoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/<ClienteCertificadoController>
        [HttpGet]
        public async Task<IActionResult> GetClienteCertificados()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from t_cliente_certificado";
                var clienteCertificados = await con.QueryAsync<t_cliente_certificado>(query);

                return clienteCertificados.Any() ? Ok(clienteCertificados) : NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ClienteCertificadoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteCertificado(int id)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var parameters = new { id_cliente_certificado = id };

                con.Open();
                var query = "Select * from t_cliente_certificado where id_cliente_certificado = @id_cliente_certificado";
                var clienteCertificado = await con.QueryFirstOrDefaultAsync<t_cliente_certificado>(query, parameters);

                return clienteCertificado != null ? Ok(clienteCertificado) : NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<ClienteCertificadoController>
        [HttpPost]
        public async Task<IActionResult> Post(ClienteCertificadoRequest model)
        {
            var clienteCertificado = new t_cliente_certificado
            {
                id_cliente_token = model.id_cliente_token,
                nm_thumbprint = model.nm_thumbprint,
                nm_usuario_certificado = model.nm_usuario_certificado,
                nm_senha_certificado = model.nm_senha_certificado
            };

            var parameters = new
            {
                clienteCertificado.id_cliente_token,
                clienteCertificado.nm_thumbprint,
                clienteCertificado.nm_usuario_certificado,
                clienteCertificado.nm_senha_certificado
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"insert into t_cliente_certificado output INSERTED.id_cliente_certificado 
                                    values (@id_cliente_token, @nm_thumbprint, @nm_usuario_certificado, @nm_senha_certificado)";
                var id = await con.QuerySingleAsync<int>(sql, parameters);

                clienteCertificado.id_cliente_certificado = id;

                return id > 0 ? Ok(clienteCertificado) : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<ClienteCertificadoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteCertificadoRequest model)
        {
            var clienteCertificado = new t_cliente_certificado
            {
                id_cliente_token = model.id_cliente_token,
                nm_thumbprint = model.nm_thumbprint,
                nm_usuario_certificado = model.nm_usuario_certificado,
                nm_senha_certificado = model.nm_senha_certificado
            };

            var parameters = new
            {
                id,
                clienteCertificado.id_cliente_token,
                clienteCertificado.nm_thumbprint,
                clienteCertificado.nm_usuario_certificado,
                clienteCertificado.nm_senha_certificado
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"update t_cliente_certificado set id_cliente_token = @id_cliente_token, nm_thumbprint = @nm_thumbprint,
                                    nm_usuario_certificado = @nm_usuario_certificado, nm_senha_certificado = @nm_senha_certificado where id_cliente_certificado = @id";
                await con.ExecuteAsync(sql, parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<ClienteCertificadoController>/5
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
                var query = "delete from t_cliente_certificado where id_cliente_certificado = @id";
                await con.ExecuteAsync(query, parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
