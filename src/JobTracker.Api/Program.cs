using JobTracker.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JobTrackerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("JobTracker")
        ?? "Data Source=jobtracker.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<JobTrackerDbContext>();
    if (db.Database.IsRelational())
        db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;
