using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class Request {  //Database-first classes are "partial".  Do I need to do that?

        public int Id { get; set; }    // auto-generates to (1,1)
        [Required]
        [StringLength(80)]
        public string Description { get; set; }
        [Required]
        [StringLength(80)]
        public string Justification { get; set; }
        [StringLength(80)]
        public string RejectionReason { get; set; }
        [Required]
        [StringLength(20)]
        public string DeliveryMode { get; set; } = "Pickup";
        [Required]
        [StringLength(10)]
        public string Status { get; set; } = "NEW";
        [Column(TypeName = "decimal(11, 2)")]
        public decimal Total { get; set; } = 0;  // "Required" attribute not necessary, since bool, numbers can't be null
        public int UserId { get; set; }  // "Required" attribute not necessary, since bool, numbers can't be null

        // Specs: "There should be a virtual `User` instance in the Request to hold the FK instance when reading a Request"
        public virtual User User { get; set; }

        // Specs: "There should be a virtual collection of `RequestLine` instances in the Request to hold the collection of lines related to this Request."
        public virtual ICollection<RequestLine> RequestLines { get; set; }


        public Request() {

        }



    }
}
