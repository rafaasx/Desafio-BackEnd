using Microsoft.AspNetCore.Mvc;
using MotorRent.ApiService.Common;
using MotorRent.ApiService.Extensions;
using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Application.ViewModels;
using MotorRent.Domain.Constants;
using MotorRent.OutOfBox.Queues;
using Swashbuckle.AspNetCore.Annotations;

namespace MotorRent.ApiService.Controllers
{
    [ApiController]
    [Route("api/motos")]
    [Tags("motos")]
    public class MotoController(IMessageSender messageSender, IMotoService service) : ControllerBase
    {

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova moto")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateMotoDTO request)
        {
            await messageSender.Publish(request);
            return Created();
        }

        [HttpPut("{id}/placa")]
        [SwaggerOperation(Summary = "Modificar a placa de uma moto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPlate([FromRoute] string id, [FromBody] UpdateMotoDTO request)
        {
            var validationResult = await this.ValidateDTOAsync(request);
            if (validationResult is not null) return validationResult;
            await service.UpdatePlateAsync(id, request);
            return Ok(new ApiResponse { Mensagem = Messages.PlateUpdated });
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Consultar motos existentes por id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoViewModel>> GetById(string id)
        {
            var result = await service.GetViewModelByIdAsync(id);
            if (result is null)
                return NotFound(new ApiResponse { Mensagem = "Moto não encontrada." });
            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Consultar motos existentes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MotoViewModel>>> Get([FromQuery] string? placa)
        {
            var result = service.QueryByPlate(placa);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remover uma moto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] DeleteMotoDTO request)
        {
            await service.DeleteAsync(request.id);
            return Ok();
        }
    }
}
