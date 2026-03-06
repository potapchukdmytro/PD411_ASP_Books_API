using Microsoft.EntityFrameworkCore;
using PD411_Books.BLL.Dtos.Author;
using PD411_Books.DAL.Entities;
using PD411_Books.DAL.Repositories;

namespace PD411_Books.BLL.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorService(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ServiceResponse> CreateAsync(CreateAuthorDto dto)
        {
            var entity = new AuthorEntity
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Image = dto.Image,
            };

            bool res = await _authorRepository.CreateAsync(entity);

            if (!res)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Не вдалося додати автора"
                };
            }

            return new ServiceResponse
            {
                Message = $"Автор '{entity.Name}' успішно доданий",
                Payload = new AuthorDto
                {
                    Id = entity.Id,
                    BirthDate = entity.BirthDate,
                    Image = entity.Image,
                    Name = entity.Name
                }
            };
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateAuthorDto dto)
        {
            var entity = await _authorRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Автора з id {dto.Id} не існує"
                };
            }

            string oldName = entity.Name;
            entity.Name = dto.Name;
            entity.BirthDate = dto.BirthDate;
            entity.Image = dto.Image;

            bool res = await _authorRepository.UpdateAsync(entity);

            if (!res)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Не вдалося оновити автора"
                };
            }

            return new ServiceResponse
            {
                Message = $"Автор '{oldName}' успішно оновлений",
                Payload = new AuthorDto
                {
                    Id = entity.Id,
                    BirthDate = entity.BirthDate,
                    Image = entity.Image,
                    Name = entity.Name
                }
            };
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await _authorRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Автора з id {id} не існує"
                };
            }

            bool res = await _authorRepository.DeleteAsync(entity);

            if (!res)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Не вдалося видалити автора"
                };
            }

            return new ServiceResponse
            {
                Message = $"Автор '{entity.Name}' успішно видалений",
                Payload = new AuthorDto
                {
                    Id = entity.Id,
                    BirthDate = entity.BirthDate,
                    Image = entity.Image,
                    Name = entity.Name
                }
            };
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _authorRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Автора з id {id} не існує"
                };
            }

            return new ServiceResponse
            {
                Message = "Автор успішно отриманий",
                Payload = new AuthorDto
                {
                    Id = entity.Id,
                    BirthDate = entity.BirthDate,
                    Image = entity.Image,
                    Name = entity.Name
                }
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var dtos = await _authorRepository.Authors
                .Select(a => new AuthorDto { Name = a.Name, BirthDate = a.BirthDate, Id = a.Id, Image = a.Image })
                .ToListAsync();
            return new ServiceResponse 
            {
                Message = "Автори отримано",
                Payload = dtos
            };
        }
    }
}
