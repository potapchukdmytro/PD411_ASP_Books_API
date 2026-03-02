namespace PD411_Books.DAL.Entities
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.UtcNow;
        public string? Image { get; set; }

        public List<BookEntity> Books { get; set; } = [];
    }
}
    