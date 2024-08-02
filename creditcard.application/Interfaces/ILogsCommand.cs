using creditcard.application.Features.Logs.Commands;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Interfaces
{
    public interface ILogsCommand
    {
        Task<GenericResponse> AddlogsInDB(AddLogsInDBCommand command);
    }
}
