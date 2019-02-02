using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.EntranceExitViewModel
{
    public class DailyLeaveViewModel
    {
        [Display(Name = "از تاریخ:")]
        [Required(ErrorMessage = "{0}را وارد نمایید")]
        public string StartDate { get; set; }
        [Display(Name = "تا تاریخ:")]
        [Required(ErrorMessage = "{0}را وارد نمایید")]
        public string EndDate { get; set; }
        [Display(Name = "نوع مرخصی:")]
        [Required(ErrorMessage = "{0}را وارد نمایید")]
        public int LeaveTypeD { get; set; }
        [Display(Name ="تاریخ ثبت:")]
        public string RegistrationDate { get; set; }

    }
}
