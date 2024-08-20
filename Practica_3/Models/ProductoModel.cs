using Practica_3.baseDatos;  // Asegúrate de usar el espacio de nombres correcto
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
    }
}
