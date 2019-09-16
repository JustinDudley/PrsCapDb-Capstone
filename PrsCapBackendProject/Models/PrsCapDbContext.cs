using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class PrsCapDbContext : DbContext {

        // must have tables. must have classes
        // As soon as create class, put this in here.  
        // Otherwise, can't access, can't migrate. Need to define things.
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }      
        public virtual DbSet<Request> Requests { get; set; }  
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RequestLine> RequestLines { get; set; }


        public PrsCapDbContext(DbContextOptions<PrsCapDbContext> context) : base(context) {
        }



        // UNIQUENESS OF A COLUMN.  Done with "Fluent API syntax". 
        // This method (called by the system) ensures that certain columns in the three tables below are set to be unique. 
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>(entity => {
                entity.HasIndex(e => e.Username)
                .HasName("IDX_Username")
                    .IsUnique();
            });

            modelBuilder.Entity<Vendor>(entity => {
                entity.HasIndex(e => e.Code)
                .HasName("IDX_Code")
                    .IsUnique();
            });

            modelBuilder.Entity<Product>(entity => {
                entity.HasIndex(e => e.PartNbr)
                .HasName("IDX_PartNbr")
                    .IsUnique();
            });
        }




    }
}
