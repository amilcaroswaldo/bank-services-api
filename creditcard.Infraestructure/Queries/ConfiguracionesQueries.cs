using Azure;
using creditcard.application.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using creditcard.Domain.EstadoCuentaResponse;
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
    public class ConfiguracionesQueries : IConfiguracionesQueries
    {
        private readonly IAppDbContext _appDbContext;

        public ConfiguracionesQueries(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ObjectResponse<GetConfiguracion>> GetSingleConfigurationByName(string name)
        {
            string query = @"SELECT  nombre, valor FROM configuraciones WHERE nombre = @pName";
            var mapParameters = new
            {
                pName = name
            };
            var response = new ObjectResponse<GetConfiguracion>();
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = await _connection.QueryFirstAsync<GetConfiguracion>(query, mapParameters);
                response.Items = result;
                if (result == null)
                {
                    response.Code = 0;
                    response.Message = $"No se encontro el valor";
                    return response;
                }
                response.Code = 1;
                response.Message = $"Success";
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
