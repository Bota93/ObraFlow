using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using ObraFlow.Application.Abstractions;
using ObraFlow.Application.DTOs.DailyReports;

namespace ObraFlow.Api.Controllers;

[ApiController]
[Route("daily-reports")]
public class DailyReportsController : ControllerBase
{
    private readonly IDailyReportService _dailyReportService;

    public DailyReportsController(IDailyReportService dailyReportService)
    {
        _dailyReportService = dailyReportService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DailyReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DailyReportDto>>> GetAll(CancellationToken cancellationToken)
    {
        var reports = await _dailyReportService.GetAllAsync(cancellationToken);
        return Ok(reports);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DailyReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DailyReportDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var report = await _dailyReportService.GetByIdAsync(id, cancellationToken);
        if (report is null)
        {
            return NotFound();
        }

        return Ok(report);
    }

    [HttpPost]
    [EnableRateLimiting("DemoCreateWrites")]
    [ProducesResponseType(typeof(DailyReportDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public async Task<ActionResult<DailyReportDto>> Create([FromBody] CreateDailyReportDto dto, CancellationToken cancellationToken)
    {
        var createdReport = await _dailyReportService.CreateAsync(dto, cancellationToken);
        if (createdReport is null)
        {
            ModelState.AddModelError(nameof(dto.WorkerId), "The specified worker does not exist.");
            return ValidationProblem(ModelState);
        }

        return CreatedAtAction(nameof(GetById), new { id = createdReport.Id }, createdReport);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DailyReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DailyReportDto>> Update(Guid id, [FromBody] UpdateDailyReportDto dto, CancellationToken cancellationToken)
    {
        var existingReport = await _dailyReportService.GetByIdAsync(id, cancellationToken);
        if (existingReport is null)
        {
            return NotFound();
        }

        var updatedReport = await _dailyReportService.UpdateAsync(id, dto, cancellationToken);
        if (updatedReport is null)
        {
            ModelState.AddModelError(nameof(dto.WorkerId), "The specified worker does not exist.");
            return ValidationProblem(ModelState);
        }

        return Ok(updatedReport);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _dailyReportService.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
