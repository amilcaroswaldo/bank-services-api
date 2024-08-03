using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using creditcard.Domain.EstadoCuentaResponse;
using creditcard.Domain.FuncionesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Interfaces
{
    public interface IEstadoCuentaQueries
    {
        Task<ObjectResponse<InteresBonificableResponse>> GetInteresBonificable(string v_numero_tarjeta);
        Task<ObjectResponse<MontoContadoConInteresesResponse>> GetContadoConIntereses(string v_numero_tarjeta);
        Task<ObjectResponse<CoutaMinimaResponse>> GetCuotaMinima(string v_numero_tarjeta);
        Task<ObjectResponse<EstadoCuentaRespons>> EstadoCuenta(string Numero_Tarjeta);
        Task<ObjectResponse<NumeroTarjertaResponse>> GenerarNumeroTarjeta(string NumeroTarjeta);
    }
}
