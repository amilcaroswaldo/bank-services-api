using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Domain.TarjetasResponse
{
    public class TarjetaResponse
    {
        public string NumeroTarjeta { get; set; }
        public int ClienteId { get; set; }
        public double limiteCredito { get; set; }
        public double SaldoActual { get; set; }
        public double SaldoDisp { get; set; }
        public string FchApertura { get; set; }
        public string FchaVenc { get; set; }
        public string FchCorteIni { get; set; }
        public string FchCorteFin { get; set; }
        public string DescBeneficios { get; set; }
    }
}
