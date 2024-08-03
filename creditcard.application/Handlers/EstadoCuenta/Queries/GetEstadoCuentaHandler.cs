using creditcard.application.Features.EstadoCuenta.Queries;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.EstadoCuentaResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.EstadoCuenta.Queries
{
    public class GetEstadoCuentaHandler : IRequestHandler<EstadoCuentaQuery, ObjectResponse<EstadoCuentaRespons>>
    {
        private readonly IEstadoCuentaUseCases _estadoCuentaUseCases;

        public GetEstadoCuentaHandler(IEstadoCuentaUseCases estadoCuentaUseCases) => _estadoCuentaUseCases = estadoCuentaUseCases;
        public Task<ObjectResponse<EstadoCuentaRespons>> Handle(EstadoCuentaQuery request, CancellationToken cancellationToken)
        => _estadoCuentaUseCases.EstadoCuenta(request);

    }
}
