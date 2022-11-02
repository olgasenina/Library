using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    /// <summary>
    /// Книги
    /// </summary>
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }        // наименование книги
        public string BookAuthor { get; set; }      // автор книги
        public string PublishingHouse { get; set; } // издательство
        public int PublicationYear { get; set; }    // год издания

        // Внешний ключ
        public int GenreId { get; set; }         // жанр книги

        // Навигационное свойство
        public Genre Genre { get; set; }
        public List<User> ListUsers { get; set; } = new List<User>();
    }
}
