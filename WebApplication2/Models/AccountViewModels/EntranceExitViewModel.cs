using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.AccountViewModels
{
    public class EntranceExitViewModel
    {
        public int Id { get; set; }
        [Display(Name ="نام")]
        public string FirstName{ get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "شناسه کاربری")]
        public string Username { get; set; }
        [Display(Name = "تاریخ")]
        public string EntranceDate { get; set; }
        [Display(Name = "ساعت ورود")]
        public string EntranceTime { get; set; }
        [Display(Name = "ساعت خروج")]
        public string ExitTime { get; set; }
        [Display(Name = "ساعت حضور واقعی")]
        public string workTime { get; set; }
        [Display(Name = "از تاریخ:")]
        public string DateFrom { get; set; }
        [Display(Name = "تا تاریخ:")]
        public string DateTo { get; set; }
    }
}
