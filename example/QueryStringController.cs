using System.Collections.Generic;
using System.Net;
using DelimitedQueryString;
using Microsoft.AspNetCore.Mvc;

namespace Example
{
    public class Nested
    {
        public int NestedId { get; set; }
        public List<string> NestedItems { get; set; }
    }
    public class ComplexQuery
    {
        public int Id { get; set; }
        public List<string> Items { get; set; }
        public Nested Nest { get; set; }
        
    }

    public class QueryStringController : Controller
    {
        [HttpGet("/simple")]
        [ProducesResponseType(typeof(IEnumerable<string>), (int) HttpStatusCode.OK)]
        public IEnumerable<string> Simple([CommaDelimited] List<string> items)
        {
            return items;
        }

        [HttpGet("/complex")]
        [ProducesResponseType(typeof(ComplexQuery), (int) HttpStatusCode.OK)]
        public IActionResult Complex([FromQuery, CommaDelimited] ComplexQuery complexQuery)
        {
            return Ok(complexQuery);
        }
    }
}