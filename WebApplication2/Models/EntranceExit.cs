using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class EntranceExit

    {
        //[Key]


        public int Id { get; set; }
       // public string UserName { get; set; }
        public string EntranceDate { get; set; }
        public string EntranceTime { get; set; }
        public string ExitTime { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        //public int BlogForeignKey { get; set; }

        //[ForeignKey("BlogForeignKey")]
        //public Blog Blog { get; set; }

        //public ApplicationUser ApplicationUser { get; set; }

        //[ForeignKey("UserName")]
        //
    }
}
