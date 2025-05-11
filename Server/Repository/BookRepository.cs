using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Repository
{
    public class BookRepository
    {
        private Context database;

        public BookRepository(Context database)
        {
            this.database = database;
        }

        public async Task<List<Book>> GetBooks() 
        {
            return await this.database.Books.ToListAsync();
        }

        public async Task Add(String title, String author, int quality)
        {
            Book newBook = new Book(title, author, quality);

            await this.database.AddAsync(newBook);

            await this.database.SaveChangesAsync();
        }

        public async Task<bool> DoesBookExistsById(int id)
        {
            return await this.database.Books.FirstOrDefaultAsync(book => book.Id == id) != null;
        }

        public async Task Delete(int id)
        {
            Book? book = await this.GetBookById(id);

            if (book != null)
            {
                this.database.Remove(book);
            }

            await this.database.SaveChangesAsync();
        }

        public async Task DeleteAllOccurences(String title, String author)
        {
            List<Book> books = await this.GetBooksByTitleAndAuthor(title, author);

            foreach (Book book in books)
            {
                this.database.Remove(book);
            }

            await this.database.SaveChangesAsync();
        }

        public async Task<bool> DoesBookExistsByTitleAndAuthor(String title, String author)
        {
            return await this.database.Books.FirstOrDefaultAsync(book => book.Title == title && book.Author == author) != null;
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await this.database.Books.FirstOrDefaultAsync(book => book.Id == id);
        }

        public async Task<List<Book>> GetBooksByTitleAndAuthor(String title, String author)
        {
            return await this.database.Books.Where(book => book.Title == title && book.Author == author).ToListAsync();
        }
    }
}
