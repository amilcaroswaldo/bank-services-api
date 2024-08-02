using AutoMapper;
using Azure.Core;
using creditcard.application.Features.Configurations.Queries;
using creditcard.application.Handlers.Configuraciones.Queries;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using creditcard.webapi.Models.Request;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace creditcard.webapi.Controllers.V1
{
    public class creditcardcontroller : BaseApiController
    {
        private readonly IMapper _mapper;

        public creditcardcontroller(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("GetSingleConfigurationByName")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResponse<GetConfiguracion>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResponse<GetConfiguracion>))]
        [ProducesResponseType(typeof(ObjectResponse<GetConfiguracion>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleConfigurationByName([FromQuery] GetSingleConfigurationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<GetConfiguracionesQueries>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }
    }
}
