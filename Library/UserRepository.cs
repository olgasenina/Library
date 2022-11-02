using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Репозиторий функций для работы с пользователями:
    /// выбор объекта из БД по его идентификатору, 
    /// выбор всех объектов, 
    /// добавление объекта в БД и его удаление из БД
    /// обновление имени пользователя (по Id)
    /// </summary>
    public class UserRepository
    {
        AppContext db;

        public UserRepository(AppContext appContext)
        {
            db = appContext;

            var user1 = new User { UserName = "Ольга", Email = "Olga@yandex.ru", PhoneNumber = "+7(915)305-14-98" };
            var user2 = new User { UserName = "Дмитрий", Email = "Dmitry@yandex.ru", PhoneNumber = "+7(915)788-23-16" };

            db.Users.AddRange(user1, user2);

            db.SaveChanges();
        }

        /// <summary>
        /// выбор всех объектов
        /// </summary>
        /// <returns></returns>
        public List<User> AllSelect()
        {
            var allBooks = db.Users.ToList();
            return allBooks;
        }

        /// <summary>
        /// Выбор объекта из БД по его Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        public User SelectById(int id)
        {
            var thisUser = db.Users.Where(user => user.UserId == id).FirstOrDefault();

            return thisUser;
        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        public void AddUser()
        {
            Console.WriteLine("Введите данные для нового пользователя.");

            Console.WriteLine("Имя: ");
            string userName = Console.ReadLine();

            Console.WriteLine("Email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Номер телефона: ");
            string phoneNumber = Console.ReadLine();

            var user = new User { UserName = userName, Email = email, PhoneNumber = phoneNumber };
            db.Users.Add(user);
            db.SaveChanges();
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        public void DeleteUser(int id)
        {
            var user = SelectById(id);  // найдем пользователя по id
            db.Users.Remove(user);      // операция удаления
            db.SaveChanges();           // сохранить изменения в БД
        }

        /// <summary>
        /// Обновление имени пользователя (по Id)
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="newName">новое имя</param>
        public void UpdateUser(int id, string newName)
        {
            var user = SelectById(id);  // найдем пользователя по id
            user.UserName = newName;
            db.SaveChanges();
        }

        /// <summary>
        /// Вывести на экран список пользователей
        /// </summary>
        /// <param name="listUsers"></param>
        public void UserPrint(List<User> listUsers)
        {
            Console.WriteLine();
            Console.WriteLine("Список всех пользователей:");

            foreach (var u in listUsers)
            {
                Console.WriteLine($"ID = {u.UserId}, имя пользователя {u.UserName}, Email {u.Email}, телефон {u.PhoneNumber}");
            }
        }
    }
}
