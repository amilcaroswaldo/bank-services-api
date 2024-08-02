using creditcard.Domain.Base;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.IHealthCheck.Queries
{
    public class HealthCheckDatabaseQuery : IRequest<ObjectResponse<HealthCheckResult>>
    {
    }
}
