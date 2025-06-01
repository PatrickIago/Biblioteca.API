using AutoMapper;
using Biblioteca.Application.Contract;
using Biblioteca.Application.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infra.Service;
public class AuthorService : IAuthorService
{
    private readonly BibliotecaDbContext _context;
    private readonly IMapper _mapper;

    public AuthorService(BibliotecaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuthorDto> Create(AuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);

        // Associa os livros apenas com base nos IDs
        if (authorDto.BookIds != null && authorDto.BookIds.Any())
        {
            var books = await _context.Books
                .Where(b => authorDto.BookIds.Contains(b.Id))
                .ToListAsync();

            author.Books = books;
        }

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return _mapper.Map<AuthorDto>(author);
    }

    public async Task<bool> Delete(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author == null)
            return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<AuthorDto>> Get()
    {
        var authors = await _context.Authors
            .Include(a => a.Books)
            .ToListAsync();
        return _mapper.Map<List<AuthorDto>>(authors);
    }

    public async Task<AuthorDto> Get(int id)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (author == null)
            return null;

        return _mapper.Map<AuthorDto>(author);
    }

    public async Task<bool> Update(AuthorDto authorDto)
    {
        var existingAuthor = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == authorDto.Id);

        if (existingAuthor == null)
            return false;

        // Atualiza propriedades básicas
        existingAuthor.Name = authorDto.Name;
        existingAuthor.Age = authorDto.Age;

        // Atualiza os livros associados
        if (authorDto.BookIds != null)
        {
            var books = await _context.Books
                .Where(b => authorDto.BookIds.Contains(b.Id))
                .ToListAsync();

            existingAuthor.Books = books;
        }

        await _context.SaveChangesAsync();
        return true;
    }
}
