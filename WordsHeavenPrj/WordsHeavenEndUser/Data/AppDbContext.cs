using System;
using Microsoft.EntityFrameworkCore;
using WordsHeavenEndUser.Models;
using WordsHeavenEndUser.Models;

namespace WordsHeavenEndUser.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EndUser> Users { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specify the table name explicitly
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Category>().ToTable("Categories");

            // EndUser
            modelBuilder.Entity<EndUser>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<EndUser>()
                .HasIndex(u => u.Email)
                .IsUnique();



            // Review

            modelBuilder.Entity<Review>()
                .HasOne(r => r.EndUser)
            .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.EndUserId)
                .OnDelete(DeleteBehavior.Cascade);
 
        }


    }
}
