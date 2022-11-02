using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        // Навигационное свойство
        public List<Book> Books { get; set; }
    }
}
