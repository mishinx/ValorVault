using Microsoft.AspNetCore.Mvc;
using ValorVault.Models;
using ValorVault.Services;

namespace ValorVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoldierInfo>>> SearchByName(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest("Keyword cannot be null or empty.");
            }

            var soldiers = await _searchService.SearchByNameAsync(keyword);

            return Ok(soldiers);
        }
    }
}
