using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("api/J1")]
    public class J1RollerCoasterRide : ControllerBase
    {
        /// <summary>
        /// Determines if a person will be on the next roller coaster ride.
        /// </summary>
        /// <param name="n">Your position in line (N).</param>
        /// <param name="c">Number of cars on the train (C).</param>
        /// <param name="p">People per car (P).</param>
        /// <returns>"yes" if you get on the ride, otherwise "no".</returns>
        /// <example>
        /// GET /api/J1/RollerCoasterRide?n=14&c=3&p=2
        /// Response: "no"
        /// </example>
        [HttpGet("RollerCoasterRide")]
        public IActionResult CanRide([FromQuery] int n, [FromQuery] int c, [FromQuery] int p)
        {
            int totalCapacity = c * p; // Total people the train can hold

            if (n <= totalCapacity)
            {
                return Ok("yes"); // You get on the ride
            }
            else
            {
                return Ok("no"); // You have to wait
            }
        }
    }
}
