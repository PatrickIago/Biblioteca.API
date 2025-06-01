using Biblioteca.Domain.Entities;

namespace Biblioteca.Application.DTO;
public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<int>? BookIds { get; set; }

    public AuthorDto() { }

    public AuthorDto(int id, string name, int age, List<int>? bookIds)
    {
        Id = id;
        Name = name;
        Age = age;
        BookIds = bookIds;
    }
}
