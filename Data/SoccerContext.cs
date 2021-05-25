using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Soccer.Entities;

namespace Soccer.Data
{

    public class SoccerContext : ISoccerContext
    {
        public SoccerContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Contents = database.GetCollection<Content>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            SoccerContextSeed.SeedData(Contents);
        }
        public IMongoCollection<Content> Contents {get;}

    }
}