// URLs 
const API_BASE_URL_AUTHORS = 'https://localhost:7299/api/Author';
const API_BASE_URL_BOOKS = 'https://localhost:7299/api/Book';

// --- Elementos da UI - Navegação (na página de gerenciamento) ---
// Estes só existirão em gerenciamento.html
const navAuthorsBtn = document.getElementById('nav-authors-btn');
const navBooksBtn = document.getElementById('nav-books-btn');

// --- Elementos da UI - Autores ---
const authorsListSection = document.getElementById('authors-list');
const authorsContainer = document.getElementById('authors-container');
const showAddAuthorFormBtn = document.getElementById('show-add-author-form-btn');
const authorsErrorMessage = document.getElementById('authors-error-message');
const authorsEmptyState = document.getElementById('authors-empty-state');

const authorDetailsSection = document.getElementById('author-details');
const detailAuthorId = document.getElementById('detail-author-id');
const detailAuthorName = document.getElementById('detail-author-name');
const detailAuthorAge = document.getElementById('detail-author-age');
const detailAuthorBooks = document.getElementById('detail-author-books');
const editAuthorBtn = document.getElementById('edit-author-btn');
const deleteAuthorBtn = document.getElementById('delete-author-btn');
const backToAuthorsListBtn = document.getElementById('back-to-authors-list-btn');

const authorFormSection = document.getElementById('author-form');
const authorFormTitle = document.getElementById('author-form-title');
const authorForm = document.getElementById('authorForm');
const authorIdInput = document.getElementById('author-id');
const authorNameInput = document.getElementById('author-name');
const authorAgeInput = document.getElementById('author-age');
const authorBookIdsInput = document.getElementById('author-book-ids');
const cancelAuthorFormBtn = document.getElementById('cancel-author-form-btn');

let currentAuthorId = null; // Para controlar o autor sendo editado/detalhado

// --- Elementos da UI - Livros ---
const booksListSection = document.getElementById('books-list');
const booksContainer = document.getElementById('books-container');
const showAddBookFormBtn = document.getElementById('show-add-book-form-btn');
const booksErrorMessage = document.getElementById('books-error-message');
const booksEmptyState = document.getElementById('books-empty-state');

const bookDetailsSection = document.getElementById('book-details');
const detailBookId = document.getElementById('detail-book-id');
const detailBookName = document.getElementById('detail-book-name');
const detailBookDescription = document.getElementById('detail-book-description');
const detailBookGenre = document.getElementById('detail-book-genre');
const detailBookAuthor = document.getElementById('detail-book-author');
const editBookBtn = document.getElementById('edit-book-btn');
const deleteBookBtn = document.getElementById('delete-book-btn');
const backToBooksListBtn = document.getElementById('back-to-books-list-btn');

const bookFormSection = document.getElementById('book-form');
const bookFormTitle = document.getElementById('book-form-title');
const bookForm = document.getElementById('bookForm');
const bookIdInput = document.getElementById('book-id');
const bookNameInput = document.getElementById('book-name');
const bookDescriptionInput = document.getElementById('book-description');
const bookGenreSelect = document.getElementById('book-genre');
const bookAuthorIdInput = document.getElementById('book-author-id');
const cancelBookFormBtn = document.getElementById('cancel-book-form-btn');

let currentBookId = null; // Para controlar o livro sendo editado/detalhado

// --- Mapeamento de Gêneros (para exibição e formulário) ---
const BookGenreMap = {
    1: 'Romance',
    2: 'ScienceFiction',
    3: 'Fantasy',
    4: 'Biography',
    5: 'Poesia',
    6: 'História',
    7: 'Ciência',
    8: 'Ficção'
};

// --- Funções Auxiliares de UI (para mostrar/esconder seções) ---
function hideAllSections() {
    authorsListSection.classList.add('hidden');
    authorDetailsSection.classList.add('hidden');
    authorFormSection.classList.add('hidden');
    booksListSection.classList.add('hidden');
    bookDetailsSection.classList.add('hidden');
    bookFormSection.classList.add('hidden');
}

