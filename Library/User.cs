using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    /// <summary>
    /// Пользователи библиотеки
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Навигационное свойство
        public List<Book> ListBooks { get; set; } = new List<Book>();
    }
}
