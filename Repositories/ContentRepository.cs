using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Soccer.Data;
using Soccer.Entities;

namespace Soccer.Repositories
{

    public class ContentRepository: IContentRepository
    {
        private readonly ISoccerContext _context;
        public ContentRepository(ISoccerContext context)
        {
            _context = context;
        }

        public async Task CreateContent(Content content)
        {
            await _context.Contents.InsertOneAsync(content);
        }

        public async Task<bool> DeleteContent(string id)
        {
            FilterDefinition<Content> filter = Builders<Content>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context.Contents.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;            
        }

        public async Task<Content> GetContent(string id)
        {
            return await _context.Contents.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Content>> GetContentByName(string name)
        {
            FilterDefinition<Content> filter = Builders<Content>.Filter.Eq(p => p.Name, name);
            return await _context.Contents.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Content>> GetContents()
        {
            return await _context.Contents.Find(p=> true).ToListAsync();
        }

        public async Task<bool> UpadateContent(Content content)
        {
            var updateResult = await _context.Contents.ReplaceOneAsync(
                filter: g => g.Id == content.Id, replacement: content);

            return updateResult.IsAcknowledged 
                && updateResult.ModifiedCount > 0;            
        }
    }
}