function showSection(sectionElement) {
    hideAllSections(); // Oculta todas as seções antes de mostrar a desejada
    sectionElement.classList.remove('hidden');
}

function setActiveNavButton(button) {
    // Verifica se os botões de navegação existem (eles só existem em gerenciamento.html)
    if (navAuthorsBtn && navBooksBtn) {
        navAuthorsBtn.classList.remove('active');
        navBooksBtn.classList.remove('active');
        button.classList.add('active');
    }
}

// --- Funções para Autores ---

async function loadAuthors() {
    showSection(authorsListSection); // Mostra a seção de lista de autores
    authorsContainer.innerHTML = '<p class="loading-message">Carregando autores...</p>';
    authorsErrorMessage.classList.add('hidden'); // Oculta mensagem de erro
    authorsEmptyState.classList.add('hidden'); // Oculta mensagem de estado vazio

    try {
        const response = await fetch(API_BASE_URL_AUTHORS);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const authors = await response.json();
        displayAuthors(authors);
        setActiveNavButton(navAuthorsBtn);
    } catch (error) {
        console.error("Erro ao carregar autores:", error);
        authorsContainer.innerHTML = ''; // Limpa o carregando
        authorsErrorMessage.textContent = `Erro ao carregar autores: ${error.message}. Verifique a conexão com a API.`;
        authorsErrorMessage.classList.remove('hidden');
    }
}

function displayAuthors(authors) {
    authorsContainer.innerHTML = ''; // Limpa a lista
    if (authors.length === 0) {
        authorsEmptyState.classList.remove('hidden');
        return;
    }

    authors.forEach(author => {
        const authorCard = document.createElement('div');
        authorCard.classList.add('author-card');
        authorCard.innerHTML = `
            <div>
                <h3>${author.name}</h3>
                <p>Idade: ${author.age}</p>
            </div>
            <div>
                <button data-id="${author.id}" class="view-author-details-btn action-button secondary">
                    <i class="fas fa-info-circle"></i> Ver Detalhes
                </button>
            </div>
        `;
        authorsContainer.appendChild(authorCard);
    });

    // Adiciona event listeners aos botões "Ver Detalhes"
    document.querySelectorAll('.view-author-details-btn').forEach(button => {
        button.addEventListener('click', (event) => {
            const id = event.target.dataset.id;
            fetchAuthorDetails(id);
        });
    });
}

