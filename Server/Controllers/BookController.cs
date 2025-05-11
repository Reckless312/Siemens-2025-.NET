using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repository;
using Server.Validators;

namespace Server.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private int NewId { get; set; }

        private BookRepository repository;

        public BookController(BookRepository repository)
        {
            // TO DO: Make a function in the entity framework to get the last id added
            this.repository = repository;
            this.NewId = 1;
        }

        [HttpGet]
        public List<Book> GetAllBooks()
        {
            // TO DO: Connect to the repo
            return new List<Book>();
        }

        [HttpPost ("add")]
        public bool AddBook(String title, String author, int quality)
        {
            bool isBookContentValid = BookValidator.ValidateEntry(title, author, quality);

            switch (isBookContentValid)
            {
                case true:
                    // TO DO: Connect to Repo
                    break;
                case false:
                    return false;
            }

            return true;
        }

        [HttpDelete ("delete/{id}")]
        public bool DeleteOneBookAppearance(String id)
        {
            // TO DO: Connect to Repo (check if it exists)

            bool isIdValid = true;

            // Extra: Check if it's rented

            switch (isIdValid)
            {
                case true:
                    // TO DO: Connect to Repo (delete the entry)
                    break;
                case false:
                    break;
            }

            return true;
        }

        [HttpDelete ("delete/{author}/{title}")]
        public bool DeleteAllAppearancesOfABook(String title, String author)
        {
            // TO DO: Connect To Repo (check if it exists)

            // TO DO: Check if there exists a book which is rented

            // TO DO: Delete all of them

            return true;
        }

        [HttpPatch ("update/quality")]
        public bool UpdateOneBook(int id, int quality)
        {
            // TO DO: Check if it exists

            // TO DO: Check if it rented

            // TO DO: Update the book

            return true;
        }

        [HttpPatch ("update")]
        public bool UpdateBook(int id, String newTitle, String newAuthor, int newQuality)
        {
            // TO DO: Check if it exists

            // TO DO: Check if it rented

            // TO DO: Update the book

            return true;
        }
    }
}
