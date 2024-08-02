using AutoMapper;
using creditcard.application.Features.Configurations.Queries;
using creditcard.application.Features.HealthCheck.Queries;
using creditcard.application.Features.IHealthCheck.Queries;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using creditcard.webapi.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace creditcard.webapi.Controllers.V1
{
    public class HealthCheckController : BaseApiController
    {
        private readonly IMapper _mapper;

        public HealthCheckController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("CheckDatabaseConnection")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResponse<object>))]
        [ProducesResponseType(typeof(ObjectResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleConfigurationByName()
        {
            try
            {
                var request = new HealthCheckDatabaseRequest();
                return Ok(await Mediator.Send(_mapper.Map<HealthCheckDatabaseQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("CheckDatabaseConnection failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }

        [HttpGet("CheckEndpointConnection")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResponse<object>))]
        [ProducesResponseType(typeof(ObjectResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckEndpointConnection([FromQuery] HealthCheckApiRequest request)
        {
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<HealthCheckApiQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("CheckEndpointConnection failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }
    }
}