async function fetchAuthorDetails(id) {
    try {
        const response = await fetch(`${API_BASE_URL_AUTHORS}/${id}`);
        if (!response.ok) {
            if (response.status === 404) {
                alert("Autor não encontrado.");
            } else {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
        }
        const author = await response.json();
        displayAuthorDetails(author);
        currentAuthorId = author.id; // Salva o ID para edição/exclusão
        showSection(authorDetailsSection);
    } catch (error) {
        console.error("Erro ao buscar detalhes do autor:", error);
        alert(`Erro ao buscar detalhes do autor: ${error.message}`);
    }
}

function displayAuthorDetails(author) {
    detailAuthorId.textContent = author.id;
    detailAuthorName.textContent = author.name;
    detailAuthorAge.textContent = author.age;
    detailAuthorBooks.innerHTML = '';
    if (author.books && author.books.length > 0) {
        author.books.forEach(book => {
            const li = document.createElement('li');
            li.textContent = `${book.name} (ID: ${book.id})`;
            detailAuthorBooks.appendChild(li);
        });
    } else {
        detailAuthorBooks.innerHTML = '<li>Nenhum livro associado.</li>';
    }
}

function showAuthorForm(author = null) {
    if (author) {
        authorFormTitle.textContent = 'Editar';
        authorIdInput.value = author.id;
        authorNameInput.value = author.name;
        authorAgeInput.value = author.age;
        authorBookIdsInput.value = author.books ? author.books.map(b => b.id).join(', ') : '';
    } else {
        authorFormTitle.textContent = 'Adicionar';
        authorForm.reset();
        authorIdInput.value = '';
    }
    showSection(authorFormSection);
}

async function saveAuthor(event) {
    event.preventDefault();

    const id = authorIdInput.value;
    const name = authorNameInput.value.trim();
    const age = parseInt(authorAgeInput.value);
    const bookIdsString = authorBookIdsInput.value.trim();
    const bookIds = bookIdsString ? bookIdsString.split(',').map(s => parseInt(s.trim())).filter(Number.isInteger) : [];

    if (!name || isNaN(age)) {
        alert("Por favor, preencha o nome e a idade corretamente.");
        return;
    }

    const authorData = {
        id: id ? parseInt(id) : 0,
        name: name,
        age: age,
        bookIds: bookIds
    };

    try {
        let response;
        if (id) { // Atualização
            response = await fetch(`${API_BASE_URL_AUTHORS}/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(authorData)
            });
            if (response.status === 204) {
                alert("Autor atualizado com sucesso!");
                loadAuthors();
            } else if (response.status === 404) {
                alert("Erro: Autor não encontrado para atualização.");
            } else if (response.status === 400) {
                const errorData = await response.json();
                alert(`Erro na requisição: ${errorData.title || JSON.stringify(errorData)}`);
            } else {
                const errorData = await response.json();
                throw new Error(`HTTP error! status: ${response.status}. Detalhes: ${JSON.stringify(errorData)}`);
            }
        } else { // Criação
            response = await fetch(API_BASE_URL_AUTHORS, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(authorData)
            });
            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(`HTTP error! status: ${response.status}. Detalhes: ${JSON.stringify(errorData)}`);
            }
            const newAuthor = await response.json();
            alert(`Autor "${newAuthor.name}" criado com sucesso!`);
            loadAuthors();
        }
    } catch (error) {
        console.error("Erro ao salvar autor:", error);
        alert(`Erro ao salvar autor: ${error.message}`);
    }
}

async function deleteAuthor() {
    if (!currentAuthorId) {
        alert("Nenhum autor selecionado para exclusão.");
        return;
    }

    if (!confirm(`Tem certeza que deseja excluir o autor com ID ${currentAuthorId}?`)) {
        return;
    }

    try {
        const response = await fetch(`${API_BASE_URL_AUTHORS}/${currentAuthorId}`, {
            method: 'DELETE'
        });

        if (response.status === 204) {
            alert("Autor excluído com sucesso!");
            currentAuthorId = null;
            loadAuthors();
        } else if (response.status === 404) {
            alert("Erro: Autor não encontrado para exclusão.");
        } else {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
    } catch (error) {
        console.error("Erro ao excluir autor:", error);
        alert(`Erro ao excluir autor: ${error.message}`);
    }
}

// --- Funções para Livros ---

function populateBookGenres() {
    bookGenreSelect.innerHTML = '<option value="">Selecione um Gênero</option>';
    for (const key in BookGenreMap) {
        const option = document.createElement('option');
        option.value = key;
        option.textContent = BookGenreMap[key];
        bookGenreSelect.appendChild(option);
    }
}

async function loadBooks() {
    showSection(booksListSection); // Mostra a seção de lista de livros
    booksContainer.innerHTML = '<p class="loading-message">Carregando livros...</p>';
    booksErrorMessage.classList.add('hidden');
    booksEmptyState.classList.add('hidden');

    try {
        const response = await fetch(API_BASE_URL_BOOKS);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const books = await response.json();
        displayBooks(books);
        setActiveNavButton(navBooksBtn);
    } catch (error) {
        console.error("Erro ao carregar livros:", error);
        booksContainer.innerHTML = '';
        booksErrorMessage.textContent = `Erro ao carregar livros: ${error.message}. Verifique a conexão com a API.`;
        booksErrorMessage.classList.remove('hidden');
    }
}

function displayBooks(books) {
    booksContainer.innerHTML = '';
    if (books.length === 0) {
        booksEmptyState.classList.remove('hidden');
        return;
    }

    books.forEach(book => {
        const bookCard = document.createElement('div');
        bookCard.classList.add('book-card');
        bookCard.innerHTML = `
            <div>
                <h3>${book.name}</h3>
                <p>Gênero: ${BookGenreMap[book.genre] || 'Desconhecido'}</p>
                <p>Autor ID: ${book.authorId || 'N/A'}</p>
            </div>
            <div>
                <button data-id="${book.id}" class="view-book-details-btn action-button secondary">
                    <i class="fas fa-info-circle"></i> Ver Detalhes
                </button>
            </div>
        `;
        booksContainer.appendChild(bookCard);
    });

    document.querySelectorAll('.view-book-details-btn').forEach(button => {
        button.addEventListener('click', (event) => {
            const id = event.target.dataset.id;
            fetchBookDetails(id);
        });
    });
}

async function fetchBookDetails(id) {
    try {
        // Ajuste aqui se sua API de detalhes de livro não usa query parameter para GET by ID
        const response = await fetch(`${API_BASE_URL_BOOKS}?id=${id}`);
        if (!response.ok) {
            if (response.status === 404) {
                alert("Livro não encontrado.");
            } else {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
        }
        const book = await response.json();
        displayBookDetails(book);
        currentBookId = book.id;
        showSection(bookDetailsSection);
    } catch (error) {
        console.error("Erro ao buscar detalhes do livro:", error);
        alert(`Erro ao buscar detalhes do livro: ${error.message}`);
    }
}

function displayBookDetails(book) {
    detailBookId.textContent = book.id;
    detailBookName.textContent = book.name;
    detailBookDescription.textContent = book.description || 'N/A';
    detailBookGenre.textContent = BookGenreMap[book.genre] || 'Desconhecido';
    detailBookAuthor.textContent = book.authorId ? `ID: ${book.authorId}` : 'N/A';
}

function showBookForm(book = null) {
    populateBookGenres();
    if (book) {
        bookFormTitle.textContent = 'Editar';
        bookIdInput.value = book.id;
        bookNameInput.value = book.name;
        bookDescriptionInput.value = book.description || '';
        bookGenreSelect.value = book.genre;
        bookAuthorIdInput.value = book.authorId || '';
    } else {
        bookFormTitle.textContent = 'Adicionar';
        bookForm.reset();
        bookIdInput.value = '';
        bookGenreSelect.value = '';
    }
    showSection(bookFormSection);
}

async function saveBook(event) {
    event.preventDefault();

    const id = bookIdInput.value;
    const name = bookNameInput.value.trim();
    const description = bookDescriptionInput.value.trim();
    const genre = parseInt(bookGenreSelect.value);
    const authorId = bookAuthorIdInput.value ? parseInt(bookAuthorIdInput.value) : null;

    if (!name || isNaN(genre)) {
        alert("Por favor, preencha o nome e selecione um gênero.");
        return;
    }
    if (bookAuthorIdInput.value && (isNaN(authorId) || authorId <= 0)) { // Adicionado verificação para ID > 0
        alert("Por favor, insira um ID de autor válido (número positivo).");
        return;
    }

    const bookData = {
        id: id ? parseInt(id) : 0,
        name: name,
        description: description,
        genre: genre,
        authorId: authorId
    };

    try {
        let response;
        if (id) { // Atualização
            response = await fetch(`${API_BASE_URL_BOOKS}?id=${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(bookData)
            });
            if (response.status === 204) {
                alert("Livro atualizado com sucesso!");
                loadBooks();
            } else if (response.status === 404) {
                alert("Erro: Livro não encontrado para atualização.");
            } else if (response.status === 400) {
                const errorData = await response.json();
                alert(`Erro na requisição: ${errorData.title || JSON.stringify(errorData)}`);
            } else {
                const errorData = await response.json();
                throw new Error(`HTTP error! status: ${response.status}. Detalhes: ${JSON.stringify(errorData)}`);
            }
        } else { // Criação
            response = await fetch(API_BASE_URL_BOOKS, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(bookData)
            });
            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(`HTTP error! status: ${response.status}. Detalhes: ${JSON.stringify(errorData)}`);
            }
            const newBook = await response.json();
            alert(`Livro "${newBook.name}" criado com sucesso!`);
            loadBooks();
        }
    } catch (error) {
        console.error("Erro ao salvar livro:", error);
        alert(`Erro ao salvar livro: ${error.message}`);
    }
}

