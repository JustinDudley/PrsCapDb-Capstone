using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class User {     //Database-first classes are "partial".  Do I need to do that?

        //PROPERTIES
        [Required]
        public int Id { get; set; }    // how to auto-generate? How to set (1,1)? //  Should this be private set??
        //USERNAME MUST BE UNIQUE. See CustDbContext file from other solution?  Fluent API?
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        [Required]
        [StringLength(30)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(30)]
        public string Lastname { get; set; }
        [StringLength(12)]
        public string Phone { get; set; } // Do a CATCH in case system throws exception for too many digits entered?
        [StringLength(255)]
        public string Email { get; set; }
        // The "Required" attribute not necessary? If not, why not? [Required]
        public bool IsReviewer { get; set; } = false;
        // The "Required" attribute not necessary? If not, why not? [Required]
        public bool IsAdmin { get; set; } = false;

        public User() {

        }


    }
}
