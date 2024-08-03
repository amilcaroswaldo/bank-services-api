using creditcard.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.EstadoCuenta.Commands
{
    public class AddPagoCommand : IRequest<GenericResponse>
    {
        public string NumeroTarjeta { get; set; }
        public double Monto { get; set; }
    }
}
