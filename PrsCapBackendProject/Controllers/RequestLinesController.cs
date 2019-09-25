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


        // recalculate Total.  Although this method lives in the RequestLinesController class, and although the URL includes 
        // a call to RequestLines, the Id in the URL is NOT RequestLines.Id, but is in fact requestId, a.k.a Requests.Id
        // As of Sept. 24, calls to this method are peppered throughout this class: In get, getbyPk, put, insert, and delete.
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

        // Multi-purpose method, used either to (1) get ReqLine by PK, OR to (2) recalculate Request.Total
        // When used to recalculate, it still returns a requestlin, which will not be used for anything. Is that ok?
        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLine(int id) {
            var requestLine = await _context.RequestLines.FindAsync(id);

            if (requestLine == null)
            {
                return NotFound();
            }

            RecalcTotal(id);  // CALL METHOD TO RECALCULATE REQUEST.TOTAL
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

            return NoContent();
        }

        // POST: api/RequestLines
        [HttpPost]
        public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine) {
            _context.RequestLines.Add(requestLine);
            await _context.SaveChangesAsync();

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

            _context.RequestLines.Remove(requestLine);
            await _context.SaveChangesAsync();

            return requestLine;
        }


        private bool RequestLineExists(int id) {
            return _context.RequestLines.Any(e => e.Id == id);
        }


    }
}
