namespace ProjectManagement.Domain;

public class ProjectDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public List<string>? AssignedUserIds { get; set; }

    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
}