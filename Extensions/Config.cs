using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
namespace Soccer.Extensions
{
    public class Config
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public Config(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public static string GetConfig(string key, bool environment = true)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                if (environment)
                {
                    string EnvironmentDefault = config["EnvironmentDefault"];
                    return config["Environment:" + EnvironmentDefault + ":" + key];
                }
                else
                {
                    return config[key];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}