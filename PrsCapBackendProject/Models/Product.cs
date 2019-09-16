using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class Product {      //Database-first classes are "partial".  Do I need to do that?

        public int Id { get; set; }    // how to auto-generate (1,1) ?
        [Required]
        [StringLength(30)]
        public string PartNbr { get; set; }  // how to make PartNbr unique??
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        // The Attribute "Required" isn't necessary here?  [Required]
        [Column(TypeName = "decimal(11, 2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(30)]
        public string Unit { get; set; }
        [StringLength(255)]
        public string PhotoPath { get; set; }
        [Required]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
        /*  In specs.MD:  "There should be a virtual `Vendor` instance in the Product to hold the FK instance when reading a Product."
             -- Does this relate to lazy loading?         */


        public Product() {

        }


    }
}
