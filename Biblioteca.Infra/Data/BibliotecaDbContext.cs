using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Infra.Data;
public class BibliotecaDbContext : DbContext
{
    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
    : base(options) { }

    public  DbSet<Book> Books {  get; set; }
    public DbSet<Author> Authors { get; set; }
}
