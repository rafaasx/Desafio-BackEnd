using Microsoft.AspNetCore.Mvc;
using MotorRent.Application.Services.Interfaces;
using MotorRent.OutOfBox.Queues;

namespace MotorRent.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController(IRentalService motoService, IMessageSender messageSender) : ControllerBase
    {
    }
}
