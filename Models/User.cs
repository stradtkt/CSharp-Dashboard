using System;
using System.ComponentModel.DataAnnotations;
namespace Login.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        [Key]
        public long Id { get; set; }
 

        [Required(ErrorMessage="First Name is required")]
        [MinLength(2, ErrorMessage="You must have at least 2 names for first name")]
        [Display(Name="First Name")]
        public string first_name {get;set;}

        [Required(ErrorMessage="Last Name is required")]
        [MinLength(2, ErrorMessage="You must have at least 2 names for last name")]
        [Display(Name="Last Name")]
        public string last_name {get;set;}

        [EmailAddress]
        [Required(ErrorMessage="Email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Email")]
        public string email {get;set;}

        [Required(ErrorMessage="Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="You must have at least 8 characters for your password")]
        [Display(Name="Password")]
        public string password {get;set;}

        [Required(ErrorMessage="Confirm is required")]
        [Compare("password")]
        [Display(Name="Confirm Password")]
        public string confirm {get;set;}

        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
    }
}