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

        [Required, MaxLength(10, ErrorMessage = "The field {0} is too long")]
        [DataType(DataType.CreditCard)]
        public int CardNumber { get; set; }


        [Required, StringLength(3, ErrorMessage = "The field {0} must be {1} characters long}")]
        public string SecurityCode { get; set; }


        [Required, DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: MM/yyyy}")]
        public DateTime ExpiryDate { get; set; }


    }
}
