using Practica_3.baseDatos;
using Practica_3.Entities;
using Practica_3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Practica_3.Controllers
{
    public class HomeController : Controller
    {
        RegistroModel RegistroM = new RegistroModel();
        ProductoModel ProductoM = new ProductoModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Consulta()
        {
            ViewBag.msj = "";
            return View();
        }

        [HttpPost]
        public ActionResult Consulta(Consulta entity)
        {
            ViewBag.msj = "";
            return View();
        }
        [HttpGet]
        public ActionResult Registro()
        {
            // Obtener la lista de productos pendientes
            var productos = ProductoM.ObtenerProductosPendientes();

            // Convertir los productos a SelectListItem
            var lstProductos = productos.Select(item => new SelectListItem
            {
                Value = item.Id_Compra.ToString(),
                Text = item.Descripcion
            }).ToList();

            ViewBag.ComprasPendientes = new SelectList(lstProductos, "Value", "Text");
            return View();
        }


        [HttpPost]
        public ActionResult Registro(Registro registro)
        {
            var respuesta = RegistroM.Registrar(registro);

            if (respuesta)
            {
                ViewBag.msj = "Su información se ha registrado con éxito!";
                return RedirectToAction("Consulta"); // Redirigir a Consulta
            }
            else
            {
                ViewBag.msj = "Error! Su información no se ha registrado";
                return View();
            }
        }

        [HttpGet]
        public JsonResult ConsultarSaldo(long id)
        {
            var saldo = RegistroM.ObtenerSaldoanterior(id);
            return Json(new { saldo = saldo }, JsonRequestBehavior.AllowGet);
        }
    }
}
