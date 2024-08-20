using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica_3.Entities
{
    public class Producto
    {
        public int Id_Compra { get; set; }
        public string Descripcion { get; set; }
        public Decimal Saldo { get; set; }

        public String Estado { get; set; }



    }
}