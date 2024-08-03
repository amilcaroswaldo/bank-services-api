using creditcard.application.Features.InfoClientes.Queries;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.ClientesResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.InfoClientehandler.Queries
{
    public class AllClienteHandler : IRequestHandler<ClienteQuery, ListResponse<ClienteResponse>>
    {
        private readonly IInfoClientesUseCases _infoClientesUse;

        public AllClienteHandler(IInfoClientesUseCases infoClientesUse) => _infoClientesUse = infoClientesUse;

        public Task<ListResponse<ClienteResponse>> Handle(ClienteQuery request, CancellationToken cancellationToken)
        => _infoClientesUse.AllClientes(request);
    }
}
