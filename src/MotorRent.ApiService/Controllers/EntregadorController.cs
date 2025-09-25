using Microsoft.AspNetCore.Mvc;
using MotorRent.Application.DTO.Entregador;
using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services.Interfaces;
using MotorRent.OutOfBox.Queues;
using Swashbuckle.AspNetCore.Annotations;

namespace MotorRent.ApiService.Controllers
{
    [ApiController]
    [Route("api/entregadores")]
    [Tags("entregadores")]
    public class EntregadorController(IEntregadorService service, IMessageSender messageSender) : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Cadastrar entregador")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateEntregadorDTO request)
        {
            await messageSender.Publish(request);
            return Created();
        }

        [HttpPost("{id}/cnh")]
        [SwaggerOperation(Summary = "Enviar foto da CNH")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostCnh([FromRoute] string id, [FromBody] UpdateEntregadorDTO request)
        {
            await service.UpdateCnhImageAsync(id, request.imagem_cnh);
            return Created();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remover um entregador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] DeleteEntregadorDTO request)
        {
            await service.DeleteAsync(request.id);
            return Ok();
        }
    }
}
