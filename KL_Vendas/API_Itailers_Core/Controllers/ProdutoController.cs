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
    public class ProdutoController : ControllerBase
    {
        private readonly string _connectionString;
        public ProdutoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/<ProdutoController>
        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from t_produto_kl";
                var produtos = await con.QueryAsync<t_produto_kl>(query);

                return produtos.Any() ? Ok(produtos) : NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ProdutoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto(int id)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var parameters = new { id_produto_kl = id };

                con.Open();
                var query = "Select * from t_produto_kl where id_produto_kl = @id_produto_kl";
                var produto = await con.QueryFirstOrDefaultAsync<t_produto_kl>(query, parameters);

                return produto != null ? Ok(produto) : NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public async Task<IActionResult> Post(ProdutoRequest model)
        {
            var produto = new t_produto_kl 
            {
                id_produto_kl = model.id_produto_kl,
                nm_produto_kl = model.nm_produto_kl,
                cd_produto_kl = model.cd_produto_kl,
                nm_urn = model.nm_urn,
                qtd_licencas = model.qtd_licencas,
                id_combo = model.id_combo,
                imagem_produto = model.imagem_produto,
                descricao = model.descricao
            };

            var parameters = new
            {
                produto.id_produto_kl,
                produto.nm_produto_kl,
                produto.cd_produto_kl,
                produto.nm_urn,
                produto.qtd_licencas,
                produto.id_combo,
                produto.imagem_produto,
                produto.descricao
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"insert into t_produto_kl output INSERTED.id_produto_kl values (@id_produto_kl, @nm_produto_kl, @cd_produto_kl, @nm_urn, @qtd_licencas,
                                @id_combo, @imagem_produto, @descricao)";
                var id = await con.QuerySingleAsync<int>(sql, parameters);

                produto.id_produto_kl = id;

                return id > 0 ? Ok(produto) : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<ProdutoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProdutoRequest model)
        {
            var produto = new t_produto_kl
            {
                id_produto_kl = model.id_produto_kl,
                nm_produto_kl = model.nm_produto_kl,
                cd_produto_kl = model.cd_produto_kl,
                nm_urn = model.nm_urn,
                qtd_licencas = model.qtd_licencas,
                id_combo = model.id_combo,
                imagem_produto = model.imagem_produto,
                descricao = model.descricao
            };

            var parameters = new
            {
                produto.id_produto_kl,
                produto.nm_produto_kl,
                produto.cd_produto_kl,
                produto.nm_urn,
                produto.qtd_licencas,
                produto.id_combo,
                produto.imagem_produto,
                produto.descricao
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"update t_produto_kl set id_produto_kl = @id_produto_kl, nm_produto_kl = @nm_produto_kl, cd_produto_kl = @cd_produto_kl, 
                                nm_urn = @nm_urn, qtd_licencas = @qtd_licencas, id_combo = @id_combo, imagem_produto = @imagem_produto, 
                                descricao = @descricao where id_produto_kl = @id_produto_kl";
                var rowsAffected = await con.ExecuteAsync(sql, parameters);

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<ProdutoController>/5
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
                var sql = "delete from t_produto_kl where id_produto_kl = @id";
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
