using System.ComponentModel.DataAnnotations;

namespace PD411_Books.BLL.Dtos.Author
{
    public class UpdateAuthorDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.UtcNow;
        public string? Image { get; set; }
    }
}
