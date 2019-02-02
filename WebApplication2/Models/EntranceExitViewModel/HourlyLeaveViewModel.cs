using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.EntranceExitViewModel
{
    public class HourlyLeaveViewModel
    {
        [Display(Name="ساعت شروع:")]
        [Required(ErrorMessage = "{0}را وارد نمایید")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        [DataType(DataType.Time)]
        [RegularExpression(pattern:"([01]?[0-9]|2[0-3]):[0-5][0-9]", ErrorMessage = "فرمت معتبر نمی باشد")]
        public string StartTime { get; set; }
        [Display(Name = "ساعت پایان:")]
        [Required(ErrorMessage = "{0}را وارد نمایید")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        [DataType(DataType.Time)]
        [RegularExpression(pattern: "([01]?[0-9]|2[0-3]):[0-5][0-9]", ErrorMessage = "فرمت معتبر نمی باشد")]
        public string EndTime { get; set; }
        
        [Display(Name = "نوع مرخصی:")]
        [Required(ErrorMessage = "{0}را وارد نمایید")]
        public int LeaveTypeH { get; set; }
        [Display(Name ="تاریخ ثبت:")]
        [Required(ErrorMessage = "{0}را وارد نمایید")]
        public string RegistrationDate { get; set; }




        //[Required]
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        // [DataType(DataType.Time)]
        //  [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
    }
}
