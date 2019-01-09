using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SneakerSTVietnamMVC.Models.DataView
{
    public class ViewRegisterModel
    {
        [Required]
        [Display(Name = "Email (*)")]
        [StringLength(50, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password (*)")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "First Name (*)")]
        [StringLength(20, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name (*)")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Gender (*)")]
        public bool Gender { get; set; }
        [Required]
        [Display(Name = "Address (*)")]
        [MaxLength(250)]
        public string Address { get; set; }
        [Required]
        [Display(Name = "City (*)")]
        [MaxLength(50)]
        public string City { get; set; }
        [Display(Name = "Postcode")]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Phone Number (*)")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[\d]{7,20}$", ErrorMessage = "Invalid phone number")]
        [StringLength(20, MinimumLength = 7)]
        public string PhoneNumber { get; set; }
        public int RoleID { get; set; }
        public bool Active { get; set; }
        [Display(Name = "Receive email from us?")]
        public bool ReceiveEmail { get; set; }
        [Required]
        [Display(Name = "Country (*)")]
        [MaxLength(50)]
        public string Country { get; set; }
    }

    public class ViewLoginModel
    {
        [Required]
        [Display(Name = "Email (*)")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password (*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ViewConfirmModel
    {
        public int UserID { get; set; }
        public string SecurityCode { get; set; }
        public bool Active { get; set; }
    }

    public class ViewEmailConfirm
    {
        [Required]
        [Display(Name = "Email (*)")]
        [StringLength(50, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ViewActiveModel
    {
        public int UserID { get; set; }
        public bool Active { get; set; }
    }

    public class ViewUserDataModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }

    public class ViewChangePassword {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password: (*)")]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password: (*)")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password: (*)")]
        public string ConfirmNewPassword { get; set; }
    }
}