using HotPepperSearch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotPepperSearch.WebAPI.Controllers;

[ApiController]
[Route("api/master")]
public class MasterDataController : ControllerBase
{
    private readonly IMasterDataService _masterDataService;

    public MasterDataController(IMasterDataService masterDataService)
    {
        _masterDataService = masterDataService;
    }

    [HttpGet("genres")]
    public Task<IActionResult> GetGenresAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("prefectures")]
    public Task<IActionResult> GetPrefecturesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("cities/{prefId}")]
    public Task<IActionResult> GetCitiesAsync(string prefId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
