using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapBackendProject.Models {
    public class RequestLine {

        public int Id { get; set; }     // auto-generates to (1,1)
        public int Quantity { get; set; } = 1;
        public int RequestId { get; set; }
        public int ProductId { get; set; }

        // Specs: "There should be a virtual `Product` instance in the RequestLine to hold the FK instance when reading a RequestLine for the Product."

        public virtual Product Product { get; set; }


        //NOT SURE IF I SHOULD ADD 2 LINES CODE BELOW.  9/24:
        [JsonIgnore]
        public virtual Request Request { get; set; }




        public RequestLine() {

        }

    }
}
