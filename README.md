📚 Sistema de Gerenciamento de Biblioteca
Este é um sistema simples de gerenciamento de biblioteca desenvolvido com ASP.NET Core para o backend (API) e HTML, CSS e JavaScript puro para o frontend. Ele permite o cadastro, visualização, edição e exclusão de autores e livros.

🧭 Visão Geral do Projeto
O sistema é dividido em duas partes principais:

🔹 Backend (API - ASP.NET Core)
Responsável por gerenciar os dados de autores e livros, persistindo em um banco de dados e expondo-os através de uma API RESTful.

🔧 Arquitetura
O backend segue princípios de Clean Architecture e DDD (Domain-Driven Design), com as seguintes camadas:

Apresentação: Camada de API (Controladores).

Aplicação: Lógica de negócio e casos de uso.

Domínio: Entidades, enumerações e regras de negócio.

Infraestrutura: Persistência de dados (EF Core) e serviços externos.

Testes: Projetos de teste para garantir a qualidade do código.

🔹 Frontend (HTML, CSS, JavaScript)
Aplicação web simples que consome a API para exibir os dados e oferecer uma interface de usuário intuitiva.

✨ Páginas
Dashboard: Layout com cartões que direcionam para "Gerenciar Autores" ou "Gerenciar Livros".

Gerenciamento: Interface para listar, detalhar, adicionar, editar e excluir autores e livros.

🛠️ Funcionalidades
✅ Gerenciar Autores
Listar todos os autores

Visualizar detalhes de um autor (e seus livros)

Adicionar novo autor

Editar autor

Excluir autor

✅ Gerenciar Livros
Listar todos os livros

Visualizar detalhes de um livro

Adicionar novo livro

Editar livro

Excluir livro

🗂️ Estrutura do Projeto
nginx
Copiar
Editar
Sistema de Gerenciamento de Biblioteca
├── 📁 .github/
│   └── 📁 workflows/
│       └── 📄 ... (arquivos GitHub Actions)
├── 📁 src/
│   ├── 📁 1 - Apresentação/         # Camada de API (.NET Core)
│   │   ├── 📁 Controllers/
│   │   │   ├── 📄 AuthorController.cs
│   │   │   └── 📄 BookController.cs
│   │   ├── 📁 wwwroot/              # Frontend (HTML, CSS, JS, Imagens)
│   │   │   ├── 📄 index.html        # Página inicial (Dashboard)
│   │   │   ├── 📄 gerenciamento.html# Página de gerenciamento
│   │   │   ├── 📁 css/
│   │   │   │   └── 📄 Style.css
│   │   │   ├── 📁 js/
│   │   │   │   └── 📄 Script.js
│   │   │   └── 📁 images/
│   │   │       ├── 📄 author-illustration.png
│   │   │       └── 📄 books-illustration.png
│   │   ├── 📄 appsettings.json
│   │   └── 📄 Program.cs
│   ├── 📁 2 - Application/          # Casos de uso, DTOs, manipuladores
│   │   ├── 📄 Biblioteca.Application.csproj
│   │   └── 📄 ...
│   ├── 📁 3 - Domínio/              # Entidades e regras de negócio
│   │   ├── 📄 Biblioteca.Domain.csproj
│   │   ├── 📁 Entidades/
│   │   │   ├── 📄 Autor.cs
│   │   │   └── 📄 Livro.cs
│   │   └── 📁 Enumerado/
│   │       └── 📄 BookGenre.cs
│   ├── 📁 4 - Infraestrutura/       # Acesso a dados, serviços externos
│   │   ├── 📄 Biblioteca.Infra.csproj
│   │   ├── 📁 Dados/
│   │   │   ├── 📄 BibliotecaDbContext.cs
│   │   │   └── 📁 Migrações/
│   │   │       └── 📄 ...
│   │   ├── 📁 Serviço/
│   │   │   ├── 📄 AuthorService.cs
│   │   │   └── 📄 BookService.cs
│   │   └── 📄 ...
│   ├── 📁 5 - Testes/               # Testes automatizados
│   │   ├── 📄 Biblioteca.Tests.csproj
│   │   └── 📄 ...
├── 📄 README.md
├── 📄 .gitattributes
├── 📄 .gitignore
└── 📄 ...
