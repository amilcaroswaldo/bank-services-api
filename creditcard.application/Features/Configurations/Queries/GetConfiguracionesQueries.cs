using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Features.Configurations.Queries
{
    public class GetConfiguracionesQueries : IRequest<ObjectResponse<GetConfiguracion>>
    {
        public string Nombre { get; set; }
    }
}
