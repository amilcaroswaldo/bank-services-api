using AutoMapper;
using creditcard.application.Features.EstadoCuenta.Queries;
using creditcard.application.Features.InfoClientes.Queries;
using creditcard.Domain.Base;
using creditcard.Domain.ClientesResponse;
using creditcard.Domain.EstadoCuentaResponse;
using creditcard.Domain.Pagos;
using creditcard.Domain.TarjetasResponse;
using creditcard.Domain.Transacciones;
using creditcard.webapi.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace creditcard.webapi.Controllers.V1
{
    public class InfoClienteController : BaseApiController
    {
        private readonly IMapper _mapper;

        public InfoClienteController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("GetAllClientes")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ListResponse<EstadoCuentaRespons>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ListResponse<EstadoCuentaRespons>))]
        [ProducesResponseType(typeof(ListResponse<ClienteResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClientes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var request = new GetAllClientesRequest();
                return Ok(await Mediator.Send(_mapper.Map<ClienteQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }

        [HttpGet("GetTarjetaByIDCliente")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ObjectResponse<TarjetaResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResponse<TarjetaResponse>))]
        [ProducesResponseType(typeof(ObjectResponse<TarjetaResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTarjetaByIDCliente([FromQuery] GetTarjetasRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<TarjetasQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }

        [HttpGet("GetPagosFromTarjeta")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ListResponse<AllpagosResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ListResponse<AllpagosResponse>))]
        [ProducesResponseType(typeof(ListResponse<AllpagosResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPagosFromTarjeta([FromQuery] GetPagosFromTarjetaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<AllPagosQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }

        [HttpGet("GetTransaccionesByRangedate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ListResponse<TransaccionesResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ListResponse<TransaccionesResponse>))]
        [ProducesResponseType(typeof(ListResponse<TransaccionesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransaccionesByRangedate([FromQuery] GetTransaccionesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await Mediator.Send(_mapper.Map<TransaccionesQuery>(request)));
            }
            catch (Exception ex)
            {
                string msg = string.Format("select failed", null, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Code = 0, Message = msg });
            }
        }
    }
}
