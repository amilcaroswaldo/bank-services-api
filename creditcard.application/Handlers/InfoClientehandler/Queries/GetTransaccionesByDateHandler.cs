using creditcard.application.Features.InfoClientes.Queries;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.Transacciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.InfoClientehandler.Queries
{
    public class GetTransaccionesByDateHandler : IRequestHandler<TransaccionesQuery, ListResponse<TransaccionesResponse>>
    {
        private readonly IInfoClientesUseCases _infoClientesUse;

        public GetTransaccionesByDateHandler(IInfoClientesUseCases infoClientesUse) => _infoClientesUse = infoClientesUse;
        public Task<ListResponse<TransaccionesResponse>> Handle(TransaccionesQuery request, CancellationToken cancellationToken)
        => _infoClientesUse.GetTransaccionesByDate(request);
    }
}
