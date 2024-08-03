using creditcard.application.Features.InfoClientes.Queries;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.TarjetasResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.InfoClientehandler.Queries
{
    public class GetTarjetasFromClienteHandler : IRequestHandler<TarjetasQuery, ObjectResponse<TarjetaResponse>>
    {
        private readonly IInfoClientesUseCases _infoClientesUse;

        public GetTarjetasFromClienteHandler(IInfoClientesUseCases infoClientesUse) => _infoClientesUse = infoClientesUse;
        public async Task<ObjectResponse<TarjetaResponse>> Handle(TarjetasQuery request, CancellationToken cancellationToken)
        =>await _infoClientesUse.GetTarjetaFromCliente(request);
    }
}
