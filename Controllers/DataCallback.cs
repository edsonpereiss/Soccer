using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Soccer.Entities;
using Soccer.Extensions;
using Soccer.Repositories;

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
        public static IRestResponse CallBack(ApiRef apiref, ParamApi ParamApi, IContentRepository repository)
        {
            string querys = ToQueryString(ParamApi.Qry);
            string path = ToParamRstString(ParamApi.Rst, Config.GetConfig(apiref.ToString() + ":path", false));
            string uri =  Config.GetConfig("URL_BASE", false) + path + querys;
            int executionTimeout = Convert.ToInt32(Config.GetConfig(apiref.ToString() + ":executionTimeout", false));
            return CallContent(apiref, uri, executionTimeout, repository);
        }

        private static IRestResponse CallContent(ApiRef apiref, string uri, int executionTimeout, IContentRepository repository)
        {
            bool call = false;
            Content content = repository.GetContentByName(uri).Result;
            bool contentExist = content is not null;
            IRestResponse response = new RestResponse();;

            if (contentExist)
            {
                DateTime lastUpdate = content.LastUpdate;
                DateTime currentUpdate = lastUpdate.AddSeconds(executionTimeout);
                call = currentUpdate < DateTime.UtcNow;
            }
            else
            {
                call = true;
            }

            if (call)
            {
                var client = new RestClient(uri);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    if (contentExist)
                    {
                        response.Content = content.jsonFile;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    return response;
                }                
            }

            if ((call) && (contentExist))
            {
                content.jsonFile = response.Content;
                content.LastUpdate = DateTime.UtcNow;
                repository.UpadateContent(content);
            }
            else if ((call) && (!contentExist))
            {
                content = new Content();
                content.Name = uri;
                content.jsonFile = response.Content;
                content.LastUpdate = System.DateTime.UtcNow; 
                repository.CreateContent(content);
            }
            
            response.Content = content.jsonFile;
            response.StatusCode = System.Net.HttpStatusCode.OK;
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