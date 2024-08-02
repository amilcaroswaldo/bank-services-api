using creditcard.application.Features.HealthCheck.Queries;
using creditcard.Domain.Base;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases.Interfaces
{
    public interface IhealthCheckUseCase
    {
        Task<ObjectResponse<HealthCheckResult>> CheckHealthDBAsync();
        Task<ObjectResponse<HealthCheckResult>> CheckHealthApiAsync(HealthCheckApiQuery query);
    }
}
