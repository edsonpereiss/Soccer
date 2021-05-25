using Soccer.Entities;
using MongoDB.Driver;

namespace Soccer.Data
{

    public interface ISoccerContext
    {
        IMongoCollection<Entities.Content> Contents {get;}
    }
}