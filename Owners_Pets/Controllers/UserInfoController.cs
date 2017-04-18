using System;
using System.Collections.Generic;
using System.Web.Http;
using Owners_Pets.Models;
using Owners_Pets.Areas;

namespace Owners_Pets.Controllers
{
    public class UserInfoController : ApiController
    {
        Database database = new Database();

        // GET: api/UserInfo
        public IEnumerable<UserInfo> Get()
        {
            return database.GetAllUserInfo();
        }

        // GET: api/UserInfo/5
        public IEnumerable<UserInfo> Get(int id)
        {
            return database.GetUserInfoByUserId(id);
        }

        // POST: api/UserInfo
        public void Post([FromBody]string value)
        {
            UserInfo userInfo = new UserInfo()
            {
                Id = 0,
                UserId = Convert.ToInt32(value.Split(',')[0]),
                PetName = value.Split(',')[1]
            };

            database.AddUserInfo(userInfo);
        }

        // PUT: api/UserInfo/5
        public void Put(int id, [FromBody]UserInfo userInfo)
        {
            database.EditUserInfo(id, userInfo);
        }

        // DELETE: api/UserInfo/5
        public void Delete(int id)
        {
            database.DeleteUserInfo(id);
        }
    }
}