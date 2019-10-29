using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsCapBackendProject.Models;

namespace PrsCapBackendProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PrsCapDbContext _context;

        private const string StatusIsReview = "REVIEW";
        private const string StatusIsApproved = "APPROVED";
        private const string StatusIsRejected = "REJECTED";


        public RequestsController(PrsCapDbContext context)
        {
            _context = context;
        }


        // return list of requests to reviewer: All requests that are 
        // in REVIEW status, but do NOT belong to the reviewer.
        // GET: api/Requests/list/5
        [HttpGet("list/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsToReview(int userId) {
            var requests = await _context.Requests.ToListAsync();
            return requests.Where(e => e.Status == StatusIsReview && e.UserId != userId).ToList();
        }
           

        private async Task<bool> SetStatusValidAsync(int id, string statusUpdate) {
            var request = await _context.Requests.FindAsync(id);
            if (request == null) { 
                return false;
            }
            request.Status = statusUpdate;
            _context.SaveChanges();
            return true;
        }

        // Takes id (from reviewer), changes status to "APPROVED" 
        // PUT: api/Requests/approve/5
        [HttpPut("approve/{id}")]    // attribute, in brackets, is what listens for and hears Postman's http call (URL)  eg. approve/5.  AND, this line determines that the value 5 is passed into the method 
        public async Task<IActionResult> PutStatusApproved(int id) {  // Note:  Do NOT send an instance in the body of postman for this method.  (Not sure if this would get ignored, or actually misused)
            if (await SetStatusValidAsync(id, StatusIsApproved)) {
                return NoContent(); // (good news)
            } else {
                return NotFound();
            }
        }
        

        // NO NO NO NO NO  NO  NO  NO  THIS ISN'T JUST GOING TO WORK.  MY METHOD WHICH IS NOW
        // COMMENTED OUT CONTAINED A FUNCTION THAT NOW ISN'T GETTING CALLED. i'M GOING
        // TO HAVE TO LOOK AT THIS IN DETAIL AND FIGURE OUT HOW TO INTEGRATE THIS NEW
        // METHOD BELOW INTO WHAT i HAVE DONE
        // THIS IS THE NEW METHOD, 10-29, FROM GREG.  REPLACES METHOD BELOW IT.
        // SEE "8-19 AND BEYOND" NOTES FOR CHANGES TO THE FRONT-END, HIGHLIGHTED IN PINK 10-29
        //PUT: api/Requests/Reject/5
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> PutStatusReject(int id, Request request) {
            request.Status = "REJECTED";  // RequestStatus.Rejected instead of "REJECTED" //for Greg. He had a class for this.
            // i assume i could put StatusIsRejected  --see const string above
            return await PutRequest(id, request);
        }



        //METHOD BELOW IS NOT RIGHT ANYMORE.  SEE ABOVE FOR THE NEW UPDATED METHOD
        // Takes id (from reviewer), changes status to "REJECTED" 
        // PUT: api/Requests/reject/5
        /*
        [HttpPut("reject/{id}")]    
        public async Task<IActionResult> PutStatusRejected(int id) { 
            if (await SetStatusValidAsync(id, StatusIsRejected)) {
                return NoContent(); // (good news)
            } else {
                return NotFound();
            }
        }   */



        // User picks one of their pre-existing requests in the Db, sets it to REVIEW status.
        // URL includes a coded message (review/5) which this method is listening for. 
        // PUT: api/Requests/review/5
        [HttpPut("review/{id}")]    
        public async Task<IActionResult> PutStatusReview(int id) {  
            var request = await _context.Requests.FindAsync(id);
            if(request == null) {
                return NotFound();
            }
            if (Decimal.Compare(request.Total, 50.0M) <= 0) {    // M indicates decimal 
                request.Status = StatusIsApproved;
            } else {
                request.Status = StatusIsReview;
            }
            _context.SaveChanges();
            return NoContent();
        }



        // ALL METHODS BELOW WERE AUTO-GENERATED

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
