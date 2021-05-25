using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soccer.Entities;

namespace Soccer.Repositories
{

    public interface IContentRepository
    {
        Task<IEnumerable<Content>> GetContents();
        Task<Content> GetContent(string id);
        Task<IEnumerable<Content>> GetContentByName( string name);

        Task CreateContent(Content content);
        Task<bool> UpadateContent(Content content);
        Task<bool> DeleteContent(string id);        
    }
}