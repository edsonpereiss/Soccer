using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Soccer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private string error_api_token = "{ \"error\": { \"message\": \"Unauthenticated\", \"code\": 403 }}";
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("v.1/api/leagues")]
        public object leagues(string api_token  = null, string include = null)
        {
            if (string.IsNullOrEmpty(api_token))
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden, error_api_token);

            var nvc = new NameValueCollection();
            nvc.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                nvc.Add("include", include);

            string querys = ToQueryString(nvc);
            string uri = "https://soccer.sportmonks.com/api/v2.0/leagues" + querys;
            
            var client = new RestClient(uri);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            
            if (!response.IsSuccessful)            
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden,response.Content);
            else
               return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK,response.Content);
        }

        [HttpGet]
        [Route("v.1/api/countries")]
        public IEnumerable<WeatherForecast> countries()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            var array = (
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)
                select string.Format(
                "{0}={1}",
                    HttpUtility.UrlEncode(key),
                    HttpUtility.UrlEncode(value))
            ).ToArray();
        return "?" + string.Join("&", array);
        }

    }
}
