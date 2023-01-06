using System.ComponentModel.DataAnnotations;

namespace MTM_Holidays.Models
{
    /// <summary>
    /// This class models the data for Customer entity in the database.
    /// 
    /// Each order is placed by a customer, which details need to be stored in the database.
    /// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>5th Jan 2023</version>
    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(20, ErrorMessage = "The field {0} is too long")]
        public string FirstName { get; set; } = String.Empty;

        [Required, MaxLength(20, ErrorMessage = "The field {0} is too long")]
        public string LastName { get; set; } = String.Empty;

        [Required, DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address Format")]
        public string EmailAddress { get; set; } = String.Empty;

        [MinLength(9), MaxLength(11), DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set;}

        [Required, DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public int AddressID { get; set; }
        public Address Address { get; set; }

        public int CardPaymentID { get; set; }
        public CardPayment CardPayment { get; set; }

        public List<Order> Orders { get; set; }

    }
}
