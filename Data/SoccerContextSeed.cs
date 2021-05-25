using System;
using Soccer.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Soccer.Data
{

    public class SoccerContextSeed {
        public static void SeedData(IMongoCollection<Content> contentCollection)
        {
            bool existContent = contentCollection.Find(p => true).Any();
            if (!existContent)
            {
                contentCollection.InsertManyAsync(getMyContents());
            }
        }

        private static IEnumerable<Content> getMyContents()
        {  
            return new List<Content>()
            {
                new Content()
                {
                    Id = "312840823048048f1",
                    Name = "stands",
                    jsonFile = "[]",
                    LastUpdate = DateTime.Now
                },
                new Content()
                {
                    Id = "312840823048048f2",
                    Name = "Matches",
                    jsonFile = "[]",
                    LastUpdate = DateTime.Now
                }
            };
        }
    }
}