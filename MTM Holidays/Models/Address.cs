using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
namespace MTM_Holidays.Models
{
        /// <summary>
        /// This class models the data for Address entity in the database.
        /// 
        /// 
        /// Address model contain the propetries needed to describe all the adreess details.
        /// 
        /// </summary>
        /// <author> Milena Michalska </author>
        /// <version>5th Jan 2023</version>
    public class Address
    {

            [Key]
            public int ID { get; set; }

            [MaxLength(30, ErrorMessage = "The field {0} is too long")]
            public string Street { get; set; } = String.Empty;

            [Required, MaxLength(40, ErrorMessage = "The field {0} is too long")]
            public string Town { get; set; } = String.Empty;

            [Range(5, 7, ErrorMessage = "The field {0} must be between {1} and {2}")]
            public string PostCode { get; set; } = string.Empty;

            [MaxLength(30, ErrorMessage = "The field {0} is too long")]
            public string County { get; set; } = String.Empty;

            [Required, MaxLength(20, ErrorMessage = "The field {0} is too long")]
            public string Country { get; set; } = string.Empty;

            [MaxLength(20, ErrorMessage = "The field {0} is too long")]
            public string Region { get; set; } = String.Empty;

           
      }
}
