using creditcard.Domain.Base;
using creditcard.Domain.ClientesResponse;
using creditcard.Domain.EstadoCuentaResponse;
using creditcard.Domain.Pagos;
using creditcard.Domain.TarjetasResponse;
using creditcard.Domain.Transacciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Interfaces
{
    public interface IInfoClienteQueries
    {
        Task<ListResponse<ClienteResponse>> AllClientes();
        Task<ObjectResponse<TarjetaResponse>> GetTarjetaFromCliente(int IdCliente);
        Task<ListResponse<AllpagosResponse>> GetPagosFromTarjeta(string NumeroTarjeta);
        Task<ListResponse<TransaccionesResponse>> GetTransaccionesByDate(string NumeroTarjeta, string FchInicio, string FchFin);
    }
}
