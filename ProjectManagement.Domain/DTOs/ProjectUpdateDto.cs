using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Domain.DTOs
{
    public class ProjectUpdateDto
    {
        [Required(ErrorMessage = "Project ID is required.")]
        public string Id { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime? EndDate { get; set; }

        public List<string>? AssignedUserIds { get; set; }
    }
}
