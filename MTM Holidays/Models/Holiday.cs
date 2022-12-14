using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MTM_Holidays.Models
{
    /// <summary>
    /// This class models the data for Holiday entity in the database.
	/// 
    /// Holidays can have start and end dates according to customer needs.
    /// The price reflects the cost per customer per day in the holiday.
    /// There should be a minimum of 2 day interval between start and end date.
	/// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>14th Dec 2022</version>
    public class Holiday
	{
		[Key]
        public int HolidayID { get; set; }

		[Required, MaxLength(30, ErrorMessage = "The field {0} is too long")]
		public string Title { get; set; } = String.Empty;

		[MaxLength(100)]
		public string Description { get; set; } = String.Empty;

		// Price per day of the Holiday
		[Required]
		[DataType(DataType.Currency)]
		[Range(0.0, 10000, ErrorMessage = "The field {0} must be between {1} and {2}")]
		public double Price { get; set; } = 0;

		[DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime StartDate { get; set; } = DateTime.Now;

		// There should be a minimum of 2 day interval
		// for each holiday. Needs to be updated to reflect start date...
		[DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; } = DateTime.Now;

		[Required, MaxLength(50, ErrorMessage = "The field {0} is too long")]
		public string Region { get; set; } = String.Empty;

		[Range(1, 5, ErrorMessage = "The field {0} must be between {1} and {2}")]
		public int Rating { get; set; }

		[MaxLength(30, ErrorMessage = "The field {0} is too long")]
		public string AccommodationType { get; set; } = String.Empty;

		public int OriginAddressID { get; set; }
		//TODO Remove after creation of entity Address
		//public Address OriginAddress { get; set; }

        public int DestinationAddressID { get; set; }
        //TODO Remove after creation of entity Address
        //public Address DestinationAddress { get; set; }
    }
}

