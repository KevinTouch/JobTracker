namespace JobTracker.Api.Models;

public class JobApplication
{
    public int Id { get; set; }
    public string Company { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string Status { get; set; } = "Applied";
    public DateOnly ApplicationDate { get; set; }
    public string? JobUrl { get; set; }
    public string? Notes { get; set; }
}
