using AutoMapper;
using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Domain.ValueObjects;
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
    private readonly IMapper _mapper;
    private readonly ILogger<SalonController> _logger;

    public SalonController(
        ISalonSearchService searchService,
        ISalonSortService sortService,
        ISearchHistoryService historyService,
        IMapper mapper,
        ILogger<SalonController> logger)
    {
        _searchService = searchService;
        _sortService = sortService;
        _historyService = historyService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchAsync([FromBody] SearchRequestDto request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Search request received: {@Request}", request);

        var searchCondition = _mapper.Map<SearchCondition>(request);

        var result = await _searchService.SearchSalonsByConditionAsync(searchCondition, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogWarning("Search failed: {ErrorMessage}", result.ErrorMessage);
            return BadRequest(new { Error = result.ErrorMessage });
        }

        var salons = result.Value!.ToList();
        var response = _mapper.Map<List<SalonResponseDto>>(salons);

        _logger.LogInformation("Returning {Count} salons", response.Count);
        return Ok(response);
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
