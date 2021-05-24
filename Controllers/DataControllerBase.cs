using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Soccer.Controllers
{
    public class ParamApi
    {
        public NameValueCollection Qry { get; set; }
        public NameValueCollection Rst { get; set; }
        public ParamApi()
        {
            Qry = new NameValueCollection();
            Rst = new NameValueCollection();
        }

    }
     public enum ApiRef
     {
        countries,
        leagues,
        leagueById,
        standings,
        teamById,
        livescores,
        livescoresNow,
        fixturesByIdLive,
        seasonTeams,
        seasonResults,
        seasonSquad,
        seasonTopPlayers,
        fixturesById,
        fixturesBetweenDates,
        fixturesForDate,
        particularFixtures,
        commentary,
        player,
        h2h
    }

    public class DataControllerBase : ControllerBase 
    {
        protected IConfiguration _iconfiguration;
        protected readonly string error_api_token = "{ \"error\": { \"message\": \"Unauthenticated\", \"code\": 403 }}";
        protected readonly string API_KEY;
        protected readonly string URL_BASE;
        protected readonly string countries;
        protected readonly string leagues;
        protected readonly string leagueById;
        protected readonly string standings;
        protected readonly string teamById;
        protected readonly string livescores;
        protected readonly string livescoresNow;
        protected readonly string fixturesByIdLive;
        protected readonly string seasonTeams;
        protected readonly string seasonMatches;
        protected readonly string seasonSquad;
        protected readonly string seasonTopPlayers;
        protected readonly string fixturesById;
        protected readonly string fixturesBetweenDates;
        protected readonly string fixturesForDate;
        protected readonly string particularFixtures;
        protected readonly string commentary;
        protected readonly string player;
        protected readonly string h2h;

        public DataControllerBase(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;                     
        }

        public object Callback(ApiRef apiref, ParamApi ParamApi)
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
            
            string querys = ToQueryString(ParamApi.Qry);
            string path = ToParamRstString(ParamApi.Rst, _iconfiguration.GetSection(apiref.ToString()).Value);
            string uri =  _iconfiguration.GetSection("URL_BASE").Value +
                          path +
                          querys;
            
            var client = new RestClient(uri);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            
            if (!response.IsSuccessful)            
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden,response.Content);
            else
               return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK,response.Content);

        }

        private string ToQueryString(NameValueCollection qry)
        {
            var array = (
                from key in qry.AllKeys
                from value in qry.GetValues(key)
                select string.Format(
                "{0}={1}",
                    HttpUtility.UrlEncode(key),
                    HttpUtility.UrlEncode(value))
            ).ToArray();
        return "?" + string.Join("&", array);
        }


       private string ToParamRstString(NameValueCollection rst, string path)
        {
            string tmp_path = path;
            foreach (var item in rst.AllKeys)
            {
             string[] value = rst.GetValues(item);
               tmp_path = tmp_path.Replace("{" + item + "}", value[0]);     
            }
            return tmp_path;
        }

    }
    

}