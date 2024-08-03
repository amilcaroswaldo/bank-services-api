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
    public class CuotaMinimaHandler : IRequestHandler<CuotaMinimaQuery, ObjectResponse<CoutaMinimaResponse>>
    {
        private readonly IEstadoCuentaUseCases _estadoCuentaUseCases;

        public CuotaMinimaHandler(IEstadoCuentaUseCases estadoCuentaUseCases)=> _estadoCuentaUseCases = estadoCuentaUseCases;

        public async Task<ObjectResponse<CoutaMinimaResponse>> Handle(CuotaMinimaQuery request, CancellationToken cancellationToken)
        =>await _estadoCuentaUseCases.GetCuotaMinima(request);
    }
}
