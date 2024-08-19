using Practica_3.Entities;
using Practica_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica_3.Controllers
{
    public class HomeController : Controller
    {
        HomeModel homeM = new HomeModel();



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
            ViewBag.msj = "";
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Registro entity)
        {
            var respuesta = homeM.Registrar(entity);

            if (respuesta) {
                ViewBag.msj = "Su información se ha registrado con exito!";
                return Consulta();
            }
            else {
                ViewBag.msj = "Error! Su información no se ha registrado";
                return View();
            }
        }
    }
}