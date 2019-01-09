using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SneakerSTVietnamMVC.Models.DataView
{
    public class SliderDataView
    {
        public int SlideID { get; set; }
        [Required]
        public string SlideName { get; set; }
        [Required]
        public string Slide_Description { get; set; }
        [Required]
        public HttpPostedFileBase SliderImage { get; set; }
        [Required]
        public string SlideLink { get; set; }
        [Required]
        public string SlideButtonName { get; set; }
    }
}