using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Репозиторий функций для работы с книгами:
    /// выбор объекта из БД по его идентификатору, выбор всех объектов, добавление объекта в БД и его удаление из БД
    /// обновление года выпуска книги (по Id)
    /// </summary>
    public class BookRepository
    {
        AppContext db;
        public BookRepository(AppContext appContext)
        {
            db = appContext;

            // Справочник жанров

            var genre1 = new Genre { GenreName = "боевик" };
            var genre2 = new Genre { GenreName = "детектив" };
            var genre3 = new Genre { GenreName = "исторический роман" };
            var genre4 = new Genre { GenreName = "любовный роман" };
            var genre5 = new Genre { GenreName = "мистика" };
            var genre6 = new Genre { GenreName = "приключения" };
            var genre7 = new Genre { GenreName = "триллер/ужасы" };
            var genre8 = new Genre { GenreName = "фантастика" };
            var genre9 = new Genre { GenreName = "фэнтези/сказки" };
            var genre10 = new Genre { GenreName = "воспитание и образование" };
            var genre11 = new Genre { GenreName = "наука и техника" };
            var genre12 = new Genre { GenreName = "саморазвитие и психология" };
            var genre13 = new Genre { GenreName = "питание и кулинария" };
            var genre14 = new Genre { GenreName = "история" };

            // Сохраним справочник в БД
            db.Genres.AddRange(genre1, genre2, genre3, genre4, genre5, genre6, genre7, genre8, genre9, genre10, genre11, genre12, genre13, genre14);
            db.SaveChanges();

            // Книги

            var book1 = new Book { BookName = "Педагогическая поэма", BookAuthor = "Макаренко А.С.", PublishingHouse = "ООО Издательство АСТ", PublicationYear = 2022 };
            var book2 = new Book { BookName = "Ружья, микробы и сталь", BookAuthor = "Даймонд Д.", PublishingHouse = "ООО Издательство АСТ", PublicationYear = 2021 };
            var book3 = new Book { BookName = "Не рычите на собаку", BookAuthor = "Прайор К.", PublishingHouse = "ООО Издательство Эксмо", PublicationYear = 2021 };
            var book4 = new Book { BookName = "Обыкновенное чудо", BookAuthor = "Шварц Е.Л.", PublishingHouse = "ООО Издательство АСТ", PublicationYear = 2021 };

            // Определение жанра книг

            book1.GenreId = genre10.GenreId;
            book2.GenreId = genre14.GenreId;
            book3.GenreId = genre12.GenreId;
            book4.GenreId = genre9.GenreId;

            // Сохраняем данные в БД

            db.Books.AddRange(book1, book2, book3, book4);
            db.SaveChanges();
        }

        /// <summary>
        /// выбор всех объектов
        /// </summary>
        public List<Book> AllSelect()
        {
            var allBooks = db.Books.ToList();
            return allBooks;
        }

        /// <summary>
        /// Выбор объекта из БД по его Id
        /// </summary>
        /// <param name="id">Id книги</param>
        /// <returns></returns>
        public Book SelectById(int id)
        {
            var thisBook = db.Books.Where(book => book.BookId == id).FirstOrDefault();
            return thisBook;
        }

        /// <summary>
        /// Добавить книгу
        /// </summary>
        public void AddBook()
        {
            Console.WriteLine("Введите данные для новой книги.");

            Console.WriteLine("Наименование: ");
            string bookName = Console.ReadLine();

            Console.WriteLine("Автор: ");
            string bookAuthor = Console.ReadLine();

            Console.WriteLine("Издательство: ");
            string publishingHouse = Console.ReadLine();

            Console.WriteLine("Год издания: ");
            int publicationYear = int.Parse(Console.ReadLine());

            var book = new Book { BookName = bookName, BookAuthor = bookAuthor, PublishingHouse = publishingHouse, PublicationYear = publicationYear };
            db.Books.Add(book);
            db.SaveChanges();
        }

        /// <summary>
        /// Удалить книгу
        /// </summary>
        /// <param name="id">Id книги</param>
        public void DeleteBook(int id)
        {
            var book = SelectById(id);  // найдем книгу по id
            db.Books.Remove(book);      // операция удаления
            db.SaveChanges();           // сохранить изменения в БД
        }   

        /// <summary>
        /// Обновление года выпуска книги (по Id)
        /// </summary>
        public void UpdateBook(int id, int newPublicationYear)
        {
            var book = SelectById(id);  // найдем книгу по id
            book.PublicationYear = newPublicationYear;
            db.SaveChanges();
        }

        /// <summary>
        /// Получать список книг определенного жанра и вышедших между определенными годами.
        /// </summary>
        /// <param name="genreId">Id жанра</param>
        /// <param name="beginYear">год начала периода выборки</param>
        /// <param name="endYear">год окончания периода выборки</param>
        /// <returns></returns>
        public List<Book> BooksGenreList(int genreId, int beginYear, int endYear)
        {
            var bookQuery =
                from book in db.Books
                where book.GenreId == genreId && book.PublicationYear >= beginYear && book.PublicationYear <= endYear
                select book;

            var books = bookQuery.ToList();

            // тоже самое может быть записано также с помощью методов-расширений LINQ
            // var bookQuery = db.Books.Where(b => b.GenreId == genreId && b.PublicationYear >= beginYear && b.PublicationYear <= endYear);

            return books;
        }

        /// <summary>
        /// Получать количество книг определенного автора в библиотеке.
        /// </summary>
        /// <param name="authorName">Наименование автора</param>
        /// <returns></returns>
        public int QuantityBookAuthor(string authorName)
        {
            int q = db.Books.Count(b => b.BookAuthor == authorName);
            return q;
        }

        /// <summary>
        /// Получать количество книг определенного жанра в библиотеке.
        /// </summary>
        /// <param name="genreId">Id жанра</param>
        /// <returns></returns>
        public int QuantityBookGenre(int genreId)
        {
            int q = db.Books.Count(b => b.GenreId == genreId);
            return q;
        }

        /// <summary>
        /// Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
        /// </summary>
        /// <param name="bookName">Наименование книги</param>
        /// <param name="bookAuthor">Автор книги</param>
        /// <returns></returns>
        public bool BookSearch(string bookName, string bookAuthor)
        {
            bool b = db.Books.Any(b => b.BookName == bookName && b.BookAuthor == bookAuthor);
            return b;
        }

        /// <summary>
        /// Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="bookId">Id книги</param>
        /// <returns></returns>
        public bool UserHaveBook(int userId, int bookId)
        {
            var f = db.Users.Any(u => u.UserId == userId && u.ListBooks.Any(b => b.BookId == bookId));
            return f;
        }

        /// <summary>
        /// Получать количество книг на руках у пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns></returns>
        public int QuantityBookUser(int userId)
        {
            var q = db.Users.Where(u => u.UserId == userId).FirstOrDefault().ListBooks.Count();
            return q;
        }

        /// <summary>
        /// Получение последней вышедшей книги.
        /// </summary>
        /// <returns></returns>
        public Book LastBook()
        {
            var thisBook = db.Books.OrderByDescending(b => b.PublicationYear).FirstOrDefault();
            return thisBook;
        }

        /// <summary>
        /// Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        /// </summary>
        /// <returns></returns>
        public List<Book> AllBookOrderByName()
        {
            var list = db.Books.OrderBy(b => b.BookName).ToList();
            return list;
        }

        /// <summary>
        /// Получение списка всех книг, отсортированного в порядке убывания года их выхода.
        /// </summary>
        /// <returns></returns>
        public List<Book> AllBookOrderByYear()
        {
            var list = db.Books.OrderByDescending(b => b.PublicationYear).ToList();
            return list;
        }

        /// <summary>
        /// Вывести на экран список книг
        /// </summary>
        public void BookPrint(List<Book> listBooks)
        {
            Console.WriteLine();
            Console.WriteLine("Список всех книг:");

            var joinedBook = listBooks.Join(db.Genres, g => g.GenreId, b => b.GenreId, (b, g) => new { b.BookId, b.BookName, b.PublicationYear, g.GenreName}).ToList();

            foreach (var b in joinedBook)
            {
                Console.WriteLine($"ID = {b.BookId}, наименование книги \"{b.BookName}\", {b.PublicationYear} г., жанр книги - {b.GenreName}");
            }
        }
    }
}
