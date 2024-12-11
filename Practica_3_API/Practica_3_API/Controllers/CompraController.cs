using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Practica_3_API.Model;
using Microsoft.Extensions.Configuration;

namespace Practica_3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly IConfiguration _conf;
        private readonly IHostEnvironment _env;

        public CompraController(IConfiguration conf, IHostEnvironment env)
        {
            _conf = conf;
            _env = env;
        }


        [HttpGet]
        [Route("Consultar")]
        public IActionResult Consultar()
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<CompraModel>("sp_ConsultarProductos", new { });

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay compras registradas en este momento";
                }

                return Ok(respuesta);
            }
        }


        [HttpPost]
        [Route("Registrar")]
        public IActionResult Registrar(AbonoModel model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                try
                {
                    var result = context.Execute("sp_RegistrarAbono", new { model.Id_Compra, model.Monto });

                    if (result > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Mensaje = "Abono registrado correctamente";
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Mensaje = "Error: El abono no se pudo registrar correctamente.";
                    }
                }
                catch (SqlException ex)
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Error: " + ex.Message;
                }

                return Ok(respuesta);
            }
        }

        [HttpGet]
        [Route("ConsultarAbonos/{idCompra}")]
        public async Task<IActionResult> ConsultarAbonos(long idCompra)
        {
            using (var connection = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
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
    }
}
