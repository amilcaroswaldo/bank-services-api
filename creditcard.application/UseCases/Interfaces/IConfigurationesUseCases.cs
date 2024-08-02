using creditcard.application.Features.Configurations.Queries;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.application.UseCases.Interfaces
{
    public interface IConfigurationesUseCases
    {
        Task<ObjectResponse<GetConfiguracion>> GetSingleConfigurationByName(GetConfiguracionesQueries query);
    }
}
