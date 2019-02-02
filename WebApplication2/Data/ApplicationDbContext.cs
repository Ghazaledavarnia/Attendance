using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUseres { get; set; }
        public DbSet<EntranceExit> EntranceExits { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(builder);
    //    modelBuilder.Entity<ApplicationUser>().ToTable("Person");
    //    modelBuilder.Entity<EntranceExit>().ToTable("EntranceExit")/*.HasKey(d => new { d.Id, d.IdP })*/;
    //}
    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("User");
            builder.Entity<EntranceExit>(entity =>
            {
                entity.ToTable("EntranceExit");
            });
            //builder.Entity<Leave>().HasForeignKey(s =>s.);
            //.HasOne<Grade>(s => s.Grade)
            //.WithMany(g => g.Students)

            //.OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<EntranceExit>().ToTable("EntranceExit").HasKey(d => d.);
        }

        private object modelBuilderEntity<T>()
        {
            throw new NotImplementedException();
        }
    }
}
