using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SneakerSTVietnamMVC.Models.DataView
{
    public class CheckoutDataView
    {
        public int InvoiceID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string DeliveryMethod { get; set; }
        public int InvoiceStatusID { get; set; }
        public int UserID { get; set; }
    }
}