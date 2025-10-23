# 📚 Sistema de Gerenciamento de Biblioteca

Este é um sistema simples de gerenciamento de biblioteca desenvolvido com **ASP.NET Core** para o backend (API) e **HTML, CSS e JavaScript puro** para o frontend. Ele permite o **cadastro, visualização, edição e exclusão** de autores e livros.

---

## 🧭 Visão Geral do Projeto

O sistema é dividido em duas partes principais:

### 🔹 Backend (API - ASP.NET Core)

Responsável por gerenciar os dados de autores e livros, persistindo em um banco de dados e expondo-os através de uma API RESTful.

#### 🔧 Arquitetura

O backend segue princípios de **Clean Architecture** e **DDD (Domain-Driven Design)**, com as seguintes camadas:

- **Apresentação**: Camada da API (Controladores)
- **Aplicação**: Lógica de negócio e casos de uso
- **Domínio**: Entidades, enumerações e regras de negócio
- **Infraestrutura**: Persistência de dados (EF Core) e serviços externos
- **Testes**: Projetos de teste para garantir a qualidade do código

---

### 🔹 Frontend (HTML, CSS, JavaScript)

Aplicação web simples que consome uma API para exibir os dados e oferecer uma **interface de usuário intuitiva**.

#### ✨ Páginas

- **Dashboard**: Layout com cartões que direcionam para _Gerenciar Autores_ ou _Gerenciar Livros_
- **Gerenciamento**: Interface para listar, detalhar, adicionar, editar e excluir autores e livros

---

## ✅ Funcionalidades

### 📖 Gerenciar Autores

- Listar todos os autores
- Visualizar detalhes de um autor (e seus livros)
- Adicionar novo autor
- Editar autor
- Excluir autor

### 📚 Gerenciar Livros

- Listar todos os livros
- Visualizar detalhes de um livro
- Adicionar novo livro
- Editar livro
- Excluir livro


