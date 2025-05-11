using System.Runtime.CompilerServices;
using Server.Models;

namespace Server.Validators
{
    public class BookValidator
    {
        public static bool ValidateEntry(String title, String author, int quality)
        {
            return BookValidator.ValidateTitle(title) &&
                BookValidator.ValidateAuthor(author) &&
                BookValidator.ValidateQuality(quality);
        }

        private static bool ValidateTitle(String title)
        {
            if (title == string.Empty)
            {
                return false;
            }

            String[] titleContent = title.Split(" ");

            if (titleContent.Length < Book.MINIMUM_NUMBER_OF_WORDS_IN_TITLE ||
                titleContent.Length > Book.MAXIMUM_NUMBER_OF_WORDS_IN_TITLE)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateAuthor(String author)
        {
            if (author == string.Empty)
            {
                return false;
            }

            String[] authorContent = author.Split(" ");

            if (authorContent.Length < Book.MINIMUM_NUMBER_OF_WORDS_IN_AUTHOR ||
                authorContent.Length > Book.MAXIMUM_NUMBER_OF_WORDS_IN_AUTHOR)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateQuality (int quality)
        {
            if (quality < Book.MINIMUM_QUALITY || quality > Book.MAXIMUM_QUALITY)
            {
                return false;
            }

            return true;
        }
    }
}
