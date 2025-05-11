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
        private BookRepository repository;

        public BookController(BookRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<List<Book>> GetAllBooks()
        {
            return await this.repository.GetBooks();
        }

        [HttpPost ("add")]
        public async Task<bool> AddBook(String title, String author, int quality)
        {
            bool isBookContentValid = BookValidator.ValidateEntry(title, author, quality);

            switch (isBookContentValid)
            {
                case true:
                    await this.repository.Add(title, author, quality);
                    return true;
                case false:
                    return false;
            }
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
