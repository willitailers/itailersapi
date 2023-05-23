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
    public class ClienteProdutoCertificadoController : ControllerBase
    {
        private readonly string _connectionString;
        public ClienteProdutoCertificadoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/<ClienteProdutoCertificadoController>
        [HttpGet]
        public async Task<IActionResult> GetClienteProdutoCertificados()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from t_cliente_produto_certificado";
                var clienteProdutoCertificados = await con.QueryAsync<t_cliente_produto_certificado>(query);

                return clienteProdutoCertificados.Any() ? Ok(clienteProdutoCertificados) : NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ClienteProdutoCertificadoController>/details?id_cliente=&id_produto_kl=&id_cliente_certificado=&dv_ativa_cadastro=
        [HttpGet("details")]
        public async Task<IActionResult> GetClienteProdutoCertificado(int id_cliente, int id_produto_kl, int id_cliente_certificado, int dv_ativa_cadastro)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();

                var builder = new SqlBuilder();
                var selector = builder.AddTemplate("select * from t_cliente_produto_certificado /**where**/");

                if (id_cliente > 0)
                    builder.Where("id_cliente = @id_cliente", new { id_cliente });

                if (id_produto_kl > 0)
                    builder.Where("id_produto_kl = @id_produto_kl", new { id_produto_kl });

                if (id_cliente_certificado > 0)
                    builder.Where("id_cliente_certificado = @id_cliente_certificado", new { id_cliente_certificado });

                if (dv_ativa_cadastro > 0)
                    builder.Where("dv_ativa_cadastro = @dv_ativa_cadastro", new { dv_ativa_cadastro });

                var clienteProdutoCertificado = await con.QueryAsync<t_cliente_produto_certificado>(selector.RawSql, selector.Parameters);

                return clienteProdutoCertificado.Any() ? Ok(clienteProdutoCertificado) : NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<ClienteProdutoCertificadoController>
        [HttpPost]
        public async Task<IActionResult> Post(ClienteProdutoCertificadoRequest model)
        {
            var clienteProdutoCertificado = new t_cliente_produto_certificado 
            {
                dv_ativa_cadastro = model.dv_ativa_cadastro,
                id_cliente = model.id_cliente,
                id_cliente_certificado = model.id_cliente_certificado,
                id_produto_kl = model.id_produto_kl
            };

            var parameters = new
            {
                clienteProdutoCertificado.dv_ativa_cadastro,
                clienteProdutoCertificado.id_cliente,
                clienteProdutoCertificado.id_cliente_certificado,
                clienteProdutoCertificado.id_produto_kl
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"insert into t_cliente_produto_certificado values (@id_cliente, @id_produto_kl, @id_cliente_certificado, @dv_ativa_cadastro)";
                var rowsAffected = await con.ExecuteAsync(sql, parameters);

                return rowsAffected > 0 ? Ok(clienteProdutoCertificado) : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //// PUT api/<ClienteProdutoCertificadoController>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, ProdutoRequest model)
        //{
        //    
        //}

        // DELETE api/<ClienteProdutoCertificadoController>/5
        [HttpDelete("{id_cliente}/{id_produto_kl}/{id_cliente_certificado}/{dv_ativa_cadastro}")]
        public async Task<IActionResult> Delete(int id_cliente, int id_produto_kl, int id_cliente_certificado, int dv_ativa_cadastro)
        {
            var parameters = new
            {
                id_cliente,
                id_produto_kl,
                id_cliente_certificado,
                dv_ativa_cadastro
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"delete from t_cliente_produto_certificado where id_cliente = @id_cliente 
                            and id_produto_kl = @id_produto_kl 
                            and id_cliente_certificado = @id_cliente_certificado
                            and dv_ativa_cadastro = @dv_ativa_cadastro";
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
