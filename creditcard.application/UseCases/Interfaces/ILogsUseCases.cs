using creditcard.application.Features.Logs.Commands;
using creditcard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases.Interfaces
{
    public interface ILogsUseCases
    {
        Task<GenericResponse> AddlogsInDB(AddLogsInDBCommand command);
    }
}
