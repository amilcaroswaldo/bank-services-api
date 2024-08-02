using creditcard.application.Features.IHealthCheck.Queries;
using creditcard.application.Interfaces;
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
    public class HealthCheckDatabaseHandler : IRequestHandler<HealthCheckDatabaseQuery, ObjectResponse<HealthCheckResult>>
    {
        private readonly IhealthCheckUseCase _healthCheck;

        public HealthCheckDatabaseHandler(IhealthCheckUseCase healthCheck)
        {
            _healthCheck = healthCheck;
        }

        public async Task<ObjectResponse<HealthCheckResult>> Handle(HealthCheckDatabaseQuery request, CancellationToken cancellationToken)
        => await _healthCheck.CheckHealthDBAsync();
    }
}
