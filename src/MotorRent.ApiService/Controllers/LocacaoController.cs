using Microsoft.AspNetCore.Mvc;
using MotorRent.ApiService.Common;
using MotorRent.ApiService.Extensions;
using MotorRent.Application.DTO.Locacao;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Constants;
using MotorRent.OutOfBox.Queues;
using Swashbuckle.AspNetCore.Annotations;

namespace MotorRent.ApiService.Controllers
{
    [ApiController()]
    [Route("api/locacao")]
    [Tags("locações")]
    public class LocacaoController(ILocacaoService service, IMessageSender messageSender) : ControllerBase
    {
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Consultar locação por id")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.GetViewModelByIdAsync(id);
            if (result is null)
                return NotFound(new ApiResponse { Mensagem = "Locação não encontrada." });
            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Alugar uma moto")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateLocacaoDTO request)
        {
            await messageSender.Publish(request);
            return Created();
        }

        [HttpPut("{id}/devolucao")]
        [SwaggerOperation(Summary = "Informar data de devolução e calcular valor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutReturnedAt([FromRoute] string id, [FromBody] UpdateLocacaoDTO request)
        {
            var validationResult = await this.ValidateDTOAsync(request);
            if (validationResult is not null) return validationResult;
            await service.UpdateReturnedAtAsync(id, request);
            return Ok(new ApiResponse { Mensagem = Messages.RentalUpdated });
        }
    }
}
