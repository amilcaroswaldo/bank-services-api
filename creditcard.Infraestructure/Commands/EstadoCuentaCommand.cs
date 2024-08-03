using creditcard.application.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.EstadoCuentaResponse;
using creditcard.Infraestructure.DbContext.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Infraestructure.Commands
{
    public class EstadoCuentaCommand : IEstadoCuentaCommand
    {
        private readonly IAppDbContext _appDbContext;

        public EstadoCuentaCommand(IAppDbContext appDbContext)=> _appDbContext = appDbContext;
        public async Task<GenericResponse> Addpago(string Numero_Tarjeta, double Monto)
        {
            var response = new GenericResponse();
            var paramsSp = new { Numero_Tarjeta, Monto };
            string sql = "PackageEstadoCuenta.sp_RealizarPago";
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, paramsSp, commandType: CommandType.StoredProcedure);                
                if (result == 0)
                {
                    response.Code = 0;
                    response.Message = $"No se hizo el pago";
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
                return response;
            }
            catch (Exception ex)
            {
                response.Code = 0;
                response.Message = $"Error interno: {ex.Message}";
                return response;
            }
        }

        public async Task<GenericResponse> AddTransaccion(string Numero_Tarjeta, string Descripcion, double Monto, string Tipo_Transaccion, string categoria)
        {
            var response = new GenericResponse();
            var paramsSp = new { Numero_Tarjeta, Descripcion, Monto, Tipo_Transaccion , categoria };
            string sql = "PackageEstadoCuenta.sp_RegistrarTransaccion";
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, paramsSp, commandType: CommandType.StoredProcedure);
                if (result == 0)
                {
                    response.Code = 0;
                    response.Message = $"No se agrego la transaccion";
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
