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
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return Callback(ApiRef.countries, paramApi);    
        }

        [HttpGet]
        [Route("v.1/api/leagues")]
        public new object leagues(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return Callback(ApiRef.leagues, paramApi);    
        }

        [HttpGet]
        [Route("v.1/api/leagues/{id}")]
        public new object leagueById(long id, string api_token  = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            paramApi.Rst.Add("id", id.ToString());

            return Callback(ApiRef.leagueById, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/fixtures/{id}")]
        public new object fixturesForDate(long id, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("id", id.ToString());

            return Callback(ApiRef.fixturesById, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/fixtures/date/{date}")]
        public new object fixturesForDate(string date, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("date", date);

            return Callback(ApiRef.fixturesForDate, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/fixtures/between/{from}/{to}")]
        public new object fixturesBetweenDates(string from, string to, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("from", from);
            paramApi.Rst.Add("to", to);

            return Callback(ApiRef.fixturesBetweenDates, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/fixtures/multi/{ids}")]
        public new object particularFixtures(string ids, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("ids", ids);

            return Callback(ApiRef.particularFixtures, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/commentaries/fixture/{id}")]
        public new object commentary(long id, string api_token  = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            paramApi.Rst.Add("id", id.ToString());

            return Callback(ApiRef.commentary, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/players/{id}")]
        public new object player(long id, string api_token  = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            paramApi.Rst.Add("id", id.ToString());

            return Callback(ApiRef.player, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/head2head/{team1id}/{team2id}")]
        public new object h2h(long team1id, long team2id, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("team1id", team1id.ToString());
            paramApi.Rst.Add("team2id", team2id.ToString());

            return Callback(ApiRef.h2h, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/teams/{id}")]
        public new object teamById(long id, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("id", id.ToString());

            return Callback(ApiRef.teamById, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/livescores")]
        public new object livescores(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return Callback(ApiRef.livescores, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/livescores/now")]
        public new object livescoresNow(string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            return Callback(ApiRef.livescoresNow, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/teams/season/{seasonId}")]
        public new object seasonTeams(long seasonId, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("seasonId", seasonId.ToString());

            return Callback(ApiRef.seasonTeams, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/seasons/{id}")]
        public new object seasonResults(long id, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("id", id.ToString());

            return Callback(ApiRef.seasonResults, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/squad/season/{seasonId}/team/{teamId}")]
        public new object seasonSquad(long seasonId, long teamId, string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("seasonId", seasonId.ToString());
            paramApi.Rst.Add("teamId", teamId.ToString());

            return Callback(ApiRef.seasonSquad, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/topscorers/season/{seasonId}")]
        public new object seasonTopPlayers(long seasonId,  string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("seasonId", seasonId.ToString());

            return Callback(ApiRef.seasonTopPlayers, paramApi);      
        }

        [HttpGet]
        [Route("v.1/api/standings/season/{id}")]
        public new object standings(long id,  string api_token  = null, string include = null)
        {
            ParamApi paramApi = new ParamApi();
            paramApi.Qry.Add("api_token", api_token);

            if (!string.IsNullOrEmpty(include))
                paramApi.Qry.Add("include", include);

            paramApi.Rst.Add("id", id.ToString());

            return Callback(ApiRef.standings, paramApi);      
        }


    }
}