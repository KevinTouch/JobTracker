using JobTracker.Api.Data;
using JobTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobApplicationsController(JobTrackerDbContext db) : ControllerBase
{
    private static readonly string[] ValidStatuses = ["Wishlist", "Applied", "Interview", "Offer", "Rejected", "Withdrawn"];

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobApplication>>> GetAll(
        [FromQuery] string? status,
        [FromQuery] string? company,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 25,
        CancellationToken cancellationToken = default)
    {
        if (page < 1 || pageSize is < 1 or > 100)
            return BadRequest("page must be at least 1 and pageSize must be between 1 and 100.");

        var query = db.JobApplications.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(x => x.Status == status);
        if (!string.IsNullOrWhiteSpace(company))
            query = query.Where(x => x.Company.Contains(company));

        var applications = await query
            .OrderByDescending(x => x.ApplicationDate)
            .ThenBy(x => x.Company)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return Ok(applications);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<JobApplication>> Get(int id, CancellationToken cancellationToken)
    {
        var application = await db.JobApplications.FindAsync([id], cancellationToken);
        return application is null ? NotFound() : Ok(application);
    }

    [HttpPost]
    public async Task<ActionResult<JobApplication>> Create(JobApplicationRequest request, CancellationToken cancellationToken)
    {
        if (!ValidStatuses.Contains(request.Status, StringComparer.OrdinalIgnoreCase))
            ModelState.AddModelError(nameof(request.Status), $"Status must be one of: {string.Join(", ", ValidStatuses)}.");
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var application = ToEntity(request);
        db.JobApplications.Add(application);
        await db.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = application.Id }, application);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<JobApplication>> Update(int id, JobApplicationRequest request, CancellationToken cancellationToken)
    {
        var application = await db.JobApplications.FindAsync([id], cancellationToken);
        if (application is null) return NotFound();
        if (!ValidStatuses.Contains(request.Status, StringComparer.OrdinalIgnoreCase))
            ModelState.AddModelError(nameof(request.Status), $"Status must be one of: {string.Join(", ", ValidStatuses)}.");
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        application.Company = request.Company;
        application.JobTitle = request.JobTitle;
        application.Status = NormalizeStatus(request.Status);
        application.ApplicationDate = request.ApplicationDate!.Value;
        application.JobUrl = request.JobUrl;
        application.Notes = request.Notes;
        application.Location = request.Location;
        application.WorkArrangement = request.WorkArrangement;
        application.SalaryRange = request.SalaryRange;
        application.ContactName = request.ContactName;
        application.InterviewDate = request.InterviewDate;
        application.NextActionDate = request.NextActionDate;
        application.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return Ok(application);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var application = await db.JobApplications.FindAsync([id], cancellationToken);
        if (application is null) return NotFound();
        db.JobApplications.Remove(application);
        await db.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    private static JobApplication ToEntity(JobApplicationRequest request) => new()
    {
        Company = request.Company.Trim(), JobTitle = request.JobTitle.Trim(), Status = NormalizeStatus(request.Status),
        ApplicationDate = request.ApplicationDate!.Value, JobUrl = request.JobUrl, Notes = request.Notes,
        Location = request.Location, WorkArrangement = request.WorkArrangement, SalaryRange = request.SalaryRange,
        ContactName = request.ContactName, InterviewDate = request.InterviewDate, NextActionDate = request.NextActionDate,
        CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow
    };

    private static string NormalizeStatus(string status) => ValidStatuses.First(x => x.Equals(status, StringComparison.OrdinalIgnoreCase));
}
