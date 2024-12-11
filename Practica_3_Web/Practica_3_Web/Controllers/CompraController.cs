using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Reflection;
using System.Text.Json;
using Practica_3_Web.Models;
using System.Net.Http.Headers;

namespace Practica_3_Web.Controllers
{
    public class CompraController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;

        public CompraController(IHttpClientFactory http, IConfiguration conf)
        {
            _http = http;
            _conf = conf;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Consulta()
        {
            ObtenerCompras();
            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            ObtenerCompras();
            return View();
        }

        [HttpPost]
        public IActionResult Registro(AbonoModel model) 
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Compra/Registrar";

                JsonContent datos = JsonContent.Create(model);

                var response = client.PostAsync(url, datos).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                    if (result != null && result.Codigo == 0)
                    {
                        return RedirectToAction("Consulta", "Compra");
                    }
                    else
                    {
                        ObtenerCompras();
                        ViewBag.Mensaje = result!.Mensaje;
                        return View();
                    }
                }
            }
            return View();
        }

        private void ObtenerCompras()
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Compra/Consultar";

                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    ViewBag.ListaCompras = JsonSerializer.Deserialize<List<CompraModel>>((JsonElement)result.Contenido!);
                    ViewBag.ListaMiedo = new List<CompraModel>();
                }
            }
        }
    }
}
