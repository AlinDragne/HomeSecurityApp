using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HomeSecurityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotionController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public MotionController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostMotionDetection([FromBody] MotionData motionData)
        {
            if (motionData.Motion == "detected")
            {
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Motion detected!");
                return Ok("Motion detected");
            }
            return BadRequest("Invalid data");
        }
    }

    public class MotionData
    {
        public string Motion { get; set; }
    }
}
