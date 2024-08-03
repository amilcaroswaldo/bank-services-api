using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Domain.EstadoCuentaResponse
{
    public class EstadoCuentaRespons
    {
        public double SaldoActual { get; set; }
        public double SaldoDisponible { get; set; }
        public double InteresBonificable { get; set; }
        public double CuotaMinima { get; set; }
        public double MontoContadoConIntereses { get; set; }
        public double MontoTotal { get; set; }
    }
}
