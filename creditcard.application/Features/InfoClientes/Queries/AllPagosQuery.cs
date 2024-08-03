using creditcard.Domain.Base;
using creditcard.Domain.ClientesResponse;
using creditcard.Domain.Pagos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.InfoClientes.Queries
{
    public class AllPagosQuery : IRequest<ListResponse<AllpagosResponse>>
    {
        public string NumeroTarjeta { get; set; }
    }
}
