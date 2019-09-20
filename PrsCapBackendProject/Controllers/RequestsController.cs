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

        public RequestsController(PrsCapDbContext context)
        {
            _context = context;
        }

        // Need to test this with dollar amounts near $50.00, and with $0.00, etc.
        // THIS IS A METHOD WHICH RECEIVES INPUT FROM A USER.  THE USER PICKS ONE OF THEIR PRE-EXISTING REQUESTS IN THE Db, SENDS
        // THE ID NUMBER, AND SOME BIT OF INFO THAT MEANS "THIS IS FOR REVIEW", AND THE METHOD HERE TAKES THE INFO AS ARGUMENTS
        // PUT: api/Requests/review/5 
        [HttpPut("review/{id}")]    //OKAY, so this attribute, in brackets, must be the thing that recognizes when a user, through postman,say, has entered the correct text in URL, namely, review/2.  AND, this line determines that the 2 should be put into the value {id}, which I assume is how the argument "int id" is fed.
        public async Task<IActionResult> PutStatusReview(int id) {
            var request = await _context.Requests.FindAsync(id);
            if(request == null) {
                return NotFound();
            }
            if (Decimal.Compare(request.Total, 50) < 0) {
                request.Status = "APPROVED"; //BUT:  USE CONSTANT STRING, DEFINE THEM AT TOP
            }  else {
            request.Status = "REVIEW";  // BUT USE CONSTANT STRING
            }
            _context.SaveChanges();
            return NoContent();
        }



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
