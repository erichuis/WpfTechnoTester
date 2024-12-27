using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class TodoItem
    {
        public string? Id { get; set; }
        public Guid TodoItemId { get; set; }

        [Required(ErrorMessage = "Title can not be empty")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Description can not be empty")]
        public required string Description { get; set; }

        public DateTime? DateStarted { get; set; }

        public DateTime? DateCompleted { get; set; }

        [Range(1, 100, ErrorMessage = "Value must be between 1 and 100.")]
        public int InProgress { get; set; }

    }
}