using Adapters.Abstractions;
using Adapters.Entities;
using AutoMapper;
using BLL.Abstractions.Services;

namespace Adapters
{
    public class SearchAdapter : ISearchAdapter
    {
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;

        public SearchAdapter(ISearchService searchService, IMapper mapper)
        {
            _searchService = searchService;
            _mapper = mapper;
        }

        public async Task<SearchResult> SearchAsync(string searchString)
        {
            return _mapper.Map<SearchResult>(await _searchService.SearchAsync(searchString));
        }
    }
}
