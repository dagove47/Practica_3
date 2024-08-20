using Practica_3.baseDatos;
using Practica_3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica_3.Models
{
    public class RegistroModel
    {
        public bool Registrar(Registro registro)
        {
            var rowsAffected = 0;


            using (var context = new PracticaS13Entities())
            {
                rowsAffected = context.RegistrarAbono(registro.Id_Compra, registro.Abono);
            }

            return (rowsAffected > 0 ? true : false);
        }



        public decimal ObtenerSaldoanterior(long idCompra)
        {
            using (var context = new PracticaS13Entities())
            {
                // Asegúrate de que ObtenerSaldoCompra devuelva un IQueryable<decimal> o similar
                var saldo = context.ObtenerSaldoCompra(idCompra).FirstOrDefault();

                // Retorna el saldo o 0 si es null
                return saldo ?? 0;
            }
        }

    }
}


