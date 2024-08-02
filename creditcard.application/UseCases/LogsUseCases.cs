using creditcard.application.Features.Logs.Commands;
using creditcard.application.Interfaces;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases
{
    public class LogsUseCases : ILogsUseCases
    {
        private readonly ILogsCommand _command;

        public LogsUseCases(ILogsCommand command)
        {
            _command = command;
        }

        public async Task<GenericResponse> AddlogsInDB(AddLogsInDBCommand command)
        {
            var response = new GenericResponse();
            response = await _command.AddlogsInDB(command);
            return response;
        }
    }
}
