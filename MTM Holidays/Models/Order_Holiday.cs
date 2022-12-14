using System;
using System.ComponentModel.DataAnnotations;

namespace MTM_Holidays.Models
{
	public class Order_Holiday
	{
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Invalid format for field {0}")]
        public double Discount { get; set; } = 0;

        [Required]
        public bool IsPaid { get; set; } = false;

        public int HolidayID { get; set; }
        public Holiday Holiday { get; set; }

        public int OrderID { get; set; }
        // TODO: Remove after creation of entity Order
        //public Order Order { get; set; }

    }
}

