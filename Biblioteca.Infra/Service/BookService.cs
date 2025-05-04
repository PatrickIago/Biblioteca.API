using AutoMapper;
using Biblioteca.Application.Contract;
using Biblioteca.Application.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Infra.Data;
using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Infra.Service;

public class BookService : IBookService
{
    private readonly BibliotecaDbContext _context;
    private readonly IMapper _mapper;

    public BookService(BibliotecaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<BookDto> Create(BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return _mapper.Map<BookDto>(book);
    }

    public async Task<bool?> Delete(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book == null)
            return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<BookDto>> Get()
    {
        var books = await _context.Books.ToListAsync();
        return _mapper.Map<List<BookDto>>(books);
    }

    public async Task<BookDto> Get(int id)
    {
       var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
            return null;

        return _mapper.Map<BookDto>(book);
    }

    public async Task<bool> Update(BookDto bookDto)
    {
        var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookDto.Id);
        if (existingBook == null)
            return false;

        _mapper.Map(bookDto, existingBook); 

        _context.Books.Update(existingBook);
        await _context.SaveChangesAsync();

        return true;
    }

}
