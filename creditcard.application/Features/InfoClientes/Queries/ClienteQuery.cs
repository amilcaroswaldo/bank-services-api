using creditcard.Domain.Base;
using creditcard.Domain.ClientesResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.InfoClientes.Queries
{
    public class ClienteQuery : IRequest<ListResponse<ClienteResponse>>
    {
    }
}
