using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="ایمیل خود را وارد نمایید")]
        [EmailAddress(ErrorMessage = "فرمت ایمل صحیح نمی باشد")]
        [Display(Name = "ایمیل")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور خود را وارد نمایید")]
      //  [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور با رمز عبور یکسان نیست")]
        public string ConfirmPassword { get; set; }
    }
}
