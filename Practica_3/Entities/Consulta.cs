using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica_3.Entities
{
    public class Consulta
    {
        public long Id_Compra { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public Decimal Saldo { get; set; }
        public string Estado { get; set; }
    }
}
