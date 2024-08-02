using creditcard.application.Features.HealthCheck.Queries;
using creditcard.application.Features.Logs.Commands;
using creditcard.application.Interfaces;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases
{
    public class HealthCheckUseCases : IhealthCheckUseCase
    {
        private readonly IHealthCheckQueries _healthCheck;
        private readonly ILogsUseCases _logsUseCases;

        public HealthCheckUseCases(IHealthCheckQueries healthCheck, ILogsUseCases logsUseCases)
        {
            _healthCheck = healthCheck;
            _logsUseCases = logsUseCases;
        }

        public async Task<ObjectResponse<HealthCheckResult>> CheckHealthApiAsync(HealthCheckApiQuery query)
        {
            var response = new ObjectResponse<HealthCheckResult>();
            try
            {                
                var result = await _healthCheck.CheckHealthApiAsync(query.Url);
                response.Code = 1;
                response.Message = result.ToString();
                response.Items = result;
                return response;
            }
            catch (Exception ex)
            {
                #region logs use case
                AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                logsParams.ErrorNumber = 1;
                logsParams.ErrorMessage = ex.Message;
                logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)" + query.Url;
                logsParams.AdditionalInfo = ex.Source;
                await _logsUseCases.AddlogsInDB(logsParams);
                #endregion
                response.Code = 0;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ObjectResponse<HealthCheckResult>> CheckHealthDBAsync()
        {
            var response = new ObjectResponse<HealthCheckResult>();
            try
            {
                var result = await _healthCheck.CheckHealthAsync();
                response.Code = 1;
                response.Message = result.ToString();
                response.Items = result;
                return response;
            }
            catch (Exception ex)
            {
                #region logs use case
                AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                logsParams.ErrorNumber = 1;
                logsParams.ErrorMessage = ex.Message;
                logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                logsParams.AdditionalInfo = ex.Source;
                await _logsUseCases.AddlogsInDB(logsParams);
                #endregion
                response.Code = 0;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
