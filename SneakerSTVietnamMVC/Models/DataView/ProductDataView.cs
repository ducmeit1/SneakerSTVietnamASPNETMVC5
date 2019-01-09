using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SneakerSTVietnamMVC.Models.DataView
{
    public class ProductDataView
    {
        [Required]
        [Display(Name = "Product ID: (*)")]
        public int ProductID { get; set; }
        [Required]
        [Display(Name = "Product Name: (*)")]
        public string ProductName { get; set; }
        [Display(Name = "Product Description:")]
        public string Description { get; set; }
        [Display(Name = "Detail:")]
        public string Detail { get; set; }
        [Required]
        [Display(Name = "Gender: ")]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Sell Price: (*)")]
        public double SellPrice { get; set; }
        public bool InStock { get; set; }
        [Required]
        [Display(Name = "Category: (*)")]
        public int CategoryID { get; set; }
        [Display(Name = "Is Display: (*)")]
        public bool IsDisplay { get; set; }
    }


    public partial class ProductModelRepresentView {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double SellPrice { get; set; }
        public string ImageLink { get; set; }
        public bool IsNew { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}