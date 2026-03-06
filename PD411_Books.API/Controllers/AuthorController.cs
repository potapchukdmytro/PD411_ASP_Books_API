using Microsoft.AspNetCore.Mvc;
using PD411_Books.API.Extensions;
using PD411_Books.BLL.Dtos.Author;
using PD411_Books.BLL.Services;

namespace PD411_Books.API.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _authorService.GetAllAsync();
            return this.GetAction(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _authorService.GetByIdAsync(id);
            return this.GetAction(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAuthorDto dto)
        {
            var response = await _authorService.CreateAsync(dto);
            return this.GetAction(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAuthorDto dto)
        {
            var response = await _authorService.UpdateAsync(dto);
            return this.GetAction(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _authorService.DeleteAsync(id);
            return this.GetAction(response);
        }
    }
}
