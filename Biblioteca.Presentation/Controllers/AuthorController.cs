using Biblioteca.Application.Contract;
using Biblioteca.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

/// <summary>
/// Controlador responsável pelas operações relacionadas a autores.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    /// <summary>
    /// Retorna todos os autores cadastrados.
    /// </summary>
    /// <returns>Lista de autores</returns>
    /// <response code="200">Autores encontrados com sucesso</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AuthorDto>>> GetAll()
    {
        var authors = await _authorService.Get();
        return Ok(authors);
    }

    /// <summary>
    /// Retorna um autor com base no ID.
    /// </summary>
    /// <param name="id">ID do autor</param>
    /// <returns>Autor correspondente</returns>
    /// <response code="200">Autor encontrado</response>
    /// <response code="404">Autor não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorDto>> GetById(int id)
    {
        var author = await _authorService.Get(id);
        if (author == null)
            return NotFound("Author not found");

        return Ok(author);
    }

    /// <summary>
    /// Cria um novo autor.
    /// </summary>
    /// <param name="authorDto">Dados do autor a ser criado</param>
    /// <returns>Autor criado</returns>
    /// <response code="201">Autor criado com sucesso</response>
    [HttpPost]
    [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<AuthorDto>> Create(AuthorDto authorDto)
    {
        var createdAuthor = await _authorService.Create(authorDto);
        return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
    }

    /// <summary>
    /// Atualiza os dados de um autor existente.
    /// </summary>
    /// <param name="id">ID do autor</param>
    /// <param name="authorDto">Dados atualizados</param>
    /// <returns>Resultado da operação</returns>
    /// <response code="204">Atualização bem-sucedida</response>
    /// <response code="400">ID da URL diferente do corpo da requisição</response>
    /// <response code="404">Autor não encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, AuthorDto authorDto)
    {
        if (authorDto.Id != id)
            return BadRequest("Mismatched ID");

        var success = await _authorService.Update(authorDto);
        if (!success)
            return NotFound("Author not found");

        return NoContent();
    }

    /// <summary>
    /// Remove um autor com base no ID.
    /// </summary>
    /// <param name="id">ID do autor a ser removido</param>
    /// <returns>Resultado da operação</returns>
    /// <response code="204">Remoção bem-sucedida</response>
    /// <response code="404">Autor não encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _authorService.Delete(id);
        if (!deleted)
            return NotFound("Author not found");

        return NoContent();
    }
}
