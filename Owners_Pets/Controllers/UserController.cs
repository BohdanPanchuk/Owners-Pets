using System.Collections.Generic;
using System.Web.Http;
using Owners_Pets.Models;
using Owners_Pets.Areas;

namespace Owners_Pets.Controllers
{
    public class UserController : ApiController
    {
        Database database = new Database();

        // GET: api/User
        public IEnumerable<User> Get()
        {
            return database.GetAllUsers();
        }

        // GET: api/User/5
        public User Get(int id)
        {
            return database.GetUserById(id);
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
            User user = new User
            {
                Id = 0,
                Name = value.Split(',')[0],
                PetsCount = 0
            };

            database.AddUser(user);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]User user)
        {
            database.EditUser(id, user);
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
            database.DeleteUser(id);
        }
    }
}