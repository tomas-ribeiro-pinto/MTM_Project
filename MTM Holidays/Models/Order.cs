using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTM_Holidays.Models
{
    /// <summary>
    /// This class models the Order Entity. Each order will has a customer linked to it
    /// and one to many Order_Holiday instances.
    /// 
    /// There is a very simple verification for payment received using a bool attribute.
    /// </summary>
    /// <author> Milena Michalska </author>
    /// <updatedBy>Tomás Pinto</updatedBy>
    /// <version>12th Jan 2023</version>
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CustomerID { get; set; }
        public Customer Customer { get; set; } = default!;

        public int? CardPaymentID { get; set; }
        public CardPayment? CardPayment { get; set; }

        public int? DiscountCodeID { get; set; }
        public DiscountCode? DiscountCode { get; set; }

        [Required]
        public bool IsPaid { get; set; } = false;

        public List<Order_Holiday> Order_Holidays { get; set; } = default!;

    }
}
