using System.ComponentModel.DataAnnotations;

namespace MTM_Holidays.Models
{
    /// <summary>
    /// This class models the data for Card Payment entity in the database.
    /// 
    /// 
    /// Card Payment model contain the propetries needed to describe all the details connected with payment.
    /// 
    /// </summary>
    /// <author> Milena Michalska </author>
    /// <version>5th Jan 2023</version>
    public class CardPayment
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(16), MinLength(16, ErrorMessage = "The field {0} must be 10 characters long")]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }


        [Required, MinLength(3, ErrorMessage = "The field {0} must be {1} characters long"), MaxLength(3)]
        public string SecurityCode { get; set; }


        [Required, DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: MM/yyyy}")]
        public DateTime ExpiryDate { get; set; }


    }
}
