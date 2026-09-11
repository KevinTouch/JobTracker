using System.Net;
using System.Net.Http.Json;
using JobTracker.Api.Models;

namespace JobTracker.Api.Tests;

public class JobApplicationsApiTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private readonly HttpClient client = factory.CreateClient();

    [Fact]
    public async Task Crud_lifecycle_works_over_http()
    {
        var request = new JobApplicationRequest
        {
            Company = "Acme", JobTitle = "Engineer", Status = "Applied",
            ApplicationDate = new DateOnly(2026, 9, 11)
        };

        var create = await client.PostAsJsonAsync("/api/JobApplications", request);
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var created = await create.Content.ReadFromJsonAsync<JobApplication>();
        Assert.NotNull(created);

        var get = await client.GetAsync($"/api/JobApplications/{created!.Id}");
        Assert.Equal(HttpStatusCode.OK, get.StatusCode);

        request.Status = "Interview";
        var update = await client.PutAsJsonAsync($"/api/JobApplications/{created.Id}", request);
        Assert.Equal(HttpStatusCode.OK, update.StatusCode);

        var delete = await client.DeleteAsync($"/api/JobApplications/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync($"/api/JobApplications/{created.Id}")).StatusCode);
    }

    [Fact]
    public async Task Invalid_status_returns_bad_request()
    {
        var response = await client.PostAsJsonAsync("/api/JobApplications", new JobApplicationRequest
        {
            Company = "Acme", JobTitle = "Engineer", Status = "Unknown",
            ApplicationDate = DateOnly.FromDateTime(DateTime.UtcNow)
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
