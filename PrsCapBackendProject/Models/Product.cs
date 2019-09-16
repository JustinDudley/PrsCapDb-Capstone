using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class Product {      //Database-first classes are "partial", but not these apparently

        public int Id { get; set; }    // auto-generates to (1,1)
        [Required]
        [StringLength(30)]
        public string PartNbr { get; set; }  // Uniqueness of PartNbr is set using Fluent API Syntax, in PrsCapDbContext file 
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(11, 2)")]
        public decimal Price { get; set; }  // "Required" attribute not necessary, since bool, numbers can't be null
        [Required]
        [StringLength(30)]
        public string Unit { get; set; }
        [StringLength(255)]
        public string PhotoPath { get; set; }
        [Required]
        public int VendorId { get; set; }

        // in specs: "Product class should have a virtual "Vendor" instance, to hold the FK instance when reading a Product." -?relates to lazy loading?
        public virtual Vendor Vendor { get; set; }
       


        public Product() {

        }


    }
}
