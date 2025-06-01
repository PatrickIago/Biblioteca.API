const apiUrl = "https://localhost:7299/api/books";


const form = document.getElementById("bookForm");
const bookId = document.getElementById("bookId");
const nameInput = document.getElementById("name");
const descriptionInput = document.getElementById("description");
const genreInput = document.getElementById("genre");
const tableBody = document.getElementById("bookTableBody");

form.addEventListener("submit", function (e) {
    e.preventDefault();

    const book = {
        name: nameInput.value,
        description: descriptionInput.value,
        genre: parseInt(genreInput.value)
    };

    if (bookId.value) {
        fetch(`${apiUrl}/${bookId.value}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ id: parseInt(bookId.value), ...book })
        }).then(() => {
            form.reset();
            bookId.value = "";
            loadBooks();
        });
    } else {
        fetch(apiUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(book)
        }).then(() => {
            form.reset();
            loadBooks();
        });
    }
});

function loadBooks() {
    fetch(apiUrl)
        .then(res => res.json())
        .then(data => {
            tableBody.innerHTML = "";
            data.forEach(book => {
                const row = document.createElement("tr");

                row.innerHTML = `
                    <td>${book.name}</td>
                    <td>${book.description}</td>
                    <td>${getGenre(book.genre)}</td>
                    <td>
                        <button onclick="editBook(${book.id})">Editar</button>
                        <button onclick="deleteBook(${book.id})">Excluir</button>
                    </td>
                `;
                tableBody.appendChild(row);
            });
        });
}

function getGenre(code) {
    const genres = {
        1: "Romance",
        2: "Ficção Científica",
        3: "Fantasia",
        4: "Biografia"
    };
    return genres[code] || "Desconhecido";
}

function editBook(id) {
    fetch(`${apiUrl}/${id}`)
        .then(res => res.json())
        .then(book => {
            bookId.value = book.id;
            nameInput.value = book.name;
            descriptionInput.value = book.description;
            genreInput.value = book.genre;
        });
}

function deleteBook(id) {
    if (confirm("Tem certeza que deseja excluir este livro?")) {
        fetch(`${apiUrl}/${id}`, {
            method: "DELETE"
        }).then(() => loadBooks());
    }
}

loadBooks();
