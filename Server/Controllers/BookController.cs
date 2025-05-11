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
        public async Task AddBook(String title, String author, int quality)
        {
            bool isBookContentValid = BookValidator.ValidateEntry(title, author, quality);

            switch (isBookContentValid)
            {
                case true:
                    await this.repository.Add(title, author, quality);
                    Ok();
                    break;
                case false:
                    BadRequest();
                    break;
            }
        }

        [HttpDelete ("delete/{id}")]
        public async Task DeleteOneBookAppearance(int id)
        {
            bool isIdValid = await this.repository.DoesBookExistsById(id);

            // Extra: Check if it's rented

            switch (isIdValid)
            {
                case true:
                    await this.repository.Delete(id);
                    Ok();
                    break;
                case false:
                    BadRequest();
                    break;
            }
        }

        [HttpDelete ("delete/{title}/{author}")]
        public async Task DeleteAllAppearancesOfABook(String title, String author)
        {
            bool isIdValid = await this.repository.DoesBookExistsByTitleAndAuthor(title, author);

            // TO DO: Check if there exists a book which is rented

            switch (isIdValid)
            {
                case true:
                    await this.repository.DeleteAllOccurences(title, author);
                    Ok();
                    break;
                case false:
                    BadRequest();
                    break;
            }
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
