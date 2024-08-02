using creditcard.application.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.FuncionesResponse;
using creditcard.Infraestructure.DbContext.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Infraestructure.Queries
{
    public class EstadoCuentaQueries : IEstadoCuentaQueries
    {
        private readonly IAppDbContext _appDbContext;

        public EstadoCuentaQueries(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ObjectResponse<MontoContadoConInteresesResponse>> GetContadoConIntereses(string v_numero_tarjeta)
        {
            var response = new ObjectResponse<MontoContadoConInteresesResponse>();
            string sql = "SELECT PackageEstadoCuenta.fn_CalcularMontoContadoConIntereses(@v_numero_tarjeta) as MontoContadoConIntereses";
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = await _connection.QueryFirstAsync<MontoContadoConInteresesResponse>(sql, new { v_numero_tarjeta });
                if (result == null)
                {
                    response.Code = 0;
                    response.Message = $"No se encontro el valor";
                }
                response.Code = 1;
                response.Message = $"success";
                response.Items = result;
                return response;
            }
            catch (SqlException ex)
            {
                response.Code = 0;
                response.Message = $"Error de base de datos: {ex.Message}";
                response.Items = null;
                return response;
            }
            catch (Exception ex)
            {
                response.Code = 0;
                response.Message = $"Error interno: {ex.Message}";
                response.Items = null;
                return response;
            }
        }

        public async Task<ObjectResponse<CoutaMinimaResponse>> GetCuotaMinima(string v_numero_tarjeta)
        {
            var response = new ObjectResponse<CoutaMinimaResponse>();
            string sql = "SELECT PackageEstadoCuenta.fn_CalcularCuotaMinima(@v_numero_tarjeta) as CuotaMinima";
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = await _connection.QueryFirstAsync<CoutaMinimaResponse>(sql, new { v_numero_tarjeta });
                if (result == null)
                {
                    response.Code = 0;
                    response.Message = $"No se encontro el valor";
                }
                response.Code = 1;
                response.Message = $"success";
                response.Items = result;
                return response;
            }
            catch (SqlException ex)
            {
                response.Code = 0;
                response.Message = $"Error de base de datos: {ex.Message}";
                response.Items = null;
                return response;
            }
            catch (Exception ex)
            {
                response.Code = 0;
                response.Message = $"Error interno: {ex.Message}";
                response.Items = null;
                return response;
            }
        }

        public async Task<ObjectResponse<InteresBonificableResponse>> GetInteresBonificable(string v_numero_tarjeta)
        {
            var response = new ObjectResponse<InteresBonificableResponse>();
            string sql = "SELECT PackageEstadoCuenta.fn_CalcularInteresBonificable(@v_numero_tarjeta) as InteresBonificable";
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = await _connection.QueryFirstAsync<InteresBonificableResponse>(sql, new { v_numero_tarjeta });
                if (result == null)
                {
                    response.Code = 0;
                    response.Message = $"No se encontro el valor";
                }
                response.Code = 1;
                response.Message = $"success";
                response.Items = result;
                return response;
            }
            catch (SqlException ex)
            {
                response.Code = 0;
                response.Message = $"Error de base de datos: {ex.Message}";
                response.Items = null;
                return response;
            }
            catch (Exception ex)
            {
                response.Code = 0;
                response.Message = $"Error interno: {ex.Message}";
                response.Items = null;
                return response;
            }
        }
    }
}
