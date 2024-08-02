using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Interfaces
{
    public interface IHealthCheckQueries
    {
        Task<HealthCheckResult> CheckHealthAsync();

        Task<HealthCheckResult> CheckHealthApiAsync(string url);
    }
}
