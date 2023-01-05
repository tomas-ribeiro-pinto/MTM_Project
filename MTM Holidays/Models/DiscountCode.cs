using System;
using System.ComponentModel.DataAnnotations;

namespace MTM_Holidays.Models
{
    /// <summary>
    /// This class is a model for Discount Codes.
    /// 
    /// Users can apply a discount code at checkout.
    /// This is a simple system of using codes already in the database.
    ///
    /// TODO: Could be improved to have expire date and be tracked and used by unique order.
    /// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>5th Jan 2023</version>
    public class DiscountCode
	{
        [Key]
        public int ID { get; set; }

        [Required]
        public string Code { get; set; } = String.Empty;

        [DataType(DataType.Currency, ErrorMessage = "Invalid format for field {0}")]
        public double Discount { get; set; } = 0;
    }
}

