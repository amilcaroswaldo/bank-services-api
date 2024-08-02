using AutoMapper;
using creditcard.application.Features.Configurations.Queries;
using creditcard.application.Features.EstadoCuenta.Queries;
using creditcard.application.Features.HealthCheck.Queries;
using creditcard.application.Features.IHealthCheck.Queries;
using creditcard.Infraestructure.Commands;
using creditcard.webapi.Models.Request;

namespace creditcard.webapi.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            Mapping();
        }

        private void Mapping()
        {
            CreateMap<GetSingleConfigurationRequest, GetConfiguracionesQueries>().ReverseMap();
            CreateMap<HealthCheckDatabaseRequest, HealthCheckDatabaseQuery>().ReverseMap();
            CreateMap<HealthCheckApiRequest, HealthCheckApiQuery>().ReverseMap();
            CreateMap<AddlogsInDBRequest, AddLogsCommand>().ReverseMap();
            CreateMap<InteresBonificableRequest, InteresBonificableQuery>().ReverseMap();
            CreateMap<CuotaMinimaRequest, CuotaMinimaQuery>().ReverseMap();
            CreateMap<MontoContadoConInteresesRequest, MontoContadoConInteresesQuery>().ReverseMap();
        }
    }
}
