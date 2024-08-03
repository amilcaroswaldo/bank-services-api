using creditcard.application.Features.InfoClientes.Queries;
using creditcard.application.Features.Logs.Commands;
using creditcard.application.Interfaces;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.ClientesResponse;
using creditcard.Domain.FuncionesResponse;
using creditcard.Domain.Pagos;
using creditcard.Domain.TarjetasResponse;
using creditcard.Domain.Transacciones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases
{
    public class InfoClientesuseCases : IInfoClientesUseCases
    {
        private readonly ILogsUseCases _logsUseCases;
        private readonly IInfoClienteQueries _infoClienteQueries;

        public InfoClientesuseCases(IInfoClienteQueries infoClienteQueries, ILogsUseCases logsUseCases)
        {
            _infoClienteQueries = infoClienteQueries;
            _logsUseCases = logsUseCases;
        }
        public async Task<ListResponse<ClienteResponse>> AllClientes(ClienteQuery query)
        {
            var response = new ListResponse<ClienteResponse>();
            try
            {
                var result = await _infoClienteQueries.AllClientes();
                if (result.Code == 0)
                {
                    #region logs catch queries
                    AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                    logsParams.ErrorNumber = 1;
                    logsParams.ErrorMessage = result.Message;
                    logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                    logsParams.AdditionalInfo = "AllClientes.query failed, error searchin: ";
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
                logsParams.AdditionalInfo = "AllClientes failed, error" + ex.Source;
                await _logsUseCases.AddlogsInDB(logsParams);
                #endregion
                response.Code = 0;
                response.Message = ex.Message;
                response.Items = null;
                return response;
            }
        }

        public async Task<ListResponse<AllpagosResponse>> GetPagosFromTarjeta(AllPagosQuery query)
        {
            var response = new ListResponse<AllpagosResponse>();
            try
            {
                var result = await _infoClienteQueries.GetPagosFromTarjeta(query.NumeroTarjeta);
                if (result.Code == 0)
                {
                    #region logs catch queries
                    AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                    logsParams.ErrorNumber = 1;
                    logsParams.ErrorMessage = result.Message;
                    logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                    logsParams.AdditionalInfo = "GetPagosFromTarjeta.query failed, error searchin: " + query.NumeroTarjeta;
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
                logsParams.AdditionalInfo = "GetPagosFromTarjeta failed, error" + ex.Source;
                await _logsUseCases.AddlogsInDB(logsParams);
                #endregion
                response.Code = 0;
                response.Message = ex.Message;
                response.Items = null;
                return response;
            }
        }

        public async Task<ObjectResponse<TarjetaResponse>> GetTarjetaFromCliente(TarjetasQuery query)
        {
            var response = new ObjectResponse<TarjetaResponse>();
            try
            {
                var result = await _infoClienteQueries.GetTarjetaFromCliente(query.IdCliente);
                if (result.Code == 0)
                {
                    #region logs catch queries
                    AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                    logsParams.ErrorNumber = 1;
                    logsParams.ErrorMessage = result.Message;
                    logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                    logsParams.AdditionalInfo = "GetTarjetaFromCliente.query failed, error searchin: " + query.IdCliente;
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
                logsParams.AdditionalInfo = "GetTarjetaFromCliente failed, error" + ex.Source;
                await _logsUseCases.AddlogsInDB(logsParams);
                #endregion
                response.Code = 0;
                response.Message = ex.Message;
                response.Items = null;
                return response;
            }
        }
        public async Task<ListResponse<TransaccionesResponse>> GetTransaccionesByDate(TransaccionesQuery query)
        {
            var response = new ListResponse<TransaccionesResponse>();
            try
            {
                var result = await _infoClienteQueries.GetTransaccionesByDate(query.NumeroTarjeta, query.FchInicio, query.FchFin);
                if (result.Code == 0)
                {
                    #region logs catch queries
                    AddLogsInDBCommand logsParams = new AddLogsInDBCommand();
                    logsParams.ErrorNumber = 1;
                    logsParams.ErrorMessage = result.Message;
                    logsParams.OriginatingComponent = "CREDIT_CARD_SERVICE(API)";
                    logsParams.AdditionalInfo = "GetTransaccionesByDate.query failed, error searchin: " + JsonConvert.SerializeObject(query);
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
                logsParams.AdditionalInfo = "GetTransaccionesByDate failed, error" + ex.Source;
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
