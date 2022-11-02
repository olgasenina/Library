using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    /// <summary>
    /// Определяет контекст данных, используемый для взаимодействия с базой данных
    /// </summary>
    public class AppContext: DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public AppContext()
        {
            Database.EnsureDeleted();   // Удаление БД
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-EG1EDM0;Database=Library;Trusted_Connection=True;");
        }
    }
}
