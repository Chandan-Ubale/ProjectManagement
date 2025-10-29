using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Domain;

public class TaskDto
{
    public string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string AssignedUserId { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = "ToDo";

    [Range(1, 5)]
    public int Priority { get; set; } = 3;

    public DateTime? DueDate { get; set; }
}