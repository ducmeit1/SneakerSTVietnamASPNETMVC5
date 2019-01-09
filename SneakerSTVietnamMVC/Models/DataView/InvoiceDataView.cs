using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SneakerSTVietnamMVC.Models.DataView
{
    public class InvoiceDataView
    {
        [Required]
        [Display(Name = "Order Number: (*)")]
        public int InvoiceID { set; get; }
    }
}