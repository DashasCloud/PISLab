using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using PISLab.Models;

namespace PISLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _memCache = new List<Product>();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_memCache);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            if (_memCache.Count <= id) throw new IndexOutOfRangeException("No such");

            return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product value)
        {
            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            _memCache.Add(value);

            return Ok($"{value.ToString()} has been added");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product value)
        {
            if (_memCache.Count <= id) return NotFound("No such");

            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var previousValue = _memCache[id];
            _memCache[id] = value;

            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_memCache.Count <= id) return NotFound("No such");

            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);

            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }
}
