using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Practica_3_API.Model;

namespace Practica_3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly string _connectionString;
        public CompraController(IConfiguration configuration)
        {
            // Inicializar _connectionString con la cadena de conexión del archivo appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration), "La cadena de conexión no puede ser nula");
        }


        [HttpGet("ConsultarProductos")]
        public async Task<IActionResult> ConsultarProductos()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var productos = await connection.QueryAsync<dynamic>("sp_ConsultarProductos", commandType: CommandType.StoredProcedure);
                    return Ok(productos);
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error en la consulta: {ex.Message}");
            }
        }



        [HttpGet("ConsultarAbonos/{idCompra}")]
        public async Task<IActionResult> ConsultarAbonos(long idCompra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id_Compra", idCompra);

                try
                {
                    var abonos = await connection.QueryAsync<dynamic>("sp_ConsultarAbonosPorCompra", parameters, commandType: CommandType.StoredProcedure);
                    return Ok(abonos);
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, $"Error al consultar abonos: {ex.Message}");
                }
            }
        }


        [HttpPost("RegistrarAbono")]
        public async Task<IActionResult> RegistrarAbono([FromBody] AbonoModel abono)
        {
            // Validar que el abono no sea nulo y que el monto sea positivo
            if (abono == null || abono.Monto <= 0)
            {
                return BadRequest("Datos del abono no válidos.");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id_Compra", abono.Id_Compra);  // ID de la compra
                parameters.Add("@Monto", abono.Monto);          // Monto del abono

                try
                {
                    // Llamar al procedimiento almacenado utilizando Dapper
                    await connection.ExecuteAsync("sp_RegistrarAbono", parameters, commandType: CommandType.StoredProcedure);
                    return Ok("Abono registrado exitosamente.");
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, $"Error al registrar el abono: {ex.Message}");
                }
            }
        }
    }
}
