using AutoMapper;
using creditcard.application.Features.Configurations.Queries;
using creditcard.application.Features.EstadoCuenta.Queries;
using creditcard.Domain.Base;
using creditcard.Domain.ConfiguracionesResponse;
using creditcard.Domain.EstadoCuentaResponse;
using creditcard.Domain.FuncionesResponse;
using creditcard.webapi.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace creditcard.webapi.Controllers.V1
{
    public class EstadoCuentaController : BaseApiController
    {
        private readonly IMapper _mapper;

        public EstadoCuentaController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("GetInteresBonificable")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResponse<InteresBonificableResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResponse<InteresBonificableResponse>))]
        [ProducesResponseType(typeof(ObjectResponse<InteresBonificableResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInteresBonificable([FromQuery] InteresBonificableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<InteresBonificableQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }

        [HttpGet("GetCoutaMinima")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResponse<CoutaMinimaResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResponse<CoutaMinimaResponse>))]
        [ProducesResponseType(typeof(ObjectResponse<CoutaMinimaResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoutaMinima([FromQuery] CuotaMinimaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<CuotaMinimaQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }

        [HttpGet("GetMontoContadoConintereses")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResponse<MontoContadoConInteresesResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResponse<MontoContadoConInteresesResponse>))]
        [ProducesResponseType(typeof(ObjectResponse<MontoContadoConInteresesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMontoContadoConintereses([FromQuery] MontoContadoConInteresesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<MontoContadoConInteresesQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }

        [HttpGet("GetEstadoCuentaByRangeDate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResponse<EstadoCuentaRespons>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResponse<EstadoCuentaRespons>))]
        [ProducesResponseType(typeof(ObjectResponse<EstadoCuentaRespons>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEstadoCuentaByRangeDate([FromQuery] GetEstadoCuentaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<EstadoCuentaQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }
    }
}
