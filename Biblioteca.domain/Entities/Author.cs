namespace Biblioteca.Domain.Entities;
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public ICollection<Book> Books{ get; set; }
    public Author() { }

    public Author(int id, string name, int age, ICollection<Book> books)
    {
        Id = id;
        Name = name;
        Age = age;
        this.Books = books;
    }
}
