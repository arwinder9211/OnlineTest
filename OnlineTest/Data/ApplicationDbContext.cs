using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineTest.Models;

namespace OnlineTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<TestQues> TestQues { get; set; }
        public DbSet<TestStu> TestStus { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<TestQues>()
                 .HasKey(bc => new { bc.TestId, bc.QuestionId });
            builder.Entity<TestQues>()
                .HasOne(bc => bc.Test)
                .WithMany(b => b.TestQues)
                .HasForeignKey(bc => bc.TestId);
            builder.Entity<TestQues>()
                .HasOne(bc => bc.Question)
                .WithMany(c => c.TestQues)
                .HasForeignKey(bc => bc.QuestionId);

            builder.Entity<TestStu>()
                 .HasKey(bc => new { bc.TestId, bc.StudentId });
            builder.Entity<TestStu>()
                .HasOne(bc => bc.Test)
                .WithMany(b => b.TestStus)
                .HasForeignKey(bc => bc.TestId);
            builder.Entity<TestStu>()
                .HasOne(bc => bc.Student)
                .WithMany(c => c.TestStus)
                .HasForeignKey(bc => bc.StudentId);
        }
    }
}
