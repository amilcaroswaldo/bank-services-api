using creditcard.Domain.Base;
using creditcard.Domain.EstadoCuentaResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.EstadoCuenta.Queries
{
    public class EstadoCuentaQuery : IRequest<ObjectResponse<EstadoCuentaRespons>>
    {
        public string Numero_Tarjeta { get; set; }
    }
}
