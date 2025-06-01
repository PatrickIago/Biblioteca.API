# Sistema de Gerenciamento de Biblioteca
Este é um sistema simples de gerenciamento de biblioteca desenvolvido com ASP.NET Core para o backend (API) e HTML, CSS e JavaScript puro para o frontend. Ele permite o cadastro, visualização, edição e exclusão de autores e livros.

Visão Geral do Projeto
O sistema é dividido em duas partes principais:

Backend (API - ASP.NET Core): Responsável por gerenciar os dados dos autores e livros, persistindo-os em um banco de dados e expondo-os através de uma API RESTful.
Arquitetura: Segue princípios de Clean Architecture / DDD (Domain-Driven Design) com camadas distintas para separação de responsabilidades:
1 - Presentation: Camada de API (Controladores).
2 - Application: Lógica de negócio e coordenação de casos de uso.
3 - Domain: Entidades, enums e regras de negócio centrais.
4 - Infrastructure: Implementação de persistência de dados (Entity Framework Core) e serviços externos.
5 - Tests: Projetos de teste para garantir a qualidade do código.
Frontend (HTML, CSS, JavaScript Puro): Uma aplicação web simples que consome a API do backend para interagir com os dados da biblioteca, proporcionando uma interface de usuário intuitiva.
Página Inicial (Dashboard): Apresenta um layout visual com cards para direcionar o usuário rapidamente para as seções de "Gerenciar Autores" ou "Gerenciar Livros".
Página de Gerenciamento: Contém a interface completa para listar, detalhar, adicionar e editar autores e livros, utilizando transições de seção via JavaScript.
Funcionalidades
Gerenciar Autores:
Listar todos os autores cadastrados.
Visualizar detalhes de um autor específico (incluindo seus livros associados).
Adicionar novos autores.
Editar informações de autores existentes.
Excluir autores.
Gerenciar Livros:
Listar todos os livros cadastrados.
Visualizar detalhes de um livro específico.
Adicionar novos livros.
Editar informações de livros existentes.
Excluir livros.

## Estrutura do Projeto

Sistema de Gerenciamento de Biblioteca
├── 📁 .github/
│   └── 📁 workflows/
│        └── 📄 ... (GitHub Actions files)
├── 📁 src/
│   ├── 📁 1 - Presentation/        # API Layer (.NET Core)
│   │   ├── 📁 Controllers/
│   │   │   ├── 📄 AuthorController.cs
│   │   │   └── 📄 BookController.cs
│   │   ├── 📁 wwwroot/           # Frontend (HTML, CSS, JS, Images)
│   │   │   ├── 📄 index.html       # Home page (Dashboard)
│   │   │   ├── 📄 gerenciamento.html # Management page (Authors/Books)
│   │   │   ├── 📁 css/
│   │   │   │   └── 📄 Style.css    # CSS Styles
│   │   │   ├── 📁 js/
│   │   │   │   └── 📄 Script.js    # JavaScript Logic
│   │   │   └── 📁 images/
│   │   │        ├── 📄 author-illustration.png
│   │   │        └── 📄 books-illustration.png
│   │   ├── 📄 appsettings.json
│   │   └── 📄 Program.cs           # Host and services configuration
│   ├── 📁 2 - Application/         # Application Logic (use cases, DTOs, handlers)
│   │   ├── 📄 Biblioteca.Application.csproj
│   │   └── 📄 ... (More Application files)
│   ├── 📁 3 - Domain/              # Domain (entities, business rules)
│   │   ├── 📄 Biblioteca.Domain.csproj
│   │   ├── 📁 Entities/
│   │   │   ├── 📄 Author.cs
│   │   │   └── 📄 Book.cs
│   │   └── 📁 Enumerado/
│   │       └── 📄 BookGenre.cs
│   │   └── 📄 ... (More Domain files)
│   ├── 📁 4 - Infrastructure/      # Infrastructure (data persistence, external services)
│   │   ├── 📄 Biblioteca.Infra.csproj
│   │   ├── 📁 Data/
│   │   │   └── 📄 BibliotecaDbContext.cs
│   │   │   └── 📁 Migrations/
│   │   │       └── 📄 ... (EF Core Migrations files)
│   │   ├── 📁 Service/
│   │   │   ├── 📄 AuthorService.cs
│   │   │   └── 📄 BookService.cs
│   │   └── 📄 ... (More Infrastructure files)
│   └── 📁 5 - Tests/               # Test Projects
│       ├── 📄 Biblioteca.Tests.csproj
│       └── 📄 ... (More Test files)
├── 📄 README.md
├── 📄 .gitattributes
├── 📄 .gitignore
└── 📄 ... (Other solution configuration files)
