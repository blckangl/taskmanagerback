using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskmanagerback.Models
{
    public class TaskManagerContext : DbContext
    {
        public DbSet<Task> tasks { get; set; }

        public DbSet<ItemsList> Itemlist { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=taskmanagerdb;user=root;password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemsList>(entity =>
            {
                entity.HasKey(e => e.ItemsListId);
                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.TaskId);
                entity.Property(e => e.Title).IsRequired();
                entity.HasOne(d => d.ItemsList)
                                 .WithMany(p => p.Tasks);
            });
        }
    }
}

