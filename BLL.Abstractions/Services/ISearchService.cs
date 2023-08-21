using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface ISearchService
    {
        Task<SearchResult> SearchAsync(string searchString);
    }
}
