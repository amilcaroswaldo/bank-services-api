using creditcard.application.Features.Configurations.Queries;
using creditcard.application.UseCases.Interfaces;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.Handlers.Configuraciones.Queries
{
    public class GetSingleConfigurationByname : IRequestHandler<GetConfiguracionesQueries, ObjectResponse<GetConfiguracion>>
    {
        private readonly IConfigurationesUseCases _configurationesUseCases;

        public GetSingleConfigurationByname(IConfigurationesUseCases configurationesUseCases)
        {
            _configurationesUseCases = configurationesUseCases;
        }

        public async Task<ObjectResponse<GetConfiguracion>> Handle(GetConfiguracionesQueries request, CancellationToken cancellationToken)
        => await _configurationesUseCases.GetSingleConfigurationByName(request);
    }
}
