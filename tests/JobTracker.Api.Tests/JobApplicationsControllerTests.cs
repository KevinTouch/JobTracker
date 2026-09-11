using JobTracker.Api.Controllers;
using JobTracker.Api.Data;
using JobTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Api.Tests;

public class JobApplicationsControllerTests
{
    [Fact]
    public async Task CreateAndList_returns_saved_application()
    {
        await using var db = CreateDb();
        var controller = new JobApplicationsController(db);

        var created = await controller.Create(new JobApplicationRequest
        {
            Company = "Acme", JobTitle = "Engineer", Status = "applied",
            ApplicationDate = new DateOnly(2026, 9, 11)
        }, CancellationToken.None);

        var result = Assert.IsType<CreatedAtActionResult>(created.Result);
        var entity = Assert.IsType<JobApplication>(result.Value);
        Assert.Equal(1, entity.Id);
        var list = await controller.GetAll(null, null, null, null, 1, 25, CancellationToken.None);
        Assert.Single(Assert.IsType<OkObjectResult>(list.Result).Value as IEnumerable<JobApplication> ?? []);
    }

    [Fact]
    public async Task Create_rejects_unknown_status()
    {
        await using var db = CreateDb();
        var controller = new JobApplicationsController(db);

        var result = await controller.Create(new JobApplicationRequest
        {
            Company = "Acme", JobTitle = "Engineer", Status = "Unknown",
            ApplicationDate = DateOnly.FromDateTime(DateTime.UtcNow)
        }, CancellationToken.None);

        Assert.IsType<ObjectResult>(result.Result);
        Assert.Empty(await db.JobApplications.ToListAsync());
    }

    [Fact]
    public async Task Delete_removes_existing_application()
    {
        await using var db = CreateDb();
        db.JobApplications.Add(new JobApplication { Company = "Acme", JobTitle = "Engineer", ApplicationDate = DateOnly.FromDateTime(DateTime.UtcNow) });
        await db.SaveChangesAsync();
        var controller = new JobApplicationsController(db);

        var result = await controller.Delete(1, CancellationToken.None);

        Assert.IsType<NoContentResult>(result);
        Assert.Empty(await db.JobApplications.ToListAsync());
    }

    private static JobTrackerDbContext CreateDb() => new(new DbContextOptionsBuilder<JobTrackerDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
}
