using PD411_Books.DAL.Entities;

namespace PD411_Books.DAL.Repositories
{
    public class AuthorRepository : GenericRepository<AuthorEntity>
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
