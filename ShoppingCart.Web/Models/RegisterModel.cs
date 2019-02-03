using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Web
{
    public class RegisterModel
    {
        [Required]
        [Remote("IsUserNameValid", "Account")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter emailId")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter mobileNo")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Please enter addressLine1")]
        [Display(Name = "Address Line1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address Line2")]
        public string AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [DataType(DataType.DateTime)]
        public System.DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
    [MetadataType(typeof(RegisterModel))]
    public partial class UserProfile
    { }
}