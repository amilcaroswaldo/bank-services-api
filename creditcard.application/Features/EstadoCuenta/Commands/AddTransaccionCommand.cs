using creditcard.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.EstadoCuenta.Commands
{
    public class AddTransaccionCommand : IRequest<GenericResponse>
    {
        public string NumeroTarjeta { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public string TipoTransaccion { get; set; }
        public string Categoria { get; set; }
    }
}
