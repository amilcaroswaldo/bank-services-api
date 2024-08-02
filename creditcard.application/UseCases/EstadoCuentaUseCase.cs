﻿using creditcard.application.Features.EstadoCuenta.Queries;
using creditcard.application.Features.Logs.Commands;
using creditcard.application.Interfaces;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using creditcard.Domain.FuncionesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases
{
    public class EstadoCuentaUseCase : IEstadoCuentaUseCases
    {
        private readonly IEstadoCuentaQueries _estadoCuentaQueries;
        private readonly ILogsUseCases _logsUseCases;

        public EstadoCuentaUseCase(IEstadoCuentaQueries estadoCuentaQueries, ILogsUseCases logsUseCases)
        {
            _estadoCuentaQueries = estadoCuentaQueries;
            _logsUseCases = logsUseCases;
        }

        public async Task<ObjectResponse<MontoContadoConInteresesResponse>> GetContadoConIntereses(MontoContadoConInteresesQuery query)
        {
            var response = new ObjectResponse<MontoContadoConInteresesResponse>();
            try
            {
                var result = await _estadoCuentaQueries.GetContadoConIntereses(query.NumeroTarjeta);
                if (result.Code == 0)
                {
                    #region logs catch queries
                    AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                    logsParams.ErrorNumber = 1;
                    logsParams.ErrorMessage = result.Message;
                    logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                    logsParams.AdditionalInfo = "GetContadoConIntereses.query failed, error searchin: " + query.NumeroTarjeta;
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
                logsParams.AdditionalInfo = "GetContadoConIntereses failed, error" + ex.Source;
                await _logsUseCases.AddlogsInDB(logsParams);
                #endregion
                response.Code = 0;
                response.Message = ex.Message;
                response.Items = null;
                return response;
            }
        }

        public async Task<ObjectResponse<CoutaMinimaResponse>> GetCuotaMinima(CuotaMinimaQuery query)
        {
            var response = new ObjectResponse<CoutaMinimaResponse>();
            try
            {
                var result = await _estadoCuentaQueries.GetCuotaMinima(query.NumeroTarjeta);
                if (result.Code == 0)
                {
                    #region logs catch queries
                    AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                    logsParams.ErrorNumber = 1;
                    logsParams.ErrorMessage = result.Message;
                    logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                    logsParams.AdditionalInfo = "GetCuotaMinima.query failed, error searchin: " + query.NumeroTarjeta;
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
                logsParams.AdditionalInfo = "GetContadoConIntereses failed, error" + ex.Source;
                await _logsUseCases.AddlogsInDB(logsParams);
                #endregion
                response.Code = 0;
                response.Message = ex.Message;
                response.Items = null;
                return response;
            }
        }

        public async Task<ObjectResponse<InteresBonificableResponse>> GetInteresBonificable(InteresBonificableQuery query)
        {
            var response = new ObjectResponse<InteresBonificableResponse>();
            try
            {
                var result = await _estadoCuentaQueries.GetInteresBonificable(query.NumeroTarjeta);
                if (result.Code == 0)
                {
                    #region logs catch queries
                    AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                    logsParams.ErrorNumber = 1;
                    logsParams.ErrorMessage = result.Message;
                    logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                    logsParams.AdditionalInfo = "GetInteresBonificable.query failed, error searchin: " + query.NumeroTarjeta;
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
                logsParams.AdditionalInfo = "GetInteresBonificable failed, error" + ex.Source;
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
