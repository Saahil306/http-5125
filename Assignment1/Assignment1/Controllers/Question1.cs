﻿using Microsoft.AspNetCore.Mvc;

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
        /// GET http://localhost:7123/api/q1/welcome
        /// Response: "Welcome to 5125!"
        /// </example>
        [HttpGet("welcome")]
        public string GetWelcomeMessage()
        {
            return "Welcome to 5125!";
        }
    }
}