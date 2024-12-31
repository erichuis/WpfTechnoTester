using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class JournalEntry
    {
        public string? Id { get; set; }
        public Guid JournalEntryId { get; set; }

        [Required(ErrorMessage = "Entry can not be empty")]
        public required string Entry { get; set; }

        [Required(ErrorMessage = "Category can not be empty")]
        public required string Category { get; set; }

        [Required(ErrorMessage = "Date can not be empty")]
        public DateTime? DateEntry { get; set; }
    }
}