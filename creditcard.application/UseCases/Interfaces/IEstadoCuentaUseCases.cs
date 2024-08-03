using creditcard.application.Features.EstadoCuenta.Queries;
using creditcard.Domain.Base;
using creditcard.Domain.EstadoCuentaResponse;
using creditcard.Domain.FuncionesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases.Interfaces
{
    public interface IEstadoCuentaUseCases
    {
        Task<ObjectResponse<InteresBonificableResponse>> GetInteresBonificable(InteresBonificableQuery query);
        Task<ObjectResponse<MontoContadoConInteresesResponse>> GetContadoConIntereses(MontoContadoConInteresesQuery query);
        Task<ObjectResponse<CoutaMinimaResponse>> GetCuotaMinima(CuotaMinimaQuery query);
        Task<ObjectResponse<EstadoCuentaRespons>> EstadoCuenta(EstadoCuentaQuery query);
    }
}
