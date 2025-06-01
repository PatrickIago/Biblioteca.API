using AutoMapper;
using Biblioteca.Application.Contract;
using Biblioteca.Application.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Infra.Data;
using Biblioteca.Infra.Service;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Biblioteca.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<BibliotecaDbContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IBookService _bookService;

        public BookServiceTests()
        {
            // Configura um DbContext em memória para os testes
            var options = new DbContextOptionsBuilder<BibliotecaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _mockContext = new Mock<BibliotecaDbContext>(options);

            // Mock do AutoMapper
            _mockMapper = new Mock<IMapper>();

            // Instancia o serviço que vamos testar, injetando os mocks
            _bookService = new BookService(_mockContext.Object, _mockMapper.Object);
        }

        // --- Testes para o método Create ---

        [Fact]
        public async Task Create_ShouldReturnCreatedBookDto_WhenBookDtoIsValid()
        {
            // Arrange
            var bookDto = new BookDto { Id = 1, Name = "Clean Code", Description = "A handbook of agile software craftsmanship", AuthorId = 101, Genre = Biblioteca.Domain.Enumerado.BookGenre.Romance };
            var bookEntity = new Book { Id = 1, Name = "Clean Code", Description = "A handbook of agile software craftsmanship", AuthorId = 101, Genre = Biblioteca.Domain.Enumerado.BookGenre.Romance };

            _mockMapper.Setup(m => m.Map<Book>(It.IsAny<BookDto>())).Returns(bookEntity);
            _mockMapper.Setup(m => m.Map<BookDto>(It.IsAny<Book>())).Returns(bookDto);

            _mockContext.Setup(c => c.Books).ReturnsDbSet(new List<Book>()); // Inicializa o DbSet mockado com uma lista vazia

            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _bookService.Create(bookDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(bookDto);         
            _mockContext.Verify(c => c.Books.Add(It.IsAny<Book>()), Times.Once());
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        // --- Testes para o método Get (todos) ---

        [Fact]
        public async Task Get_ShouldReturnListOfBookDtos_WhenBooksExist()
        {
            // Arrange
            var bookEntities = new List<Book>
            {
                new Book { Id = 1, Name = "Book 1", Description = "Desc 1", AuthorId = 1, Genre = Biblioteca.Domain.Enumerado.BookGenre.Biography},
                new Book { Id = 2, Name = "Book 2", Description = "Desc 2", AuthorId = 2, Genre = Biblioteca.Domain.Enumerado.BookGenre.Fantasy }
            };
            var bookDtos = new List<BookDto>
            {
                new BookDto { Id = 1, Name = "Book 1", Description = "Desc 1", AuthorId = 1, Genre = Biblioteca.Domain.Enumerado.BookGenre.Biography },
                new BookDto { Id = 2, Name = "Book 2", Description = "Desc 2", AuthorId = 2, Genre = Biblioteca.Domain.Enumerado.BookGenre.Fantasy }
            };

            _mockContext.Setup(c => c.Books).ReturnsDbSet(bookEntities);

            _mockMapper.Setup(m => m.Map<List<BookDto>>(bookEntities)).Returns(bookDtos);

            // Act
            var result = await _bookService.Get();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(bookDtos);
        }

        [Fact]
        public async Task Get_ShouldReturnEmptyList_WhenNoBooksExist()
        {
            // Arrange
            var bookEntities = new List<Book>(); // Lista vazia de entidades
            var bookDtos = new List<BookDto>(); // Lista vazia de DTOs

            _mockContext.Setup(c => c.Books).ReturnsDbSet(bookEntities);

            _mockMapper.Setup(m => m.Map<List<BookDto>>(bookEntities)).Returns(bookDtos);

            // Act
            var result = await _bookService.Get();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        // --- Testes para o método Get (por ID) ---

        [Fact]
        public async Task GetById_ShouldReturnBookDto_WhenBookExists()
        {
            // Arrange
            var bookId = 1;
            var bookEntity = new Book { Id = bookId, Name = "The Hobbit", Description = "A classic fantasy novel", AuthorId = 102, Genre = Biblioteca.Domain.Enumerado.BookGenre.Fantasy };
            var bookDto = new BookDto { Id = bookId, Name = "The Hobbit", Description = "A classic fantasy novel", AuthorId = 102, Genre = Biblioteca.Domain.Enumerado.BookGenre.Fantasy };

            _mockContext.Setup(c => c.Books).ReturnsDbSet(new List<Book> { bookEntity });

            // Configura o mock do mapper
            _mockMapper.Setup(m => m.Map<BookDto>(bookEntity)).Returns(bookDto);

            // Act
            var result = await _bookService.Get(bookId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(bookDto);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 99; // ID que não existe

            _mockContext.Setup(c => c.Books).ReturnsDbSet(new List<Book>()); // DbSet vazio

            // Act
            var result = await _bookService.Get(bookId);

            // Assert
            result.Should().BeNull();
            // Não precisamos mapear para DTO se a entidade não for encontrada
            _mockMapper.Verify(m => m.Map<BookDto>(It.IsAny<Book>()), Times.Never);
        }


        // --- Testes para o método Update ---

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenBookExistsAndIsUpdated()
        {
            // Arrange
            var existingBookId = 1;
            var existingBookEntity = new Book { Id = existingBookId, Name = "Old Name", Description = "Old Desc", AuthorId = 1, Genre = Biblioteca.Domain.Enumerado.BookGenre.Biography };
            var updatedBookDto = new BookDto { Id = existingBookId, Name = "New Name", Description = "New Desc", AuthorId = 2, Genre = Biblioteca.Domain.Enumerado.BookGenre.Romance };

            _mockContext.Setup(c => c.Books).ReturnsDbSet(new List<Book> { existingBookEntity });

            // Configura o mock do mapper para atualizar a entidade existente
            _mockMapper.Setup(m => m.Map(updatedBookDto, existingBookEntity)).Callback<BookDto, Book>((dto, entity) =>
            {
                entity.Name = dto.Name;
                entity.Description = dto.Description;
                entity.AuthorId = dto.AuthorId;
                entity.Genre = dto.Genre;
            });

            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1); // Simula o save

            // Act
            var result = await _bookService.Update(updatedBookDto);

            // Assert
            result.Should().BeTrue();
            existingBookEntity.Name.Should().Be("New Name");
            existingBookEntity.Description.Should().Be("New Desc");
            existingBookEntity.AuthorId.Should().Be(2);
            existingBookEntity.Genre.Should().Be(Biblioteca.Domain.Enumerado.BookGenre.Romance);
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockContext.Verify(c => c.Books.Update(It.IsAny<Book>()), Times.Once());
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange
            var nonExistentBookId = 99;
            var bookDto = new BookDto { Id = nonExistentBookId, Name = "Non Existent", Description = "No Desc", AuthorId = 999, Genre = Biblioteca.Domain.Enumerado.BookGenre.Romance };

            _mockContext.Setup(c => c.Books).ReturnsDbSet(new List<Book>());

            // Act
            var result = await _bookService.Update(bookDto);

            // Assert
            result.Should().BeFalse();
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
            _mockMapper.Verify(m => m.Map(It.IsAny<BookDto>(), It.IsAny<Book>()), Times.Never());
        }

        // --- Testes para o método Delete ---

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenBookExistsAndIsDeleted()
        {
            // Arrange
            var bookIdToDelete = 1;
            var bookEntityToDelete = new Book { Id = bookIdToDelete, Name = "Book to Delete", Description = "Some description", AuthorId = 3, Genre = Biblioteca.Domain.Enumerado.BookGenre.Biography };

            // --- MUDANÇA CRÍTICA AQUI: Use ReturnsDbSet para mockar o DbSet ---
            _mockContext.Setup(c => c.Books).ReturnsDbSet(new List<Book> { bookEntityToDelete });
            // --- FIM DA MUDANÇA ---

            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1); // Simula o save

            // Act
            var result = await _bookService.Delete(bookIdToDelete);

            // Assert
            result.Should().BeTrue();
            // --- MUDANÇA AQUI: Verifique o método Remove diretamente no DbSet do contexto mockado ---
            _mockContext.Verify(c => c.Books.Remove(It.IsAny<Book>()), Times.Once());
            // --- FIM DA MUDANÇA ---
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange
            var nonExistentBookId = 99;

            // --- MUDANÇA CRÍTICA AQUI: Use ReturnsDbSet para mockar o DbSet ---
            _mockContext.Setup(c => c.Books).ReturnsDbSet(new List<Book>()); // DbSet vazio
            // --- FIM DA MUDANÇA ---

            // Act
            var result = await _bookService.Delete(nonExistentBookId);

            // Assert
            result.Should().BeFalse();
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
            // --- MUDANÇA AQUI: Verifique o método Remove diretamente no DbSet do contexto mockado ---
            _mockContext.Verify(c => c.Books.Remove(It.IsAny<Book>()), Times.Never());
            // --- FIM DA MUDANÇA ---
        }

        // --- ESTE MÉTODO AUXILIAR NÃO É MAIS NECESSÁRIO E DEVE SER REMOVIDO ---
        // private static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> data) where T : class
        // {
        //     var queryable = data.AsQueryable();
        //     var mockSet = new Mock<DbSet<T>>();

        //     mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        //     mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        //     mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        //     mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

        //     return mockSet;
        // }
    }
}