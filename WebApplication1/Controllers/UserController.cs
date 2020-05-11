using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            UserService service = new UserService();
            var user = service.GetUser(id);
            return Json(user);
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] User user)
        {
            UserService service = new UserService();
            service.CreateUser(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            UserService service = new UserService();
            service.UpdateUser(id, user);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            UserService service = new UserService();
            service.DeleteUser(id);
        }
    }
}
