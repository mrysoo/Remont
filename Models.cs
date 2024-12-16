using System; // Импортируем пространство имен для базовых типов данных.
using System.Collections.Generic; // Импортируем пространство имен для работы с коллекциями.
using System.Linq; // Импортируем пространство имен для работы с LINQ.
using System.Text; // Импортируем пространство имен для работы с текстом.
using System.Threading.Tasks; // Импортируем пространство имен для работы с асинхронным программированием.

namespace Remont // Определяем пространство имен для нашего приложения.
{
 
    public class Request // Определяем класс Request, представляющий заявку.
    {
        public int RequestID { get; set; } // Уникальный идентификатор заявки.
        public int EquipmentID { get; set; } // Идентификатор оборудования, к которому относится заявка.
        public DateTime RequestDate { get; set; } // Дата создания заявки.
        public string Status { get; set; } // Статус заявки (например, "Создана", "Обработана").
        public string Description { get; set; } // Описание заявки.
        public virtual Equipment Equipment { get; set; } // Навигационное свойство для связи с классом Equipment.
    }

    public class Equipment // Определяем класс Equipment, представляющий оборудование.
    {
        public int EquipmentID { get; set; } // Уникальный идентификатор оборудования.
        public string EquipmentName { get; set; } // Название оборудования.
        public virtual ICollection<Request> Requests { get; set; } // Навигационное свойство для связи с коллекцией заявок.
    }
}
