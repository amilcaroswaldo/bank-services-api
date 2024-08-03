using creditcard.application.Features.InfoClientes.Queries;
using creditcard.Domain.Base;
using creditcard.Domain.ClientesResponse;
using creditcard.Domain.Pagos;
using creditcard.Domain.TarjetasResponse;
using creditcard.Domain.Transacciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases.Interfaces
{
    public interface IInfoClientesUseCases
    {
        Task<ListResponse<ClienteResponse>> AllClientes(ClienteQuery query);
        Task<ObjectResponse<TarjetaResponse>> GetTarjetaFromCliente(TarjetasQuery query);
        Task<ListResponse<AllpagosResponse>> GetPagosFromTarjeta(AllPagosQuery query);
        Task<ListResponse<TransaccionesResponse>> GetTransaccionesByDate(TransaccionesQuery query);
    }
}
