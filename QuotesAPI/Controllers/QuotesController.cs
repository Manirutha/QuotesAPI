using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Data;
using QuotesAPI.Models;

namespace QuotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private QuotesDbContext _quotesDbContext;
        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }

        // GET: api/Quotes
        [HttpGet]
        public IActionResult Get()
        {
            //return _quotesDbContext.quotes;
            return Ok(_quotesDbContext.quotes);
        }

        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
           var quote =  _quotesDbContext.quotes.Find(id);
            if (quote == null)
            {
                return NotFound("No record found against this ID");
            }
            else
            {
                return Ok(quote);
            }
        }

        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quotesDbContext.quotes.Add(quote);
            _quotesDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
           var entity = _quotesDbContext.quotes.Find(id);
            if (entity == null)
            {
                return NotFound("No record found against this ID.");
            }
            else
            {
                entity.Title = quote.Title;
                entity.Author = quote.Author;
                entity.Description = quote.Description;
                _quotesDbContext.SaveChanges();
                return Ok("Record Updated successfully.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var quote = _quotesDbContext.quotes.Find(id);
            if (quote == null)
            {
                return NotFound("No record found against this ID.");
            }
            else
            {
                _quotesDbContext.quotes.Remove(quote);
                _quotesDbContext.SaveChanges();
                return Ok("Quote deleted.");
            }
        }
    }
}
