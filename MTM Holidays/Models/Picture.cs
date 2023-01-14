using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MTM_Holidays.Models
{
    /// <summary>
    /// This class models the data for Picture entity in the database.
	/// 
    /// Holidays can have pictures that will display on holiday details page
	/// 
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>1st Jan 2023</version>
    public class Picture
	{
		[Key]
        public int PictureID { get; set; }

        [Required, MaxLength(120, ErrorMessage = "The field {0} is too long")]
        public string URL { get; set; } = String.Empty;

        public int HolidayID { get; set; }
        public Holiday Holiday { get; set; } = default!;

    }
}

