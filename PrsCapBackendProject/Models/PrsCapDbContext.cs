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


        // FLUENT API SYNTAX ?

    }
}
