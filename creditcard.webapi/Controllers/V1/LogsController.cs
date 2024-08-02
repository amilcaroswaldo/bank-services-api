using AutoMapper;
using creditcard.application.Features.Configurations.Queries;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using creditcard.Infraestructure.Commands;
using creditcard.webapi.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace creditcard.webapi.Controllers.V1
{
    public class LogsController : BaseApiController
    {
        private readonly IMapper _mapper;

        public LogsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("AddLogsInDB")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddLogsInDB([FromBody] AddlogsInDBRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<AddLogsCommand>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }
    }
}
