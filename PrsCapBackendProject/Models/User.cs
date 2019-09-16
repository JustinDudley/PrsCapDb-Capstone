using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class User {

        //PROPERTIES
        int Id { get; set; }    // how to auto-generate? 
        [StringLength(30)]
        string Username { get; set; }
        [StringLength(30)]
        string Password { get; set; }
        [StringLength(30)]
        string Firstname { get; set; }
        [StringLength(30)]
        string Lastname { get; set; }
        [StringLength(12)]
        string Phone { get; set; } // specify string length? Do a CATCH in case system throws exception for too many digits entered?
        [StringLength(255)]
        string Email { get; set; }
        bool IsReviewer { get; set; } = false;
        bool IsAdmin { get; set; } = false;

    }
}
