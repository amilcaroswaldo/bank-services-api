using creditcard.Domain.Base;
using creditcard.Domain.FuncionesResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.EstadoCuenta.Queries
{
    public class CuotaMinimaQuery : IRequest<ObjectResponse<CoutaMinimaResponse>>
    {
        public string NumeroTarjeta { get; set; }
    }
}
