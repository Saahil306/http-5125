
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("api/q2")]
    public class Q2Controller : ControllerBase
    {
        /// <summary>
        /// Returns a greeting to the specified name.
        /// </summary>
        /// <param name="name">The name to greet.</param>
        /// <returns>A greeting message.</returns>
        /// <example>
        /// Response: "Hi Gary!"
        /// </example>
        [HttpGet(template:"greeting/{name}")]
        public string GetGreeting(string name)
        {
            return "Hi " +name+ "!";
        }
    }
}
