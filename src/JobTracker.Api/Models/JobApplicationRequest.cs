using System.ComponentModel.DataAnnotations;

namespace JobTracker.Api.Models;

public class JobApplicationRequest
{
    [Required, StringLength(200)] public string Company { get; set; } = string.Empty;
    [Required, StringLength(200)] public string JobTitle { get; set; } = string.Empty;
    [Required, StringLength(50)] public string Status { get; set; } = "Applied";
    [Required] public DateOnly? ApplicationDate { get; set; }
    [Url, StringLength(2048)] public string? JobUrl { get; set; }
    [StringLength(5000)] public string? Notes { get; set; }
    [StringLength(200)] public string? Location { get; set; }
    [StringLength(30)] public string? WorkArrangement { get; set; }
    [StringLength(100)] public string? SalaryRange { get; set; }
    [StringLength(200)] public string? ContactName { get; set; }
    public DateOnly? InterviewDate { get; set; }
    public DateOnly? NextActionDate { get; set; }
}
