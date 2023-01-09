using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTM_Holidays.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        public List<Order_Holiday> Order_Holidays { get; set; }
    }
}
