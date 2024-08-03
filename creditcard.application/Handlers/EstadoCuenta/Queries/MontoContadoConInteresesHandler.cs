using creditcard.application.Features.EstadoCuenta.Queries;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.FuncionesResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.EstadoCuenta.Queries
{
    public class MontoContadoConInteresesHandler : IRequestHandler<MontoContadoConInteresesQuery, ObjectResponse<MontoContadoConInteresesResponse>>
    {
        private readonly IEstadoCuentaUseCases _estadoCuentaUseCases;

        public MontoContadoConInteresesHandler(IEstadoCuentaUseCases estadoCuentaUseCases) => _estadoCuentaUseCases = estadoCuentaUseCases;

        public async Task<ObjectResponse<MontoContadoConInteresesResponse>> Handle(MontoContadoConInteresesQuery request, CancellationToken cancellationToken)
        => await _estadoCuentaUseCases.GetContadoConIntereses(request);
    }
}
