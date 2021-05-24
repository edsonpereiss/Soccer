using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Soccer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : DataControllerBase
    {

        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger, IConfiguration iconfiguration) : base(iconfiguration)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("v.1/api/countries")]
        public new object countries(string api_token  = null, string include = null)
        {
            BodyApi bodyApi = new BodyApi();
            bodyApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                bodyApi.Qry.Add("include", include);

            return Callback(ApiRef.countries, bodyApi);    
        }

        [HttpGet]
        [Route("v.1/api/leagues")]
        public new object leagues(string api_token  = null, string include = null)
        {
            BodyApi bodyApi = new BodyApi();
            bodyApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                bodyApi.Qry.Add("include", include);

            return Callback(ApiRef.leagues, bodyApi);    
        }

        [HttpGet]
        [Route("v.1/api/leagues/{id}")]
        public new object leagueById(long id, string api_token  = null, string include = null)
        {
            BodyApi bodyApi = new BodyApi();
            bodyApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                bodyApi.Qry.Add("include", include);

            bodyApi.Rst.Add("id", id.ToString());

            return Callback(ApiRef.leagueById, bodyApi);      
        }


    }
}