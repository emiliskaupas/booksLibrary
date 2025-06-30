using Microsoft.AspNetCore.Mvc;

namespace books.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Year = 1925 },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Year = 1960 },
                new Book { Id = 3, Title = "1984", Author = "George Orwell", Year = 1949 },
                new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", Year = 1813 },
                new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Year = 1951 }
            };

            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Year = 1925 },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Year = 1960 },
                new Book { Id = 3, Title = "1984", Author = "George Orwell", Year = 1949 },
                new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", Year = 1813 },
                new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Year = 1951 }
            };

            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
