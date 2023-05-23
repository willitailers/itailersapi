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
    public class VendorThemeController : ControllerBase
    {
        private readonly string _connectionString;
        public VendorThemeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/<VendorThemeController>
        [HttpGet]
        public async Task<IActionResult> GetVendorThemes()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from t_vendor_theme";
                var vendorThemes = await con.QueryAsync<t_vendor_theme>(query);

                return vendorThemes.Any() ? Ok(vendorThemes) : NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<VendorThemeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorTheme(int id)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var parameters = new { id_vendor_theme = id };

                con.Open();
                var query = "Select * from t_vendor_theme where id_vendor_theme = @id_vendor_theme";
                var vendorTheme = await con.QueryFirstOrDefaultAsync<t_vendor_theme>(query, parameters);

                return vendorTheme != null ? Ok(vendorTheme) : NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<VendorThemeController>
        [HttpPost]
        public async Task<IActionResult> Post(VendorThemeRequest model)
        {
            var vendorTheme = new t_vendor_theme 
            {
                bannerImage = model.bannerImage,
                bannerImageMobile = model.bannerImageMobile,
                id_cliente = model.id_cliente, 
                isDarkTheme = model.isDarkTheme,
                logoImage = model.logoImage,
                primaryColor = model.primaryColor,
                secondaryColor = model.secondaryColor,
                vendorDomainName = model.vendorDomainName,
                vendorTitleImage = model.vendorTitleImage
            };

            var parameters = new
            {
                vendorTheme.bannerImage,
                vendorTheme.bannerImageMobile,
                vendorTheme.id_cliente,
                vendorTheme.isDarkTheme,
                vendorTheme.logoImage,
                vendorTheme.primaryColor,
                vendorTheme.secondaryColor,
                vendorTheme.vendorDomainName,
                vendorTheme.vendorTitleImage
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"insert into t_vendor_theme output INSERTED.id_vendor_theme values (@id_cliente, @vendorDomainName, @primaryColor, @secondaryColor, @logoImage,
                                @vendorTitleImage, @bannerImage, @isDarkTheme, @bannerImageMobile)";
                var id = await con.QuerySingleAsync<int>(sql, parameters);

                vendorTheme.id_vendor_theme = id;

                return id > 0 ? Ok(vendorTheme) : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<VendorThemeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, VendorThemeRequest model)
        {
            var vendorTheme = new t_vendor_theme
            {
                bannerImage = model.bannerImage,
                bannerImageMobile = model.bannerImageMobile,
                id_cliente = model.id_cliente,
                isDarkTheme = model.isDarkTheme,
                logoImage = model.logoImage,
                primaryColor = model.primaryColor,
                secondaryColor = model.secondaryColor,
                vendorDomainName = model.vendorDomainName,
                vendorTitleImage = model.vendorTitleImage
            };

            var parameters = new
            {
                id,
                vendorTheme.bannerImage,
                vendorTheme.bannerImageMobile,
                vendorTheme.id_cliente,
                vendorTheme.isDarkTheme,
                vendorTheme.logoImage,
                vendorTheme.primaryColor,
                vendorTheme.secondaryColor,
                vendorTheme.vendorDomainName,
                vendorTheme.vendorTitleImage
            };

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var sql = @"update t_vendor_theme set id_cliente = @id_cliente, vendorDomainName = @vendorDomainName, primaryColor = @primaryColor, 
                                secondaryColor = @secondaryColor, logoImage = @logoImage, vendorTitleImage = @vendorTitleImage, bannerImage = @bannerImage, 
                                isDarkTheme = @isDarkTheme, bannerImageMobile = @bannerImageMobile where id_vendor_theme = @id";
                var rowsAffected = await con.ExecuteAsync(sql, parameters);

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<VendorThemeController>/5
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
                var sql = "delete from t_vendor_theme where id_vendor_theme = @id";
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
