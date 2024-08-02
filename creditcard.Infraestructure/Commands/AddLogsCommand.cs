using Azure;
using creditcard.application.Features.Logs.Commands;
using creditcard.application.Interfaces;
using creditcard.Domain.Base;
using creditcard.Infraestructure.DbContext.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Infraestructure.Commands
{
    public class AddLogsCommand : ILogsCommand
    {
        private readonly IAppDbContext _appDbContext;

        public AddLogsCommand(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GenericResponse> AddlogsInDB(AddLogsInDBCommand command)
        {
            var response = new GenericResponse();
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = await _connection.ExecuteAsync("sp_RegistrarLog", command);
                if (result== 0)
                {
                    response.Code = 0;
                    response.Message = $"No se registro el error en logs";
                }
                response.Code = 1;
                response.Message = $"El error ha sido registrado en la tabla logs";
                return response;
            }
            catch (SqlException ex)
            {
                response.Code = 0;
                response.Message = $"Error de base de datos: {ex.Message}";
                return response;
            }
            catch (Exception ex)
            {
                response.Code = 0;
                response.Message = $"Error interno: {ex.Message}";
                return response;
            }

        }
    }
}
