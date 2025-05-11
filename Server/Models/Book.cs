using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }
        public int Quality { get; set; }

        private const int MINIMUM_ACCEPTABLE_QUALITY = 5;

        // Constraints:
        public const int MINIMUM_NUMBER_OF_WORDS_IN_TITLE = 3;
        public const int MAXIMUM_NUMBER_OF_WORDS_IN_TITLE = 10;

        public const int MINIMUM_NUMBER_OF_WORDS_IN_AUTHOR = 2;
        public const int MAXIMUM_NUMBER_OF_WORDS_IN_AUTHOR = 5;

        public const int MINIMUM_QUALITY = 0;
        public const int MAXIMUM_QUALITY = 10;

        public Book(String title, String author, int quality)
        {
            this.Title = title;
            this.Author = author;
            this.Quality = quality;
        }

        public bool InNeedOfReplacement()
        {
            return this.Quality < Book.MINIMUM_ACCEPTABLE_QUALITY;
        }

        public override string ToString()
        {
            return $"Id: {this.Id}, Title: {this.Title}, Author: {this.Author}, Quality: {this.Quality}";
        }
    }
}
