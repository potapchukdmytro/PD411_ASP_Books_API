namespace PD411_Books.DAL.Entities
{
    public class GenreEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<BookEntity> Books { get; set; } = [];
    }
}
