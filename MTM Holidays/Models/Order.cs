using System.ComponentModel.DataAnnotations;

namespace MTM_Holidays.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [Required, DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CustomerID { get; set; }
        // To do later on: Remove after creation of entity Customer
        //public Customer Customer { get; set; }

    }
}
