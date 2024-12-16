using System; // Импортируем пространство имен для базовых типов данных.
using System.Collections.Generic; // Импортируем пространство имен для работы с коллекциями.
using System.Linq; // Импортируем пространство имен для работы с LINQ.
using System.Text; // Импортируем пространство имен для работы с текстом.
using Microsoft.EntityFrameworkCore; // Импортируем пространство имен для работы с Entity Framework Core.
using System.Threading.Tasks; // Импортируем пространство имен для работы с асинхронным программированием.

namespace Remont // Определяем пространство имен для нашего приложения.
{
    public class RemontDBContext : DbContext // Определяем класс RemontDBContext, наследующий от DbContext для работы с базой данных.
    {
        public DbSet<Request> Requests { get; set; } // Определяем набор данных для работы с заявками.
        public DbSet<Equipment> Equipments { get; set; } // Определяем набор данных для работы с оборудованием.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Переопределяем метод для настройки параметров контекста базы данных.
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RemontDB;Trusted_Connection=True;"); // Строка подключения к базе данных
        }
    }
    
}
