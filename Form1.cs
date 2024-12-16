using Microsoft.EntityFrameworkCore; // Импортируем пространство имен для работы с Entity Framework Core.
using System; // Импортируем пространство имен для базовых типов данных.
using System.Collections.Generic; // Импортируем пространство имен для работы с коллекциями.
using System.ComponentModel; // Импортируем пространство имен для работы с компонентами и их свойствами.
using System.Data; // Импортируем пространство имен для работы с данными.
using System.Drawing; // Импортируем пространство имен для работы с графикой.
using System.Linq; // Импортируем пространство имен для работы с LINQ.
using System.Text; // Импортируем пространство имен для работы с текстом.
using System.Threading.Tasks; // Импортируем пространство имен для работы с асинхронным программированием.
using System.Windows.Forms; // Импортируем пространство имен для работы с Windows Forms.


namespace Remont // Определяем пространство имен для нашего приложения.
{
    public partial class Form1 : Form // Определяем класс Form1, который наследуется от класса Form.
    {
        private RemontDBContext _context; // Создаем переменную для контекста базы данных.
        private Request _selectedRequest; // Создаем переменную для хранения выбранной заявки.

        public Form1() // Конструктор класса Form1.
        {
            InitializeComponent(); // Инициализируем компоненты формы.
            _context = new RemontDBContext(); // Создаем новый экземпляр контекста базы данных.
            LoadRequests(); // Загружаем заявки из базы данных при инициализации формы.
        }

        private void Form1_Load(object sender, EventArgs e) // Обработчик события загрузки формы.
        {
           
        }

        private void buttonAddRequest_Click(object sender, EventArgs e) // Обработчик нажатия кнопки добавления заявки.
        {
            var equipmentName = textBoxEquipmentName.Text; // Получаем название оборудования из текстового поля.
            var equipment = _context.Equipments // Ищем оборудование в базе данных.
                .AsEnumerable()  // Преобразуем результат в перечисляемую коллекцию.
                .FirstOrDefault(eq => eq.EquipmentName.Equals(equipmentName, StringComparison.OrdinalIgnoreCase)); // Находим первое оборудование с совпадающим именем.

            if (equipment == null) // Проверяем, найдено ли оборудование.
            {
                MessageBox.Show("Оборудование не найдено. Пожалуйста, проверьте название."); // Выводим сообщение об ошибке, если оборудование не найдено.
                return; // Завершаем выполнение метода.
            }

            if (_selectedRequest == null)  // Проверяем, была ли выбрана существующая заявка.
            {
                var newRequest = new Request // Создаем новую заявку.
                {
                    EquipmentID = equipment.EquipmentID, // Устанавливаем ID оборудования.
                    RequestDate = DateTime.Now, // Устанавливаем дату заявки на текущее время.
                    Status = "Создана", // Устанавливаем статус заявки.
                    Description = textBoxDescription.Text // Устанавливаем описание заявки из текстового поля.
                };

                _context.Requests.Add(newRequest); // Добавляем новую заявку в контекст базы данных.
            }
            else // Если заявка уже была выбрана для редактирования.
            {
                _selectedRequest.EquipmentID = equipment.EquipmentID; // Обновляем ID оборудования в выбранной заявке.
                _selectedRequest.Description = textBoxDescription.Text; // Обновляем описание заявки.
                _selectedRequest.Status = "Обновлена";  // Обновляем статус заявки.
            }

            _context.SaveChanges();  // Сохраняем изменения в базе данных.
            LoadRequests(); // Загружаем обновленный список заявок.
            ClearForm(); // Очищаем форму для ввода новой заявки.
        }

        private void buttonDeleteRequest_Click(object sender, EventArgs e) // Обработчик нажатия кнопки удаления заявки.
        {
            if (_selectedRequest != null) // Проверяем, была ли выбрана заявка для удаления.
            {
                _context.Requests.Remove(_selectedRequest); // Удаляем выбранную заявку из контекста базы данных.
                _context.SaveChanges(); // Сохраняем изменения в базе данных.
                LoadRequests(); // Загружаем обновленный список заявок.
                ClearForm(); // Очищаем форму.
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления."); // Если заявка не была выбрана.
            }
        }

        private void dataGridViewRequests_CellContentClick(object sender, DataGridViewCellEventArgs e) // Обработчик события клика по ячейке в таблице заявок.
        {
            if (e.RowIndex >= 0) // Проверяем, что клик был по действительной строке.
            {
                _selectedRequest = (Request)dataGridViewRequests.Rows[e.RowIndex].DataBoundItem; // Получаем выбранную заявку из таблицы.
                var equipment = _context.Equipments.FirstOrDefault(eq => eq.EquipmentID == _selectedRequest.EquipmentID); // Находим оборудование по ID в выбранной заявке.

                // Заполняем текстовые поля данными выбранной заявки.
                textBoxEquipmentName.Text = equipment?.EquipmentName; // Устанавливаем название оборудования в текстовое поле.
                textBoxDescription.Text = _selectedRequest.Description; // Устанавливаем описание заявки в текстовое поле.
            }
        }
        private void ClearForm() // Метод для очистки формы.
        {
            _selectedRequest = null; // Сбрасываем выбранную заявку.
            textBoxEquipmentName.Clear(); // Очищаем текстовое поле с названием оборудования.
            textBoxDescription.Clear(); // Очищаем текстовое поле с описанием заявки.
        }

        private void LoadRequests() // Метод для загрузки заявок из базы данных.
        {
            var requests = _context.Requests.Include(r => r.Equipment).ToList(); // Загружаем заявки с включением связанных данных об оборудовании.
            dataGridViewRequests.DataSource = requests; // Устанавливаем источник данных для таблицы заявок.
        }
    }

}
