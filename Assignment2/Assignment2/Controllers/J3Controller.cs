using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.Controllers
{
    [Route("api/J3")]
    [ApiController]
    public class J3Controller : ControllerBase
    {
        [HttpPost("BronzeCount")]
        public IActionResult GetBronzeCount([FromBody] ScoreRequest request)
        {
            if (request.Scores == null || request.Scores.Count < 3)
            {
                return BadRequest("Invalid input. At least three distinct scores are required.");
            }

            // Sort scores in descending order
            var sortedScores = request.Scores.Distinct().OrderByDescending(s => s).ToList();

            // The third highest score is at index 2 (0-based index)
            int bronzeScore = sortedScores[2];

            // Count occurrences of the bronze score
            int count = request.Scores.Count(s => s == bronzeScore);

            return Ok(new { bronzeScore, count });
        }
    }

    // Request model to handle JSON input
    public class ScoreRequest
    {
        public List<int> Scores { get; set; }
    }
}
