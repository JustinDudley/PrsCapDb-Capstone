using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class Vendor {

        public int Id { get; set; }  // how to auto-generate (1,1)?
        // How to make "Code" unique??  (Unique in specs)
        [Required]
        [StringLength(30)]
        public string Code { get; set; }
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
