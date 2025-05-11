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
    }
}
