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
    public string? Location { get; set; }
    public string? WorkArrangement { get; set; }
    public string? SalaryRange { get; set; }
    public string? ContactName { get; set; }
    public DateOnly? InterviewDate { get; set; }
    public DateOnly? NextActionDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
