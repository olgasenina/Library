using System;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new AppContext())
            {
                var userReposit = new UserRepository(db);       // пользователи
                var bookReposit = new BookRepository(db);       // книги

                // Вывести на экран вписок всех пользователей
                var allUsers = userReposit.AllSelect();
                userReposit.UserPrint(allUsers);

                // Вывести на экран вписок всех книг
                var allBooks = bookReposit.AllSelect();
                bookReposit.BookPrint(allBooks);

                // --------------------------------------------------------------------------------
                // Новая книга
                //bookReposit.AddBook();

                // Вывести на экран вписок всех книг

                //var allBooks =  bookReposit.AllSelect();
                //bookReposit.BookPrint(allBooks);

                // --------------------------------------------------------------------------------
                // Книга по ID

                //int id = 3;

                //var thisBook = bookReposit.SelectById(id);
                //Console.WriteLine();
                //Console.WriteLine($"Книга по ID = {id}: {thisBook.BookName}");

                // --------------------------------------------------------------------------------
                // Получение последней вышедшей книги.

                //var lastBook = bookReposit.LastBook();
                //Console.WriteLine();
                //Console.WriteLine($"Последняя вышедшая книга: {lastBook.BookName}, год издания {lastBook.PublicationYear}.");

                // --------------------------------------------------------------------------------
                // Удалить книгу по ID

                //Console.WriteLine();
                //Console.WriteLine("Удаляем книгу с ID=2");
                //bookReposit.DeleteBook(2);

                // Вывести на экран вписок всех книг

                //allBooks = bookReposit.AllSelect();
                //bookReposit.BookPrint(allBooks);

                // --------------------------------------------------------------------------------
                // Измениить год выпуска

                //Console.WriteLine();
                //Console.WriteLine("Меняем год выпуска для книги с ID=1");
                //bookReposit.UpdateBook(1, 2000);

                // Вывести на экран вписок всех книг

                //allBooks = bookReposit.AllSelect();
                //bookReposit.BookPrint(allBooks);

                // --------------------------------------------------------------------------------

                //var list = bookReposit.AllBookOrderByName();
                //Console.WriteLine();
                //Console.WriteLine("сортировка по наименованию книги");
                //bookReposit.BookPrint(list);

                //list = bookReposit.AllBookOrderByYear();
                //Console.WriteLine();
                //Console.WriteLine("сортировка по году издания книги в обратном порядке");
                //bookReposit.BookPrint(list);

                // --------------------------------------------------------------------------------
                // есть ли книга определенного автора и с определенным названием в библиотеке

                //var bs = bookReposit.BookSearch("Обыкновенное чудо", "Шварц Е.Л.");
                //Console.WriteLine();
                //Console.WriteLine($"Результат поиска книги: {bs}") ;

                // --------------------------------------------------------------------------------
                // выдать книгу пользователю

                // получить пользователя и книги по ID
                var book1 = bookReposit.SelectById(1);
                var book2 = bookReposit.SelectById(2);
                var user = userReposit.SelectById(1);

                // выдать книги пользователю
                user.ListBooks.AddRange( new[] { book2, book1 } );
                db.SaveChanges();

                //foreach(Book ub in user.ListBooks)
                //{
                //    Console.WriteLine($"книга {ub.BookId}, {ub.BookName}");
                //}

                // Получать количество книг на руках у пользователя.
                var i = bookReposit.QuantityBookUser(1);
                Console.WriteLine();
                Console.WriteLine($"Количество книг на руках у пользователя {i}");

                var f = bookReposit.UserHaveBook(1, 1);
                Console.WriteLine();
                Console.WriteLine($"Есть ли книга? - {f}");
            }

            Console.WriteLine();
            Console.WriteLine("Дело сделано!");
            Console.ReadKey();
        }
    }
}
