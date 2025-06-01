// Biblioteca.API/Controllers/BookController.cs

using Biblioteca.Application.Contract;
using Biblioteca.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    /// Retorna uma lista com todos os livros cadastrados.
    /// </summary>
    /// <returns>Lista de livros.</returns>
    [HttpGet] 
    public async Task<ActionResult<List<BookDto>>> GetAll()
    {
        var books = await _bookService.Get();
        return Ok(books);
    }

    /// <summary>
    /// Retorna os dados de um livro específico pelo ID.
    /// </summary>
    /// <param name="id">ID do livro.</param>
    /// <returns>Dados do livro encontrado.</returns>
    [HttpGet("{id}")] 
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await _bookService.Get(id);
        if (book == null)
            return NotFound("Book not found");

        return Ok(book);
    }

    /// <summary>
    /// Adiciona um novo livro.
    /// </summary>
    /// <param name="bookDto">Dados do livro a ser criado.</param>
    /// <returns>Livro criado com sucesso.</returns>
    [HttpPost] 
    public async Task<ActionResult<BookDto>> Create(BookDto bookDto)
    {
        var createdBook = await _bookService.Create(bookDto);
        return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
    }

    /// <summary>
    /// Atualiza os dados de um livro existente.
    /// </summary>
    /// <param name="id">ID do livro a ser atualizado.</param>
    /// <param name="bookDto">Novos dados do livro.</param>
    /// <returns>Status da operação.</returns>
    [HttpPut("{id}")] 
    public async Task<IActionResult> Update(int id, BookDto bookDto)
    {
        if (bookDto.Id != id)
            return BadRequest("Mismatched ID");

        var success = await _bookService.Update(bookDto);
        if (!success)
            return NotFound("Book not found");

        return NoContent();
    }

    /// <summary>
    /// Remove um livro do sistema.
    /// </summary>
    /// <param name="id">ID do livro a ser removido.</param>
    /// <returns>Status da operação.</returns>
    [HttpDelete("{id}")] 
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _bookService.Delete(id);
        if (deleted == null || deleted == false)
            return NotFound("Book not found");

        return NoContent();
    }
}