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
}
