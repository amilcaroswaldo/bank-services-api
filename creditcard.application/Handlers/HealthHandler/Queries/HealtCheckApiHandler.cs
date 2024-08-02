using creditcard.application.Features.HealthCheck.Queries;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.HealthHandler.Queries
{
    public class HealtCheckApiHandler : IRequestHandler<HealthCheckApiQuery, ObjectResponse<HealthCheckResult>>
    {
        private readonly IhealthCheckUseCase _healthCheck;

        public HealtCheckApiHandler(IhealthCheckUseCase healthCheck)
        {
            _healthCheck = healthCheck;
        }

        public async Task<ObjectResponse<HealthCheckResult>> Handle(HealthCheckApiQuery request, CancellationToken cancellationToken)
        => await _healthCheck.CheckHealthApiAsync(request);
    }
}
