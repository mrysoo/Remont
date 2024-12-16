using Microsoft.EntityFrameworkCore; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel; 
using System.Data; 
using System.Drawing;
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using System.Windows.Forms; 


namespace Remont 
{
    /// <summary>
    /// Основная форма приложения для управления заявками на ремонт оборудования.
    /// </summary>
    public partial class Form1 : Form 
    {
        private RemontDBContext _context; 
        private Request _selectedRequest;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Form1"/> и загружает данные.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            _context = new RemontDBContext();
            LoadRequests(); 
        }


        /// <summary>
        /// Обработчик события загрузки формы.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e) 
        {
           
        }

        /// <summary>
        /// Обработчик нажатия кнопки для добавления или обновления заявки.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void buttonAddRequest_Click(object sender, EventArgs e) 
        {
            var equipmentName = textBoxEquipmentName.Text; 
            var equipment = _context.Equipments 
                .AsEnumerable()  
                .FirstOrDefault(eq => eq.EquipmentName.Equals(equipmentName, StringComparison.OrdinalIgnoreCase)); 

            if (equipment == null) 
            {
                MessageBox.Show("Оборудование не найдено. Пожалуйста, проверьте название."); 
                return; 
            }

            if (_selectedRequest == null)  
            {
                var newRequest = new Request 
                {
                    EquipmentID = equipment.EquipmentID, 
                    RequestDate = DateTime.Now, 
                    Status = "Создана", 
                    Description = textBoxDescription.Text 
                };

                _context.Requests.Add(newRequest); 
            }
            else 
            {
                _selectedRequest.EquipmentID = equipment.EquipmentID;
                _selectedRequest.Description = textBoxDescription.Text; 
                _selectedRequest.Status = "Обновлена"; 
            }

            _context.SaveChanges();  
            LoadRequests(); 
            ClearForm(); 
        }

        /// <summary>
        /// Обработчик нажатия кнопки для удаления выбранной заявки.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void buttonDeleteRequest_Click(object sender, EventArgs e) 
        {
            if (_selectedRequest != null)  
            {
                _context.Requests.Remove(_selectedRequest);     
                _context.SaveChanges();     
                LoadRequests(); 
                ClearForm(); 
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления."); 
            }
        }

        /// <summary>
        /// Обработчик события клика по ячейке в таблице заявок.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void dataGridViewRequests_CellContentClick(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex >= 0) 
            {
                _selectedRequest = (Request)dataGridViewRequests.Rows[e.RowIndex].DataBoundItem;
                var equipment = _context.Equipments.FirstOrDefault(eq => eq.EquipmentID == _selectedRequest.EquipmentID); 

                
                textBoxEquipmentName.Text = equipment?.EquipmentName; 
                textBoxDescription.Text = _selectedRequest.Description; 
            }
        }

        /// <summary>
        /// Очищает форму для ввода новой заявки.
        /// </summary>
        private void ClearForm() 
        {
            _selectedRequest = null; 
            textBoxEquipmentName.Clear(); 
            textBoxDescription.Clear(); 
        }


        /// <summary>
        /// Загружает список заявок из базы данных и отображает их в таблице.
        /// </summary>
        private void LoadRequests() 
        {
            var requests = _context.Requests.Include(r => r.Equipment).ToList(); 
            dataGridViewRequests.DataSource = requests; 
        }
    }

}
