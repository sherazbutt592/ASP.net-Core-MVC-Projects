using System.ComponentModel.DataAnnotations;

namespace ETickets.Data.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name ="Full Name")]
        [Required(ErrorMessage ="Full Name is Required")]
        public string FullName { get; set; }

        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Email Address is Required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage ="Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
