using Biblioteca.Application.Contract;
using Biblioteca.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("Retorna uma lista com todos os livros")]
        public async Task<ActionResult<List<BookDto>>> GetAll()
        {
            var books = await _bookService.Get();
            return Ok(books);
        }

        [HttpGet("Retorna um livro por um ID especifico")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _bookService.Get(id);
            if (book == null)
                return NotFound("Book not found");

            return Ok(book);
        }

        [HttpPost("Adiciona um livro")]
        public async Task<ActionResult<BookDto>> Create(BookDto bookDto)
        {
            var createdBook = await _bookService.Create(bookDto);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("Atualiza os dados de um livro especifico")]
        public async Task<IActionResult> Update(int id, BookDto bookDto)
        {
            if (bookDto.Id != id)
                return BadRequest("Mismatched ID");

            var success = await _bookService.Update(bookDto);
            if (!success)
                return NotFound("Book not found");

            return NoContent();
        }

        [HttpDelete("Remove um livro especifico")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.Delete(id);
            if (deleted == null || deleted == false)
                return NotFound("Book not found");

            return NoContent();
        }
    }
}
