using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PD411_Books.DAL;
using PD411_Books.DAL.Entities;

namespace PD411_Books.API.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var authors = await _context.Authors.ToListAsync();

            return Ok(authors);
        }   

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]AuthorEntity entity)
        {
            await _context.Authors.AddAsync(entity);
            int res = await _context.SaveChangesAsync();
            if(res == 0)
            {
                return BadRequest("Не вдалося створити автора");
            }

            return Ok($"Автор '{entity.Name}' успішно створений");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            return Ok();
        }
    }
}
