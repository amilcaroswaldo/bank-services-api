using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Domain.Transacciones
{
    public class TransaccionesResponse
    {
        public string NumeroTarjeta { get; set; }
        public double MontoTransaccion { get; set; }
        public string FchaTransaccion { get; set; }
        public string Descripcion { get; set; }
        public string TipoTransaccion { get; set; }
        public string Categoria { get; set; }      
    }
}
