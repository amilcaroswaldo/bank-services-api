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
    public class AddTransaccionHandler : IRequestHandler<AddTransaccionCommand, GenericResponse>
    {
        private readonly IEstadoCuentaUseCases _estadoCuentaUseCases;

        public AddTransaccionHandler(IEstadoCuentaUseCases estadoCuentaUseCases) => _estadoCuentaUseCases = estadoCuentaUseCases;
        public async Task<GenericResponse> Handle(AddTransaccionCommand request, CancellationToken cancellationToken)
        => await _estadoCuentaUseCases.AddTransaccion(request);
    }
}
