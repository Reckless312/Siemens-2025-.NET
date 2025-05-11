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


    }
}
