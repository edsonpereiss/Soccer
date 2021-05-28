using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using Soccer.Repositories;

namespace Soccer.Controllers
{
    [ApiController]
    [Route("api/v1.0")]
    public class DataController : ControllerBase
    {

        private readonly ILogger<DataController> _logger;
        private readonly IContentRepository _repository;

        protected readonly string error_api_token = "{ \"error\": { \"message\": \"Unauthenticated\", \"code\": 403 }}";

        public DataController(ILogger<DataController> logger, IConfiguration iconfiguration, IContentRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Route("countries")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public object countries(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return CallApi(ApiRef.countries, paramApi);    
        }

        [HttpGet]
        [Route("leagues")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public object leagues(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return CallApi(ApiRef.leagues, paramApi);    
        }

        [HttpGet]
        [Route("leagues/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public object leagueById(long id, string api_token  = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.leagueById, paramApi);      
        }

        [HttpGet]
        [Route("fixtures/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("fixtures/date/{date}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("fixtures/between/{from}/{to}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("fixtures/multi/{ids}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("commentaries/fixture/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public object commentary(long id, string api_token  = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.commentary, paramApi);      
        }

        [HttpGet]
        [Route("players/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public object player(long id, string api_token  = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            paramApi.Rst.Add("id", id.ToString());

            return CallApi(ApiRef.player, paramApi);      
        }

        [HttpGet]
        [Route("head2head/{team1id}/{team2id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("teams/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("livescores")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public object livescores(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return CallApi(ApiRef.livescores, paramApi);      
        }

        [HttpGet]
        [Route("livescores/now")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public object livescoresNow(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return CallApi(ApiRef.livescoresNow, paramApi);      
        }

        [HttpGet]
        [Route("teams/season/{seasonId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("seasons/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("squad/season/{seasonId}/team/{teamId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("topscorers/season/{seasonId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
        [Route("standings/season/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public  object standings(long id,  string api_token  = null, string include = null)
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
            
            IRestResponse response = DataCallback.Callback(apiref, ParamApi, _repository);
            
            if (!response.IsSuccessful)            
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden,response.Content);
            else
               return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK,response.Content);
        }
    }
}