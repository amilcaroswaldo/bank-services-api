using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Domain.Pagos
{
    public class AllpagosResponse
    {
        public string NumeroTarjeta { get; set; }
        public double MontoPago { get; set; }
        public double MontoDisp { get; set; }
        public double MontoPagado { get; set; }
        public double MontoMora { get; set; }
        public string FchaPago { get; set; }
        public string FchCorteIniP { get; set; }
        public string FchCorteFinP { get; set; }
    }
}
