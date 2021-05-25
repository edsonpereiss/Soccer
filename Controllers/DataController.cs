using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Soccer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {

        private readonly ILogger<DataController> _logger;
        protected readonly string error_api_token = "{ \"error\": { \"message\": \"Unauthenticated\", \"code\": 403 }}";

        public DataController(ILogger<DataController> logger, IConfiguration iconfiguration)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("v.1/api/countries")]
        public object countries(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return CallApi(ApiRef.countries, paramApi);    
        }

        [HttpGet]
        [Route("v.1/api/leagues")]
        public object leagues(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return CallApi(ApiRef.leagues, paramApi);    
        }

        [HttpGet]
        [Route("v.1/api/leagues/{id}")]
        public object leagueById(long id, string api_token  = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.leagueById, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/fixtures/{id}")]
        public object fixturesForDate(long id, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.fixturesById, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/fixtures/date/{date}")]
        public object fixturesForDate(string date, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("date", date);

            return CallApi(ApiRef.fixturesForDate, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/fixtures/between/{from}/{to}")]
        public object fixturesBetweenDates(string from, string to, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("from", from);
            paramApi.Rst.Add("to", to);

            return CallApi(ApiRef.fixturesBetweenDates, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/fixtures/multi/{ids}")]
        public object particularFixtures(string ids, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("ids", ids);

            return CallApi(ApiRef.particularFixtures, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/commentaries/fixture/{id}")]
        public object commentary(long id, string api_token  = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.commentary, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/players/{id}")]
        public object player(long id, string api_token  = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.player, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/head2head/{team1id}/{team2id}")]
        public object h2h(long team1id, long team2id, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("team1id", team1id.ToString());
            paramApi.Rst.Add("team2id", team2id.ToString());

            return CallApi(ApiRef.h2h, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/teams/{id}")]
        public object teamById(long id, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.teamById, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/livescores")]
        public object livescores(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return CallApi(ApiRef.livescores, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/livescores/now")]
        public object livescoresNow(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return CallApi(ApiRef.livescoresNow, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/teams/season/{seasonId}")]
        public object seasonTeams(long seasonId, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("seasonId", seasonId.ToString());

            return CallApi(ApiRef.seasonTeams, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/seasons/{id}")]
        public object seasonResults(long id, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.seasonResults, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/squad/season/{seasonId}/team/{teamId}")]
        public object seasonSquad(long seasonId, long teamId, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("seasonId", seasonId.ToString());
            paramApi.Rst.Add("teamId", teamId.ToString());

            return CallApi(ApiRef.seasonSquad, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/topscorers/season/{seasonId}")]
        public object seasonTopPlayers(long seasonId,  string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("seasonId", seasonId.ToString());

            return CallApi(ApiRef.seasonTopPlayers, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/standings/season/{id}")]
        public object standings(long id,  string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.standings, paramApi);      
        }
        private object CallApi(ApiRef apiref, ParamApi ParamApi)
        {
            
            foreach (var item in ParamApi.Qry.AllKeys)
            {
                if(item.Equals("api_token"))
                    {
                        string[] value = ParamApi.Qry.GetValues(item);
                        if (value == null)
                            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden, error_api_token);
                        if (value[0].Equals(""))
                            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden, error_api_token);
                        break;
                    }
            }
            
            IRestResponse response = DataCallback.Callback(apiref, ParamApi);
            
            if (!response.IsSuccessful)            
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden,response.Content);
            else
               return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK,response.Content);
        }
    }
}