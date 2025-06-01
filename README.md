Sistema de Gerenciamento de Biblioteca
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
Navegação Intuitiva: Transição entre as seções de Autores e Livros na mesma página, com feedback visual da seção ativa.
Validação de Formulários: Validação básica no lado do cliente para garantir a integridade dos dados.
Pré-requisitos
Para executar este projeto, você precisará ter instalado:

.NET SDK 8.0 ou superior.
Um navegador web moderno (Chrome, Firefox, Edge, etc.).
(Opcional) Um editor de código como Visual Studio Code ou Visual Studio.
Como Configurar e Executar
Siga os passos abaixo para configurar e executar o sistema localmente:

1. Clonar o Repositório
Bash

git clone <URL_DO_SEU_REPOSITORIO>
cd <pasta_do_seu_repositorio>
2. Configurar o Backend (API)
A API usa Entity Framework Core com um DbContext. Por padrão, ela pode usar um banco de dados em memória ou um SQL Server LocalDB. Se precisar de outra configuração, ajuste o appsettings.json e a classe Program.cs (ou Startup.cs) no projeto 1 - Presentation.

Navegue até a pasta do projeto da API:
Bash

cd src/1 - Presentation
Restaurar Pacotes NuGet:
Bash

dotnet restore
Aplicar Migrações do Banco de Dados: Se você estiver usando um banco de dados persistente (como SQL Server) e tiver migrações configuradas, aplique-as:
Bash

dotnet ef database update --project ../4 - Infrastructure/Biblioteca.Infra.csproj
Se você estiver usando um banco de dados em memória para testes/desenvolvimento, esta etapa não é necessária.
Executar a API:
Bash

dotnet run
A API será iniciada e geralmente estará disponível em https://localhost:7299. Mantenha este terminal aberto enquanto usa o frontend.
3. Configurar e Acessar o Frontend
O frontend é composto por arquivos HTML, CSS e JavaScript estáticos.

Navegue até a pasta do frontend (se você já não estiver nela):

Bash

cd src/1 - Presentation/wwwroot
Abrir o index.html:
Simplesmente abra o arquivo index.html em seu navegador web preferido.

Você pode arrastá-lo e soltá-lo no navegador.
Ou, se estiver usando o Visual Studio Code, pode usar a extensão "Live Server" para servi-lo, o que é útil para desenvolvimento.
A página inicial será carregada, permitindo que você navegue para as seções de gerenciamento de Autores ou Livros.

Estrutura do Projeto
Biblioteca.API Solução
├─── .github/
│    └─── workflows/
│         └─── ... (GitHub Actions)
├─── src/
│    ├─── 1 - Presentation/        # Camada de apresentação (API RESTful + Frontend)
│    │    ├─── Controllers/         # Controladores da API
│    │    │    ├─── AuthorController.cs
│    │    │    └─── BookController.cs
│    │    ├─── wwwroot/           # Frontend: HTML, CSS, JS e Imagens
│    │    │    ├─── index.html       # Página inicial (Dashboard)
│    │    │    ├─── gerenciamento.html # Página de gerenciamento (Autores/Livros)
│    │    │    ├─── css/
│    │    │    │    └─── Style.css    # Estilos CSS
│    │    │    ├─── js/
│    │    │    │    └─── Script.js    # Lógica JavaScript
│    │    │    └─── images/
│    │    │         ├─── author-illustration.png
│    │    │         └─── books-illustration.png
│    │    ├─── appsettings.json
│    │    └─── Program.cs          # Configuração do host e serviços (incluindo Static Files)
│    ├─── 2 - Application/         # Lógica de Aplicação (casos de uso)
│    │    ├─── Biblioteca.Application.csproj
│    │    └─── ... (Arquivos da Camada de Aplicação)
│    ├─── 3 - Domain/              # Domínio (entidades, regras de negócio)
│    │    ├─── Biblioteca.Domain.csproj
│    │    ├─── Entities/
│    │    │    ├─── Author.cs
│    │    │    └─── Book.cs
│    │    └─── Enumerado/
│    │        └─── BookGenre.cs
│    │    └─── ... (Outros arquivos de Domínio)
│    ├─── 4 - Infrastructure/      # Infraestrutura (persistência, serviços)
│    │    ├─── Biblioteca.Infra.csproj
│    │    ├─── Data/
│    │    │    └─── BibliotecaDbContext.cs
│    │    ├─── Migrations/
│    │    │    └─── ... (Arquivos de Migração do EF Core)
│    │    ├─── Service/
│    │    │    ├─── AuthorService.cs
│    │    │    └─── BookService.cs
│    │    └─── ... (Outros arquivos de Infraestrutura)
│    └─── 5 - Tests/               # Projetos de teste
│        ├─── Biblioteca.Tests.csproj
│        └─── ... (Arquivos de Teste)
├─── README.md
├─── .gitattributes
├─── .gitignore
└─── ... (Outros arquivos de configuração da solução)
Tecnologias Utilizadas
Backend:
ASP.NET Core (C#)
Entity Framework Core (ORM)
.NET SDK 8.0
Frontend:
HTML5
CSS3
JavaScript (ES6+)
Font Awesome (para ícones)
Google Fonts (para tipografia)
Contribuição
Sinta-se à vontade para propor melhorias, corrigir bugs ou adicionar novas funcionalidades.

Faça um fork do projeto.
Crie uma nova branch (git checkout -b feature/minha-nova-feature).
Faça suas alterações e commit (git commit -m 'feat: minha nova feature').
Envie para o repositório (git push origin feature/minha-nova-feature).
Abra um Pull Request.
