using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class Vendor {       //Database-first classes are "partial".  Do I need to do that?

        public int Id { get; set; }     // auto-generates to (1,1)
        [Required]
        [StringLength(30)]
        public string Code { get; set; }  // Uniqueness of Code is set using Fluent API Syntax, in PrsCapDbContext file
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Address { get; set; }
        [Required]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [StringLength(2)]   // Do a CATCH? 
        public string State { get; set; }  
        [Required]
        [StringLength(5)] // Do a CATCH? 
        public string Zip { get; set; }
        [StringLength(12)]  // Do a CATCH in case system throws exception for too many digits entered?
        public string Phone { get; set; }
        [StringLength(255)]
        public string Email { get; set; }


        public Vendor() {

        }
    }
}
