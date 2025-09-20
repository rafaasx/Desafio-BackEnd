using Microsoft.AspNetCore.Mvc;
using MotorRent.Application.DTO;
using MotorRent.Application.Services.Interfaces;
using MotorRent.OutOfBox.Queues;

namespace MotorRent.ApiService.Controllers
{
    [ApiController]
    [Route("api/motos")]
    public class MotoController(IMotoService motoService, IMessageSender messageSender) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotoDTO request)
        {
            try
            {
                await messageSender.Publish(request);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao iniciar o processamento: {ex.Message}");
            }
        }
    }
}
