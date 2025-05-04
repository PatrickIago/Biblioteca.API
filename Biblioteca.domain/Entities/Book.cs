using Biblioteca.Domain.Enumerado;
namespace Biblioteca.Domain.Entities;
public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public BookGenre Genre { get; set; }

    public Book() { }
    public Book(int id, string name, string description, BookGenre genre)
    {
        Id = id;
        Name = name;
        Description = description;
        Genre = genre;
    }
}
