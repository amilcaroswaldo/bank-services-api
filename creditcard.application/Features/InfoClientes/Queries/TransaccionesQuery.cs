using creditcard.Domain.Base;
using creditcard.Domain.Transacciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.InfoClientes.Queries
{
    public class TransaccionesQuery : IRequest<ListResponse<TransaccionesResponse>>
    {
        public string NumeroTarjeta { get; set; }
        public string FchInicio { get; set; }
        public string FchFin { get; set; }
    }
}
