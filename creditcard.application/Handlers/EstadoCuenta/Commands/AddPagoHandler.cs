using creditcard.application.Features.EstadoCuenta.Commands;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.EstadoCuenta.Commands
{
    public class AddPagoHandler : IRequestHandler<AddPagoCommand, GenericResponse>
    {
        private readonly IEstadoCuentaUseCases _estadoCuentaUseCases;

        public AddPagoHandler(IEstadoCuentaUseCases estadoCuentaUseCases) => _estadoCuentaUseCases = estadoCuentaUseCases;
        public async Task<GenericResponse> Handle(AddPagoCommand request, CancellationToken cancellationToken)
        => await _estadoCuentaUseCases.Addpago(request);
    }
}
