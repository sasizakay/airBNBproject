using Ex1Ver6.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ex1Ver6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        // GET: api/<VacationsController>
        [HttpGet]
        public IEnumerable<Vacation> Get()
        {
            return new Vacation().Read();
        }

        // GET api/<VacationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VacationsController>
        [HttpPost]
        public bool Post([FromBody] Vacation value)
        {
            return value.Insert();
        }

        // PUT api/<VacationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VacationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet("getByDates/startDate/{startDate}/endDate/{endDate}")]
        public IEnumerable<Vacation> getByDates(DateTime startDate, DateTime endDate)
        {
            return new Vacation().getByDates(startDate, endDate);
        }
    }
}
