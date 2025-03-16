using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("api/J2")]
    public class J2DonutShopController : ControllerBase
    {
        /// <summary>
        /// Calculates the number of donuts remaining at the end of the day.
        /// </summary>
        /// <param name="d">Number of donuts available at opening.</param>
        /// <param name="events">List of events (baking or selling).</param>
        /// <returns>Number of donuts left at closing.</returns>
        /// <example>
        /// POST /api/J2/DonutShop
        /// Body: { "d": 10, "events": [{ "action": "+", "quantity": 24 }, { "action": "-", "quantity": 6 }, { "action": "-", "quantity": 12 }] }
        /// Response: 16
        /// </example>
        [HttpPost("DonutShop")]
        public IActionResult GetRemainingDonuts([FromBody] DonutRequest request)
        {
            int remainingDonuts = request.D; // Start with the initial number of donuts

            foreach (var e in request.Events)
            {
                if (e.Action == "+")
                {
                    remainingDonuts += e.Quantity; // Bake more donuts
                }
                else if (e.Action == "-")
                {
                    remainingDonuts -= e.Quantity; // Sell donuts
                }
            }

            return Ok(remainingDonuts); // Return the final count
        }
    }

    // Class to store the request data
    public class DonutRequest
    {
        public int D { get; set; } // Initial donuts
        public List<DonutEvent> Events { get; set; } // List of events
    }

    public class DonutEvent
    {
        public string Action { get; set; } // "+" for baking, "-" for selling
        public int Quantity { get; set; } // Number of donuts
    }
}
