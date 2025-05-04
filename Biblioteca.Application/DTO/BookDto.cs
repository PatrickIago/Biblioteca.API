using Biblioteca.Domain.Enumerado;

namespace Biblioteca.Application.DTO;
public class BookDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public BookGenre Genre { get; set; }

    public BookDto() { }
    
    public BookDto(int id, string name, string description, BookGenre genre)
    {
        Id = id;
        Name = name;
        Description = description;
        Genre = genre;
    }
}
