using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("api/q3")]
    public class Q3Controller : ControllerBase
    {
        /// <summary>
        /// Returns the cube of the integer number.
        /// </summary>
        /// <param name="base">The base number to cube.</param>
        /// <returns>The cube of the base number.</returns>
        /// <example>
        /// GET http://localhost:7123/api/q3/cube/4
        /// Response: 64
       
        [HttpGet("cube/{base}")]
        public int GetCube(int @base)
        {
            return (int)Math.Pow(@base, 3);
        }
    }
}