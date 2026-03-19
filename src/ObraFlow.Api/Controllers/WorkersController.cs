using Microsoft.AspNetCore.Mvc;
using ObraFlow.Application.Abstractions;
using ObraFlow.Application.DTOs.Workers;

namespace ObraFlow.Api.Controllers;

[ApiController]
[Route("workers")]
public class WorkersController : ControllerBase
{
    private readonly IWorkerService _workerService;

    public WorkersController(IWorkerService workerService)
    {
        _workerService = workerService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WorkerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WorkerDto>>> GetAll(CancellationToken cancellationToken)
    {
        var workers = await _workerService.GetAllAsync(cancellationToken);
        return Ok(workers);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WorkerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkerDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var worker = await _workerService.GetByIdAsync(id, cancellationToken);
        if (worker is null)
        {
            return NotFound();
        }

        return Ok(worker);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WorkerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkerDto>> Create([FromBody] CreateWorkerDto dto, CancellationToken cancellationToken)
    {
        var createdWorker = await _workerService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = createdWorker.Id }, createdWorker);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(WorkerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkerDto>> Update(Guid id, [FromBody] UpdateWorkerDto dto, CancellationToken cancellationToken)
    {
        var updatedWorker = await _workerService.UpdateAsync(id, dto, cancellationToken);
        if (updatedWorker is null)
        {
            return NotFound();
        }

        return Ok(updatedWorker);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _workerService.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
