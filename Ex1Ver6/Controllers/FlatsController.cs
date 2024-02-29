using Ex1Ver6.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ex1Ver6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        // GET: api/<FlatsController>
        [HttpGet]
        public IEnumerable<Flat> Get()
        {
            return new Flat().Read();
        }

        // GET api/<FlatsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FlatsController>
        [HttpPost]
        public bool Post([FromBody] Flat value)
        {
            return value.Insert();
        }

        // PUT api/<FlatsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FlatsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet("GetByCityAndPrice")]
        public IEnumerable<Flat> GetByCityAndPrice(string city, double price)
        {
            return new Flat().GetByCityAndPrice(city, price);
        }
    }
}
