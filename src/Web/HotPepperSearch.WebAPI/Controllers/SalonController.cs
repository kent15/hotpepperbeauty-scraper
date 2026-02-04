using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotPepperSearch.WebAPI.Controllers;

[ApiController]
[Route("api/salons")]
public class SalonController : ControllerBase
{
    private readonly ISalonSearchService _searchService;
    private readonly ISalonSortService _sortService;
    private readonly ISearchHistoryService _historyService;

    public SalonController(
        ISalonSearchService searchService,
        ISalonSortService sortService,
        ISearchHistoryService historyService)
    {
        _searchService = searchService;
        _sortService = sortService;
        _historyService = historyService;
    }

    [HttpPost("search")]
    public Task<IActionResult> SearchAsync([FromBody] SearchRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:guid}")]
    public Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost("sort")]
    public Task<IActionResult> SortAsync([FromBody] SortRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("history")]
    public Task<IActionResult> GetHistoryAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
