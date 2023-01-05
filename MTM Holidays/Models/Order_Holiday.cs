using System;
using System.ComponentModel.DataAnnotations;

namespace MTM_Holidays.Models
{
    /// <summary>
    /// This class models the data for Order_Holiday entity in the database.
    /// 
    /// Orders can have more than one holiday linked and that's the purpose of this entity.
    /// There is a very simple verification for payment received using a bool attribute.
    /// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>15th Dec 2022</version>
    public class Order_Holiday
	{
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Invalid format for field {0}")]
        public double Discount { get; set; } = 0;

        [Required]
        public bool IsPaid { get; set; } = false;

        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        // There should be a minimum of 2 day interval
        // for each holiday. Needs to be updated to reflect start date...
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public int HolidayID { get; set; }
        public Holiday Holiday { get; set; }

        public int OrderID { get; set; }
        // TODO: Remove after creation of entity Order
        //public Order Order { get; set; }


        public int DiscountCodeID { get; set; }
        public DiscountCode DiscountCode { get; set; } 
    }
}

