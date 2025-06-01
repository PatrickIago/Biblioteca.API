using Biblioteca.Domain.Enumerado;
namespace Biblioteca.Domain.Entities;
public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public BookGenre Genre { get; set; }
    public int? AuthorId { get; set; }
    public Author? Author { get; set; }

    public Book() { }
    public Book(int id, string name, string description, BookGenre genre, Author author, int? authorId)
    {
        Id = id;
        Name = name;
        Description = description;
        Genre = genre;
        Author = author;
        AuthorId = authorId;
    }
}
