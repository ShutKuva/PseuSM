using Adapters.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PseuSM.Entities;

namespace PseuSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchAdapter _searchAdapter;
        private readonly IMapper _mapper;

        public SearchController(ISearchAdapter searchAdapter, IMapper mapper)
        {
            _searchAdapter = searchAdapter;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<OkObjectResult> Search([FromQuery]string search)
        {
            return Ok(_mapper.Map<SearchResult>(await _searchAdapter.SearchAsync(search)));
        }
    }
}
