using creditcard.application.Features.Logs.Commands;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.LogsHandler.Commands
{
    public class AddLogsInDBHandler : IRequestHandler<AddLogsInDBCommand, GenericResponse>
    {
        private readonly ILogsUseCases _useCases;

        public AddLogsInDBHandler(ILogsUseCases useCases)
        {
            _useCases = useCases;
        }

        public async Task<GenericResponse> Handle(AddLogsInDBCommand request, CancellationToken cancellationToken)
        => await _useCases.AddlogsInDB(request);
    }
}
