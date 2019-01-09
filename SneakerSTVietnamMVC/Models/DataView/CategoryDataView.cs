using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SneakerSTVietnamMVC.Models.DataView
{
    public class CategoryUploadView
    {
        [Required]
        [Display (Name = "Category ID (*)")]
        public int CategoryID { get; set; }
        [Required]
        [Display (Name = "Category Name (*)")]
        public string CategoryName { get; set; }
        [Required]
        [Display (Name = "Category Logo (*)")]
        public HttpPostedFileBase CategoryLogo { get; set; }
        [Required]
        [Display (Name = "Category Description (*)")]
        public string CategoryDescription { get; set; }
    }
}