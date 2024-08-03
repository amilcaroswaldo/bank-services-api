using creditcard.application.Features.InfoClientes.Queries;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.Pagos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.InfoClientehandler.Queries
{
    public class GetPagosFromTarjetaHandler : IRequestHandler<AllPagosQuery, ListResponse<AllpagosResponse>>
    {
        private readonly IInfoClientesUseCases _infoClientesUse;

        public GetPagosFromTarjetaHandler(IInfoClientesUseCases infoClientesUse) => _infoClientesUse = infoClientesUse;
        public async Task<ListResponse<AllpagosResponse>> Handle(AllPagosQuery request, CancellationToken cancellationToken)
        =>await _infoClientesUse.GetPagosFromTarjeta(request);
    }
}
