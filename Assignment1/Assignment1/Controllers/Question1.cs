using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("api/q1")]
    public class Q1Controller : ControllerBase
    {
        /// <summary>
        /// Returns a welcome message.
        /// </summary>
        /// <returns>A welcome message string.</returns>
        /// <example>
       
        /// Response: "Welcome to 5125!"
        /// </example>
        [HttpGet(template:"welcome")]

        public string GetWelcomeMessage()
        {
            return "Welcome to 5125!";
        }
    }
}