using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("api/q6")]
    public class Q6Controller : ControllerBase
    {
        /// <summary>
        /// Returns the area of a regular hexagon based on the given side length.
        /// </summary>
        /// <param name="side">The side length of the hexagon.</param>
        /// <returns>The area of the hexagon.</returns>
        /// <example>
        
        /// Response: 2.598076211353316
        /// </example>
        [HttpGet(template:"hexagon")]
        public ActionResult<double> GetHexagonArea([FromQuery] double side)
        {
            if (side <= 0)
            {
                return BadRequest("Side length must be greater than zero.");
            }

            double area = (3 * Math.Sqrt(3) / 2) * Math.Pow(side, 2);
            return area;
        }
    }
}