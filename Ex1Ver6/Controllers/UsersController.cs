using Ex1Ver6.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ex1Ver6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new User().Read();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public int Post([FromBody] User user)
        {
            return user.Insert();
        }

        // POST api/<UsersController>
        [HttpPost("{email}")]
        public User Login(string email, [FromBody] string password )
        {
            return new User().Login(email, password);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{email}")]
        public int Put(string email, [FromBody] string password)
        {
            return new User().Update(email, password);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
