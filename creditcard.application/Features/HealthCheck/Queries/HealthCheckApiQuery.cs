using creditcard.Domain.Base;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.HealthCheck.Queries
{
    public class HealthCheckApiQuery : IRequest<ObjectResponse<HealthCheckResult>>
    {
        public string Url { get; set; }
    }
}
