using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("api/q4")]
    public class Q4Controller : ControllerBase
    {
        /// <summary>
        /// Starts a knock-knock joke.
        /// </summary>
        /// <returns>The first line of the knock-knock joke.</returns>
        /// <example>
        /// Response: "Who’s there?"
        /// </example>
        [HttpPost(template:"knockknock")]
        public string GetKnockKnockJoke()
        {
            return "Who’s there?";
        }
    }
}