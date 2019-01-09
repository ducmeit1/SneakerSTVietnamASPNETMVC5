using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SneakerSTVietnamMVC.Models
{
    public class BusinessDataView
    {
        public int BusinessID { get; set; }
        public string BusinessName { get; set; }
        public string BusinessDescription { get; set; }
        public string AreaName { get; set; }
        public bool IsGranted { get; set; }
    }

    public class BusinessDataAuthorizeView
    {
        public string BusinessName { get; set; }
        public string AreaName { get; set; }
    }

    public class BusinessViewModel
    {
        [Required]
        [Display(Name = "Business ID (*)")]
        public int BusinessID { get; set; }
        [Required]
        [Display(Name = "Business Name (*)")]
        public string BusinessName { get; set; }
        [Display(Name = "Description:")]
        public string BusinessDescription { get; set; }
        [Required]
        [Display(Name = "Area Name (*):")]
        public string AreaName { get; set; }
    }
}