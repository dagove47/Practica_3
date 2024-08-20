using Practica_3.baseDatos;  // Asegúrate de usar el espacio de nombres correcto
using Practica_3.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Practica_3.Models
{
    public class ProductoModel
    {
        public List<ObtenerComprasPendientes_Result> ObtenerProductosPendientes()
        {
            using (var context = new PracticaS13Entities())
            {
                // Ejecutar el procedimiento almacenado y retornar los resultados
                return context.ObtenerComprasPendientes().ToList();
            }
        }

        public List<Consulta> consultarCompras() {
            using (var context = new PracticaS13Entities())
            {
                var result = context.ConsultarCompras().Select(v => new Consulta
                {
                    Id_Compra = v.Id_Compra,
                    Descripcion = v.Descripcion,
                    Precio = v.Precio,
                    Saldo = v.Saldo,
                    Estado = v.Estado
                }).ToList();
                // Ejecutar el procedimiento almacenado y retornar los resultados
                return result;
            }
        }

    }
}
