using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using Microsoft.EntityFrameworkCore; 
using System.Threading.Tasks; 

namespace Remont 
{
    /// <summary>
    /// Контекст базы данных для приложения Remont, наследующий от DbContext.
    /// Позволяет взаимодействовать с базой данных, содержащей заявки и оборудование.
    /// </summary> 
    public class RemontDBContext : DbContext 
    {
        /// <summary>
        /// Набор данных для работы с заявками.
        /// </summary>
        public DbSet<Request> Requests { get; set; }

        /// <summary>
        /// Набор данных для работы с оборудованием.
        /// </summary>
        public DbSet<Equipment> Equipments { get; set; }

        /// <summary>
        /// Метод для настройки параметров контекста базы данных.
        /// </summary>
        /// <param name="optionsBuilder">Объект для настройки параметров базы данных.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RemontDB;Trusted_Connection=True;"); 
        }
    }
    
}
