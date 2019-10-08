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
    public class RequestLinesController : ControllerBase
    {
        private readonly PrsCapDbContext _context;

        public RequestLinesController(PrsCapDbContext context)
        {
            _context = context;
        }

        // called by put, post, and delete methods below.  They pass in requestId, a.k.a. Requests.Id.
        private void RecalcTotal(int requestId) {

            var request = _context.Requests.Find(requestId);
            if(request == null) {
                throw new Exception("There is no request matching this ID"); // indicates a serious problem
            }
            //because of a bug in .NET core, can’t use request (requestlines?), have to use context, thus:
            request.Total = _context.RequestLines.Where(rL => rL.RequestId == requestId)
              .Sum(rL => rL.Product.Price * rL.Quantity);
            _context.SaveChanges();
        }



        // GET: api/RequestLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLines() {
            return await _context.RequestLines.ToListAsync();
        }


        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLine(int id) {
            var requestLine = await _context.RequestLines.FindAsync(id);

            if (requestLine == null)
            {
                return NotFound();
            }

            return requestLine;
        }

        // PUT: api/RequestLines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLine(int id, RequestLine requestLine) {
            if (id != requestLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // capture requestId, then call RecalcTotal
            var requestId = requestLine.RequestId;
            RecalcTotal(requestId);

            return NoContent();
        }

        // POST: api/RequestLines
        [HttpPost]
        public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine) {
            _context.RequestLines.Add(requestLine);
            await _context.SaveChangesAsync();

            // capture requestId, then call RecalcTotal
            var requestId = requestLine.RequestId;
            RecalcTotal(requestId);

            return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
        }

        // DELETE: api/RequestLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestLine>> DeleteRequestLine(int id) {
            var requestLine = await _context.RequestLines.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }

            // capture requestId before deletion, then do the delete (.Remove), then call RecalcTotal.
            var requestId = requestLine.RequestId;
            _context.RequestLines.Remove(requestLine);
            await _context.SaveChangesAsync();
            RecalcTotal(requestId); 

            return requestLine;
        }


        private bool RequestLineExists(int id) {
            return _context.RequestLines.Any(e => e.Id == id);
        }


    }
}
