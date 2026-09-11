using JobTracker.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JobTracker.Api.Tests;

public class ApiFactory : WebApplicationFactory<Program>
{
    private readonly SqliteConnection connection = new("Data Source=:memory:");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        connection.Open();
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<JobTrackerDbContext>();
            services.RemoveAll<DbContextOptions<JobTrackerDbContext>>();
            services.AddDbContext<JobTrackerDbContext>(options =>
                options.UseSqlite(connection));
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing) connection.Dispose();
    }
}
