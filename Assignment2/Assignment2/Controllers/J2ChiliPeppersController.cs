using Microsoft.AspNetCore.Mvc;
using System;


namespace Assignment.Controllers
{
    // This makes our class a controller for handling web requests
    [ApiController]
    [Route("api/J2")]
    public class J2ChiliPeppersController : ControllerBase
    {
        // Dictionary to store pepper names and their SHU values
        private static readonly Dictionary<string, int> PepperSHU = new Dictionary<string, int>()
        {
            { "Poblano", 1500 },
            { "Mirasol", 6000 },
            { "Serrano", 15500 },
            { "Cayenne", 40000 },
            { "Thai", 75000 },
            { "Habanero", 125000 }
        };

        /// <summary>
        /// This method calculates the total spiciness based on given peppers.
        /// </summary>
        /// <param name="ingredients">A list of pepper names separated by commas.</param>
        /// <returns>The total SHU value.</returns>
        [HttpGet("ChiliPeppers")]
        public IActionResult GetSpiciness([FromQuery] string ingredients)
        {
            // If no ingredients are provided, return an error message
            if (string.IsNullOrEmpty(ingredients))
            {
                return BadRequest("Please provide a list of peppers.");
            }

            int totalSHU = 0; // Variable to store total spiciness

            // Split the input string by commas to get each pepper
            string[] peppers = ingredients.Split(',');

            // Loop through each pepper
            foreach (string pepper in peppers)
            {
                string trimmedPepper = pepper.Trim(); // Remove any spaces

                // Check if the pepper exists in our dictionary
                if (PepperSHU.ContainsKey(trimmedPepper))
                {
                    totalSHU += PepperSHU[trimmedPepper]; // Add its SHU value
                }
                else
                {
                    return BadRequest($"Invalid pepper name: {trimmedPepper}");
                }
            }

            // Return the total spiciness value
            return Ok(totalSHU);
        }
    }
}
