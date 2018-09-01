using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "نام کاربری اجباری است")]
        [EmailAddress(ErrorMessage = "ایمیل را اشتباه وارد کرده اید")]
        public string UserName { get; set; }
        //[RegularExpression(pattern: w//MVC1.Controllers.Infrastructure.RegularExpressions.Email,
        //                          // ErrorMessageResourceType = typeof(ggg.Resource),
        //                           // ErrorMessageResourceName = "EmailError")]
        //[Required(ErrorMessage = "پسورد [{0}]را اشتباه وارد کرده اید.")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