async function deleteBook() {
    if (!currentBookId) {
        alert("Nenhum livro selecionado para exclusão.");
        return;
    }

    if (!confirm(`Tem certeza que deseja excluir o livro com ID ${currentBookId}?`)) {
        return;
    }

    try {
        const response = await fetch(`${API_BASE_URL_BOOKS}?id=${currentBookId}`, {
            method: 'DELETE'
        });

        if (response.status === 204) {
            alert("Livro excluído com sucesso!");
            currentBookId = null;
            loadBooks();
        } else if (response.status === 404) {
            alert("Erro: Livro não encontrado para exclusão.");
        } else {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
    } catch (error) {
        console.error("Erro ao excluir livro:", error);
        alert(`Erro ao excluir livro: ${error.message}`);
    }
}

// --- Event Listeners e Lógica de Inicialização ---

document.addEventListener('DOMContentLoaded', () => {
    // Verifica se estamos na página de gerenciamento
    if (document.body.classList.contains('management-page')) {
        // Lógica de inicialização da página de gerenciamento
        const urlParams = new URLSearchParams(window.location.search);
        const sectionToLoad = urlParams.get('section'); // Pega o parâmetro 'section' da URL

        if (sectionToLoad === 'books') {
            loadBooks();
        } else {
            // Padrão ou se 'section' for 'authors'
            loadAuthors();
        }

        // Adiciona Event Listeners para a página de gerenciamento
        if (navAuthorsBtn) navAuthorsBtn.addEventListener('click', loadAuthors);
        if (navBooksBtn) navBooksBtn.addEventListener('click', loadBooks);

        if (showAddAuthorFormBtn) showAddAuthorFormBtn.addEventListener('click', () => showAuthorForm());
        if (backToAuthorsListBtn) backToAuthorsListBtn.addEventListener('click', loadAuthors);
        if (authorForm) authorForm.addEventListener('submit', saveAuthor);
        if (cancelAuthorFormBtn) cancelAuthorFormBtn.addEventListener('click', loadAuthors);
        if (editAuthorBtn) editAuthorBtn.addEventListener('click', async () => {
            if (currentAuthorId) {
                const response = await fetch(`${API_BASE_URL_AUTHORS}/${currentAuthorId}`);
                if (response.ok) {
                    const author = await response.json();
                    showAuthorForm(author);
                } else {
                    alert("Não foi possível carregar os dados do autor para edição.");
                    console.error("Erro ao carregar autor para edição.");
                }
            }
        });
        if (deleteAuthorBtn) deleteAuthorBtn.addEventListener('click', deleteAuthor);

        if (showAddBookFormBtn) showAddBookFormBtn.addEventListener('click', () => showBookForm());
        if (backToBooksListBtn) backToBooksListBtn.addEventListener('click', loadBooks);
        if (bookForm) bookForm.addEventListener('submit', saveBook);
        if (cancelBookFormBtn) cancelBookFormBtn.addEventListener('click', loadBooks);
        if (editBookBtn) editBookBtn.addEventListener('click', async () => {
            if (currentBookId) {
                const response = await fetch(`${API_BASE_URL_BOOKS}?id=${currentBookId}`);
                if (response.ok) {
                    const book = await response.json();
                    showBookForm(book);
                } else {
                    alert("Não foi possível carregar os dados do livro para edição.");
                    console.error("Erro ao carregar livro para edição.");
                }
            }
        });
        if (deleteBookBtn) deleteBookBtn.addEventListener('click', deleteBook);
    }
});