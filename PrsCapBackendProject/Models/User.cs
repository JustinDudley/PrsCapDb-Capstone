using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class User {     //Database-first classes are "partial".  Do I need to do that?

        //PROPERTIES
        [Required]
        public int Id { get; set; }    // auto-generates, (1,1) // Should this be private set??
        [Required]
        [StringLength(30)]
        public string Username { get; set; } // Uniqueness of Username is set using Fluent API Syntax, in PrsCapDbContext file
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
        public bool IsReviewer { get; set; } = false;  // "Required" attribute not necessary, since bool, numbers can't be null
        public bool IsAdmin { get; set; } = false;  // "Required" attribute not necessary, since bool, numbers can't be null

        public User() {

        }


    }
}
