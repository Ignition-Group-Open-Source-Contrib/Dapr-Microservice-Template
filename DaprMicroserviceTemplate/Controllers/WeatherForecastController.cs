using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DaprMicroserviceTemplate.Controllers
{
    /// <summary>
    /// This is an example controller description
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly ILog _logger = LogManager.GetLogger(typeof(WeatherForecastController));

        /// <summary>
        /// This is an example endpoint description
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.Info(this.Request.Path);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
