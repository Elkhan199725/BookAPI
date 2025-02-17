# BookAPI
📚 Book Management MVC Application
📝 Overview
This is a Book Management System built using ASP.NET Core MVC with Entity Framework Core for database management. The project allows users to add, edit, delete, and list books efficiently. Additionally, it provides a RESTful API to interact with book data programmatically.

🚀 Features
✅ Add New Books – Easily add books with title, author, and other details.
✅ Edit Book Details – Modify book information dynamically.
✅ Delete Books – Remove unwanted books from the database.
✅ View Book List – Display all books in a structured format.
✅ Fully Functional API – REST API to manage books programmatically.

🛠️ Technologies Used
ASP.NET Core MVC (Model-View-Controller architecture)
Entity Framework Core (ORM for database interaction)
SQL Server (Relational database)
Bootstrap & jQuery (Frontend design)
ASP.NET Core Web API (RESTful API integration)
📂 Project Structure

📁 BookManagementMVC
 ┣ 📁 Controllers
 ┃ ┣ 📄 BookController.cs
 ┃ ┣ 📄 Api/BookApiController.cs
 ┣ 📁 Models
 ┃ ┣ 📄 Book.cs
 ┣ 📁 Views
 ┃ ┣ 📁 Book
 ┃ ┃ ┣ 📄 Index.cshtml
 ┃ ┃ ┣ 📄 Create.cshtml
 ┃ ┃ ┣ 📄 Edit.cshtml
 ┃ ┃ ┣ 📄 Delete.cshtml
 ┣ 📁 Migrations
 ┣ 📁 wwwroot (CSS, JS, Images)
 ┣ 📄 appsettings.json
 ┣ 📄 Program.cs
 ┣ 📄 Startup.cs

🏗️ Setup & Installation
1️⃣ Clone the Repository

git clone https://github.com/Elkhan199725/BookManagementMVC.git
cd BookManagementMVC
2️⃣ Configure Database
Open appsettings.json and update the connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=BookDB;Trusted_Connection=True;"
}
Apply migrations to create the database:

dotnet ef database update
3️⃣ Run the Application

dotnet run
📌 The app will start at http://localhost:5000

🌐 API Endpoints
HTTP Method	Endpoint	Description
GET	/api/books	Get all books
GET	/api/books/{id}	Get a specific book by ID
POST	/api/books	Add a new book
PUT	/api/books/{id}	Update an existing book
DELETE	/api/books/{id}	Remove a book
Example: Adding a New Book (POST Request)
{
  "title": "The Great Gatsby",
  "author": "F. Scott Fitzgerald",
  "publishedYear": 1925
}
🎨 UI Screenshots
Book List Page	Add Book Page
🔥 Future Improvements
🔹 Implement user authentication (Admin vs. User roles).
🔹 Add pagination and search functionality for large book lists.
🔹 Integrate unit testing for controllers and API.

💡 Contributing
Feel free to fork this repository and submit a pull request if you have any improvements!

📜 License
This project is licensed under the MIT License.

📌 Author: Elkhan Mammadli
📌 GitHub: Elkhan199725