using Adapters.Entities;

namespace Adapters.Abstractions
{
    public interface ISearchAdapter
    {
        Task<SearchResult> SearchAsync(string searchString);
    }
}
