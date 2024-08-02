using creditcard.application.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using creditcard.Infraestructure.DbContext.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Infraestructure.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheckQueries
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public DatabaseHealthCheck(IAppDbContext appDbContext, IHttpClientFactory httpClientFactory)
        {
            _appDbContext = appDbContext;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthApiAsync(string url)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return HealthCheckResult.Healthy($"Endpoint '{url}' is healthy.");
            }
            catch (HttpRequestException ex)
            {
                return HealthCheckResult.Unhealthy($"Endpoint '{url}' is unhealthy: {ex.Message}");
            }
        }

        public async Task<HealthCheckResult> CheckHealthAsync()
        {
            try
            {
                string query = @"SELECT COUNT(nombre) FROM configuraciones";
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = await _connection.QueryFirstAsync<int>(query);
                return result > 0 ?
                    HealthCheckResult.Healthy("Database connection is healthy.") :
                    HealthCheckResult.Unhealthy("Database connection is unhealthy.");
            }
            catch (SqlException ex)
            {
                return HealthCheckResult.Unhealthy($"Database connection is unhealthy: {ex.Message}");
            }
        }
    }
}
