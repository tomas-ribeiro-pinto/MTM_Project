using System;
using System.ComponentModel.DataAnnotations;

namespace MTM_Holidays.Models
{
    /// <summary>
    /// This class models the data for Order_Holiday entity in the database.
    /// 
    /// Orders can have more than one holiday linked and that's the purpose of this entity.
    /// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>12th Jan 2023</version>
    public class Order_Holiday
	{
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(1,4, ErrorMessage = "The field {0} must be between {1} and {2}")]
        public int Quantity { get; set; } = 1;

        // Number of nights. There is a minimum of 2 nights per order
        [Required]
        [Range(2, 8, ErrorMessage = "The field {0} must be between {1} and {2}")]
        public int Night { get; set; } = 2;

        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public int HolidayID { get; set; }
        public virtual Holiday Holiday { get; set; } = default!;

        public int OrderID { get; set; }
        public virtual Order Order { get; set; } = default!;
    }
}

