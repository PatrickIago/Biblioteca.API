using Biblioteca.Application.DTO;

namespace Biblioteca.Application.Contract;
public interface IAuthorService
{
    Task<List<AuthorDto>> Get();
    Task<AuthorDto> Get(int id);
    Task<AuthorDto> Create(AuthorDto authorDto);
    Task<bool> Update(AuthorDto authorDto);
    Task<bool> Delete(int id);
}
