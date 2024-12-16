using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Remont 
{
    /// <summary>
    /// Класс, представляющий заявку на обслуживание оборудования.
    /// </summary>
    public class Request 
    {
        public int RequestID { get; set; } 
        public int EquipmentID { get; set; } 
        public DateTime RequestDate { get; set; } 
        public string Status { get; set; } 
        public string Description { get; set; } 
        public virtual Equipment Equipment { get; set; } 
    }

    /// <summary>
    /// Класс, представляющий оборудование, связанное с заявками.
    /// </summary>
    public class Equipment 
    {
        public int EquipmentID { get; set; } 
        public string EquipmentName { get; set; } 
        public virtual ICollection<Request> Requests { get; set; } 
    }
}
