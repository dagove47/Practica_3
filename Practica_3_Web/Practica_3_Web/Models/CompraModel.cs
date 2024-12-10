using System.ComponentModel.DataAnnotations;

namespace Practica_3_Web.Models
{
    public class CompraModel
    {
        public long Id_Compra { get; set; }
        public decimal Precio { get; set; }
        public decimal Saldo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
