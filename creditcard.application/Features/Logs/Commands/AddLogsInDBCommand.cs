using creditcard.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.Logs.Commands
{
    public class AddLogsInDBCommand : IRequest<GenericResponse> 
    {
        public string ErrorMessage { get; set; }
        public int ErrorNumber { get; set; }
        public string OriginatingComponent { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
