using creditcard.application.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.ClientesResponse;
using creditcard.Domain.ConfiguracionesResponse;
using creditcard.Domain.Pagos;
using creditcard.Domain.TarjetasResponse;
using creditcard.Domain.Transacciones;
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
    public class InfoClientesQueries : IInfoClienteQueries
    {
        private readonly IAppDbContext _appDbContext;

        public InfoClientesQueries(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ListResponse<ClienteResponse>> AllClientes()
        {
            string query = @"SELECT
	                            cliente_id ClienteId,
	                            Nombre,
	                            numero_identidad NumeroIdentidad,
	                            Direccion,
	                            Telefono,
	                            Email
                            FROM
	                            clientes";
            var response = new ListResponse<ClienteResponse>();
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = (IList<ClienteResponse>)await _connection.QueryAsync<ClienteResponse>(query);
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
        public async Task<ListResponse<AllpagosResponse>> GetPagosFromTarjeta(string NumeroTarjeta)
        {
            string query = @"SELECT 
                                numero_tarjeta NumeroTarjeta,
                                monto MontoPago,
                                monto_disponible MontoDisp,
                                monto_pagado MontoPagado,
                                mora MontoMora,
                                fecha_pago FchaPago,
                                fecha_corte_inicio FchCorteIniP,
                                fecha_corte_fin FchCorteFinP
                            from pagos 
                                where numero_tarjeta = @NumeroTarjeta
                                order by fecha_pago desc";
            var response = new ListResponse<AllpagosResponse>();
            var mapParameters = new
            {
                NumeroTarjeta
            };
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = (IList<AllpagosResponse>)await _connection.QueryAsync<AllpagosResponse>(query, mapParameters);
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

        public async Task<ObjectResponse<TarjetaResponse>> GetTarjetaFromCliente(int IdCliente)
        {
            string query = @"SELECT  nombre, valor FROM configuraciones WHERE nombre = @pName";
            var mapParameters = new
            {
                IdCliente
            };
            var response = new ObjectResponse<TarjetaResponse>();
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = await _connection.QueryFirstAsync<TarjetaResponse>(query, mapParameters);
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

        public async Task<ListResponse<TransaccionesResponse>> GetTransaccionesByDate(string NumeroTarjeta, string FchInicio, string FchFin)
        {
            string query = @"SELECT 
                                numero_tarjeta NumeroTarjeta,
                                fecha FchaTransaccion,
                                Descripcion,
                                monto MontoTransaccion,
                                tipo_transaccion TipoTransaccion,
                                Categoria 
                            from transacciones
                                where numero_tarjeta = @NumeroTarjeta 
                                and fecha BETWEEN @FchInicio and @FchFin
                                order by fecha desc";
            var response = new ListResponse<TransaccionesResponse>();
            var mapParameters = new
            {
                NumeroTarjeta,
                FchInicio,
                FchFin
            };
            try
            {
                using var _connection = _appDbContext.GetDbConnection();
                _connection.Open();
                var result = (IList<TransaccionesResponse>)await _connection.QueryAsync<TransaccionesResponse>(query, mapParameters);
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
