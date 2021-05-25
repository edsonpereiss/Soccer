using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Soccer.Extensions;

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

    public class DataCallback  
    {
        //protected IConfiguration _iconfiguration;

        public DataCallback(/*IConfiguration iconfiguration*/)
        {
            //_iconfiguration = iconfiguration;                   
        }

        public static IRestResponse Callback(ApiRef apiref, ParamApi ParamApi)
        {
            string querys = ToQueryString(ParamApi.Qry);
            string path = ToParamRstString(ParamApi.Rst, Config.GetConfig(apiref.ToString(), false));
            string uri =  Config.GetConfig("URL_BASE", false) +
                          path +
                          querys;
            
            var client = new RestClient(uri);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }

        private static string ToQueryString(NameValueCollection qry)
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


       private static string ToParamRstString(NameValueCollection rst, string path)
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