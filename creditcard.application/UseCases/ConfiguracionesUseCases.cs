using creditcard.application.Features.Configurations.Queries;
using creditcard.application.Features.Logs.Commands;
using creditcard.application.Interfaces;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases
{
    public class ConfiguracionesUseCases : IConfigurationesUseCases
    {
        private readonly IConfiguracionesQueries _configuracionesQueries;
        private readonly ILogsUseCases _logsUseCases;

        public ConfiguracionesUseCases(IConfiguracionesQueries configuracionesQueries, ILogsUseCases logsUseCases)
        {
            _configuracionesQueries = configuracionesQueries;
            _logsUseCases = logsUseCases;
        }

        public async Task<ObjectResponse<GetConfiguracion>> GetSingleConfigurationByName(GetConfiguracionesQueries query)
        {
            var response = new ObjectResponse<GetConfiguracion>();
            try
            {
                var result = await _configuracionesQueries.GetSingleConfigurationByName(query.Nombre);
                if (result.Code == 0)
                {
                    #region logs catch queries
                    AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                    logsParams.ErrorNumber = 1;
                    logsParams.ErrorMessage = result.Message;
                    logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                    logsParams.AdditionalInfo = "GetSingleConfigurationByName.query failed, error searchin: " + query.Nombre;
                    await _logsUseCases.AddlogsInDB(logsParams);
                    #endregion
                    response.Code = 0;
                    response.Message = result.Message;
                    response.Items = null;
                    return response;
                }
                response = result;
                return response;
            }
            catch (Exception ex)
            {
                #region logs use case
                AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                logsParams.ErrorNumber = 1;
                logsParams.ErrorMessage = ex.Message;
                logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                logsParams.AdditionalInfo = "GetSingleConfigurationByName failed, error" + ex.Source;
                await _logsUseCases.AddlogsInDB(logsParams);
                #endregion
                response.Code = 0;
                response.Message = ex.Message;
                response.Items = null;
                return response;
            }
        }
    }
}
