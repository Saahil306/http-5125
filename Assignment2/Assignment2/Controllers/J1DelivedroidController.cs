using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("api/j1")]
    public class J1DelivedroidController : ControllerBase
    {
        [HttpPost("delivedroid")]
        public int CalculateScore([FromForm] int collisions, [FromForm] int deliveries)
        {
            int score = (deliveries * 50) - (collisions * 10);
            if (deliveries > collisions)
            {
                score += 500;
            }
            return score;
        }
    }
}